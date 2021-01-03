﻿using BusBookingApp.Data.DTOs;
using BusBookingApp.Data.Models;
using BusBookingApp.PayStackApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusBookingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusTicket> BusTickets { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        //public DbSet<InitializedTransactionResponse> InitializedTransactionResponses { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<Bus>().HasIndex(b => b.BusNumber).IsUnique();
            builder.Entity<BusTicket>().HasIndex(b => b.TicketNumber).IsUnique();

            //builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

            // Seed Data for Destination
            builder.Entity<Destination>().HasData(new { DestinationId = 1, Name = "Accra", Fare = "GHS 30" });
            builder.Entity<Destination>().HasData(new { DestinationId = 2, Name = "Takoradi", Fare = "GHS 30" });
            builder.Entity<Destination>().HasData(new { DestinationId = 3, Name = "Tema", Fare = "GHS 30" });
            builder.Entity<Destination>().HasData(new { DestinationId = 4, Name = "CapeCoast", Fare = "GHS 30" });
            builder.Entity<Destination>().HasData(new { DestinationId = 5, Name = "Sunyani", Fare = "GHS 30" });
        }
    }
}
