using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Context : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagTicket> TagTickets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<AddressInstitution> AddressInstitutions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentInstitution> DepartmentInstitutions { get; set; }
        public DbSet<Subdepartment> Subdepartments { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestRoles> GuestRoles { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Institution>()
                .HasOne(a => a.AddressInstitution)
                .WithOne(ai => ai.Institution);

            builder.Entity<Guest>()
                .HasOne(g => g.User)
                .WithOne(u => u.Guest);

            builder.Entity<Status>()
                .HasOne(s => s.Ticket)
                .WithOne(t => t.Status);

            builder.Entity<Priority>()
                .HasOne(p => p.Ticket)
                .WithOne(t => t.Priority);

            builder.Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User);

            builder.Entity<Ticket>()
                .HasMany(t => t.Messages)
                .WithOne(m => m.Ticket);

            builder.Entity<Department>()
                .HasMany(d => d.Subdepartments)
                .WithOne(sd => sd.Department);

            builder.Entity<Guest>()
                .HasMany(g => g.GuestRoles)
                .WithOne(gr => gr.Guest);

            builder.Entity<TagTicket>().HasKey(tagticket 
                => new { tagticket.TagId, tagticket.TicketId });

            builder.Entity<TagTicket>()
                .HasOne<Ticket>(tagticket => tagticket.Ticket)
                .WithMany(t => t.TagTickets)
                .HasForeignKey(tagticket => tagticket.TicketId);

            builder.Entity<TagTicket>()
                .HasOne<Tag>(tagticket => tagticket.Tag)
                .WithMany(tag => tag.TagTickets)
                .HasForeignKey(tagticket => tagticket.TagId);

            builder.Entity<UserRole>().HasKey(ur
                => new { ur.InstitutionId, ur.RoleId, ur.UserId});

            builder.Entity<UserRole>()
                .HasOne<Institution>(ur => ur.Institution)
                .WithMany(i => i.UserRoleInstitutions)
                .HasForeignKey(ur => ur.InstitutionId);

            builder.Entity<UserRole>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne<Role>(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<DepartmentInstitution>().HasKey(di
                => new { di.DepartmentId, di.InstitutionId });

            builder.Entity<DepartmentInstitution>()
                .HasOne<Institution>(di => di.Institution)
                .WithMany(i => i.DepartmentInstitutions)
                .HasForeignKey(di => di.InstitutionId);

            builder.Entity<DepartmentInstitution>()
                .HasOne<Department>(di => di.Department)
                .WithMany(d => d.DepartmentInstitutions)
                .HasForeignKey(di => di.DepartmentId);
        }
    }
}
