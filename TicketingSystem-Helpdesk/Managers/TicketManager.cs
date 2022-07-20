using Confluent.Kafka;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class TicketManager : ITicketManager
    {
        private readonly UserManager<User> userManager;
        private readonly IServicesManager servicesManager;
        private readonly IPriorityManager priorityManager;
        private readonly IStatusManager statusManager;
        private readonly ITicketRepository ticketRepository;
        private readonly IMessageManager messageManager;
        private readonly ITagManager tagManager;
        private readonly IStatisticsManager statisticsManager;
        private readonly ProducerConfig producerConfig;

        public TicketManager(UserManager<User> userManager,
            IServicesManager servicesManager,
            IPriorityManager priorityManager,
            IStatusManager statusManager,
            ITicketRepository ticketRepository,
            IMessageManager messageManager,
            ITagManager tagManager,
            IStatisticsManager statisticsManager,
            ProducerConfig producerConfig)
        {
            this.userManager = userManager;
            this.servicesManager = servicesManager;
            this.priorityManager = priorityManager;
            this.statusManager = statusManager;
            this.ticketRepository = ticketRepository;
            this.messageManager = messageManager;
            this.tagManager = tagManager;
            this.statisticsManager = statisticsManager;
            this.producerConfig = producerConfig;
        }

        public async Task<Ticket> CreateObjectTicket(CreateTicketModel createTicketModel)
        {
            var arrival = await userManager.FindByEmailAsync(createTicketModel.TicketModel.ArrivalEmail);
            var creator = await userManager.FindByEmailAsync(createTicketModel.TicketModel.TicketCreatorEmail);

            var ticket = new Ticket
            {
                Id = servicesManager.CreateId(),
                Title = createTicketModel.TicketModel.Title,
                CreatedDate = servicesManager.GenerateCurrentDate(),
                Arrival = arrival.Id,
                UserId = creator.Id,
                NewActivity = createTicketModel.TicketModel.NewActivity,
                PriorityId = await priorityManager.GetPriorityIdByName(createTicketModel.TicketModel.Priority),
                StatusId = await statusManager.GetStatusIdByName(createTicketModel.TicketModel.Status)
            };

            return ticket;
        }

        public async Task<bool> CreateTicket(CreateTicketModel createTicketModel)
        {
            var creatorEmail = await userManager.FindByEmailAsync(createTicketModel.TicketModel.TicketCreatorEmail);
            var arrivalEmail = await userManager.FindByEmailAsync(createTicketModel.TicketModel.ArrivalEmail);


            if (!servicesManager.ValidationExistUser(creatorEmail) || 
                !servicesManager.ValidationExistUser(arrivalEmail))
                return false;

            var ticket = await CreateObjectTicket(createTicketModel);
            await ticketRepository.AddTicket(ticket);

            var messageModel = messageManager.CreateObjectMessageModel(createTicketModel, ticket.Id);
            await messageManager.AddMessage(messageModel);

            if (servicesManager.ValidationCountZero(createTicketModel.TicketModel.Tag))
            {

                foreach(var item in createTicketModel.TicketModel.Tag)
                {
                    var idTag = await tagManager.GetIdTagByName(item);
                    await tagManager.AddTagTicket(ticket.Id, idTag);
                }   
            }


            var openClosed = await this.statisticsManager.NumberOfTicketsOpenClosed();
            _ =  await this.ProduceMessageForKafkaBroker(openClosed, "openClosed");

            var messageModelForKafka = new
            {
                CreatorEmail = createTicketModel.TicketModel.TicketCreatorEmail,
                ArrivalEmail = createTicketModel.TicketModel.ArrivalEmail
            };
            _ = await ProduceMessageForKafkaBroker(messageModelForKafka, "newMessage");

                return true;
        }

        //Producer for kafka broker
        public async Task<Boolean> ProduceMessageForKafkaBroker(object obj, string topic) {
            //var openClosed = await this.statisticsManager.NumberOfTicketsOpenClosed();
            string serializedJson = JsonConvert.SerializeObject(obj);

            using (var producerForKafkaBroker = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                await producerForKafkaBroker.ProduceAsync(topic, new Message<Null, string> { Value = serializedJson });
                producerForKafkaBroker.Flush(TimeSpan.FromSeconds(20));
                return true;
            }
        }

        public async Task<List<ReceiveTicketModel>> GetAllTicketsByPriority(string priority, string email, bool direction)
        {
            var ticketsByEmail = await GetAllTicketsByCreatorArrivalEmail(email, direction);

            var list = new List<ReceiveTicketModel>();

            foreach(var item in ticketsByEmail)
            {
                var priorityNameForItem = await priorityManager.GetPriorityIdByName(item.TicketModel.Priority);
                if (priorityNameForItem.Equals(priority))
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public async Task<List<ReceiveTicketModel>> GetAllTicketsByStatus(string status, string email, bool direction)
        {
            var ticketsByEmail = await GetAllTicketsByCreatorArrivalEmail(email, direction);

            var list = new List<ReceiveTicketModel>();

            foreach(var item in ticketsByEmail)
            {
                var statusNameForItem = await statusManager.GetStatusIdByName(item.TicketModel.Status);
                if (statusNameForItem.Equals(status))
                {
                    list.Add(item);
                }
            }

            return list;

        }

        public async Task<List<ReceiveTicketModel>> GetAllTicketsByCreatorArrivalEmail(string email, bool direction)
        {
            var user= await userManager.FindByEmailAsync(email);

            var tickets = await ticketRepository.GetAllTickets()
                .Where(x => direction == true ? x.UserId.Equals(user.Id): x.Arrival.Equals(user.Id))
                .OrderByDescending(x => x.NewActivity == true ? 1 : 0)
                .ToListAsync();

            var list = new List<ReceiveTicketModel>();
            foreach (var item in tickets)
            {
                var userArrivalEmail = await userManager.FindByIdAsync(item.Arrival);
                var userCreatorEmail = await userManager.FindByIdAsync(item.UserId);

                list.Add(new ReceiveTicketModel
                {
                    TicketModel = new TicketModel()
                    {
                        Title = item.Title,
                        CreatedDate = item.CreatedDate,
                        ArrivalEmail = userArrivalEmail.Email,
                        TicketCreatorEmail = userCreatorEmail.Email,
                        NewActivity = item.NewActivity,
                        Priority = await priorityManager.GetPriorityNameById(item.PriorityId),
                        Status = await statusManager.GetStatusNameById(item.StatusId),
                        Tag = await tagManager.GetTagsNameTicket(item.Id)
                    },
                    IdTicket = item.Id
                });

            }

            return list;
        }

        public async Task<Ticket> GetTicketById(string idTicket)
        {
            var ticket = await ticketRepository.GetAllTickets()
                .Where(x => x.Id.Equals(idTicket)).FirstOrDefaultAsync();

            return ticket;
        }

        public async Task<bool> DeleteTicket(string idTicket)
        {
            var ticket = await GetTicketById(idTicket);

            if (ticket == null)
                return false;

            await ticketRepository.DeleteTicket(ticket);

            var openClosed = await this.statisticsManager.NumberOfTicketsOpenClosed();
            _ = await this.ProduceMessageForKafkaBroker(openClosed, "openClosed");

            return true;
        }

        public async Task<bool> ChangeStatusTicket(string idTicket, string status)
        {
            var ticket = await GetTicketById(idTicket);
            if (ticket == null)
                return false;

            var idStatus = await statusManager.GetStatusIdByName(status);

            if (idStatus == null)
                return false;

            ticket.StatusId = idStatus;

            await ticketRepository.SaveChanges();

            var openClosed = await this.statisticsManager.NumberOfTicketsOpenClosed();
            _ = await this.ProduceMessageForKafkaBroker(openClosed, "openClosed");

            return true;

        }

        public async Task<bool> ChangeNewActivityTicket(string idTicket, bool activity)
        {
            var ticket = await GetTicketById(idTicket);
            if (ticket == null)
                return false;

            ticket.NewActivity = activity;

            await ticketRepository.SaveChanges();

            return true;
        }
    }
}
