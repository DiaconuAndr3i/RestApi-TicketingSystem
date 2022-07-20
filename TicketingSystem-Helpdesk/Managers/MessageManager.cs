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
    public class MessageManager : IMessageManager
    {
        private readonly IMessageRepository messageRepository;
        private readonly IServicesManager servicesManager;
        private readonly ITicketRepository ticketRepository;
        private readonly ProducerConfig producerConfig;
        private readonly UserManager<User> userManager;


        public MessageManager(IMessageRepository messageRepository,
            IServicesManager servicesManager,
            ITicketRepository ticketRepository,
            ProducerConfig producerConfig,
            UserManager<User> userManager)
        {
            this.messageRepository = messageRepository;
            this.servicesManager = servicesManager;
            this.ticketRepository = ticketRepository;
            this.producerConfig = producerConfig;
            this.userManager = userManager;
        }

        public async Task AddMessage(MessageModel messageModel)
        {
            var message = new Message
            {
                Id = servicesManager.CreateId(),
                TicketId = messageModel.TicketId,
                CreatedDate = servicesManager.GenerateCurrentDate(),
                ContentMessage = messageModel.ContentMessage,
                AuthorMessage = messageModel.EmailCreator
            };

            await messageRepository.AddMessage(message);

            var ticket = await ticketRepository.GetAllTickets()
                .Where(x => x.Id.Equals(message.TicketId)).FirstOrDefaultAsync();

            var emailA = await userManager.GetEmailAsync(await userManager.FindByIdAsync(ticket.UserId));
            var emailB = await userManager.GetEmailAsync(await userManager.FindByIdAsync(ticket.Arrival));

            var messageModelForKafka = new
            {
                CreatorEmail = messageModel.EmailCreator,
                ArrivalEmail = emailA == messageModel.EmailCreator ? emailB : emailA
            };

            _ = await ProduceMessageForKafkaBroker(messageModelForKafka, "newMessage");

        }

        public async Task<Boolean> ProduceMessageForKafkaBroker(object obj, string topic)
        {
            string serializedJson = JsonConvert.SerializeObject(obj);

            using (var producerForKafkaBroker = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                await producerForKafkaBroker.ProduceAsync(topic, new Message<Null, string> { Value = serializedJson });
                producerForKafkaBroker.Flush(TimeSpan.FromSeconds(20));
                return true;
            }
        }

        public MessageModel CreateObjectMessageModel(CreateTicketModel createTicketModel, string idTicket)
        {
            var messageModel = new MessageModel
            {
                TicketId = idTicket,
                ContentMessage = createTicketModel.ContentMessage,
                EmailCreator = createTicketModel.TicketModel.TicketCreatorEmail
            };

            return messageModel;
        }

        public async Task<List<ReceiveMessageModel>> GetMessagesFromTicket(string idTicket)
        {
            var messages = await messageRepository.GetAllMessages()
                .Where(x => x.TicketId.Equals(idTicket))
                .ToListAsync();

            var list = new List<ReceiveMessageModel>();
            foreach(var item in messages)
            {
                list.Add(new ReceiveMessageModel
                {
                    MessageModel = new MessageModel()
                    {
                        TicketId = item.TicketId,
                        ContentMessage = item.ContentMessage,
                        EmailCreator = item.AuthorMessage
                    },
                    IdMessage = item.Id
                });
            }

            return list;
        }

        public async Task<Message> GetMessageById(string idMessage)
        {
            var message = await messageRepository.GetAllMessages()
                .Where(x => x.Id.Equals(idMessage))
                .FirstOrDefaultAsync();

            return message;
        }

        public async Task<bool> DeleteMessage(string idMessage)
        {
            var message = await GetMessageById(idMessage);

            if (message == null)
                return false;

            await messageRepository.DeleteMessage(message);

            return true;
        }

        public async Task<bool> UpdateMessage(string idMessage, MessageModel messageModel)
        {
            var message = await GetMessageById(idMessage);

            if (message == null)
                return false;

            message.ContentMessage = messageModel.ContentMessage;

            await messageRepository.SaveChanges();

            return true;
        }
    }
}
