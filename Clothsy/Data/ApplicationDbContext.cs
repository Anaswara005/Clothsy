using Clothsy.Models;
using Clothsy.Models.Donation;
using Clothsy.Models.Profile;
using Clothsy.Models.SignupModels;
using Clothsy.Models.Web.Auth;
using Microsoft.EntityFrameworkCore;
namespace Clothsy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationImage> DonationImages { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<DonationRequest> DonationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Hub configuration
            modelBuilder.Entity<Hub>(entity =>
            {
                entity.HasIndex(h => h.Email).IsUnique();
            });


            // Seed Kerala District Hubs
            modelBuilder.Entity<Hub>().HasData(
                new Hub
                {
                    Id = 1,
                    Name = "Clothsy Central Hub - Trivandrum",
                    Email = "clothsy.trivandrum@clothsy.in",
                    Phone = "+91 90000 00000",
                    Address = " Trivandrum, Kerala",
                    District = "Trivandrum",
                    IsActive = true
                },
               new Hub
               {
                   Id = 2,
                   Name = "Clothsy Central Hub - Kollam",
                   Email = "clothsy.kollam@clothsy.in",
                   Phone = "+91 90000 00001",
                   Address = "Kollam, Kerala",
                   District = "Kollam",
                   IsActive = true
               },
               new Hub
               {
                   Id = 3,
                   Name = "Clothsy Central Hub - Pathanamthitta",
                   Email = "clothsy.pathanamthitta@clothsy.in",
                   Phone = "+91 90000 00002",
                   Address = "Pathanamthitta, Kerala",
                   District = "Pathanamthitta",
                   IsActive = true
               },
               new Hub
               {
                   Id = 4,
                   Name = "Clothsy Central Hub - Alappuzha",
                   Email = "clothsy.alappuzha@clothsy.in",
                   Phone = "+91 90000 00003",
                   Address = "Alappuzha, Kerala",
                   District = "Alappuzha",
                   IsActive = true
               },
               new Hub
               {
                   Id = 5,
                   Name = "Clothsy Central Hub - Kottayam",
                   Email = "clothsy.kottayam@clothsy.in",
                   Phone = "+91 90000 00004",
                   Address = "Kottayam, Kerala",
                   District = "Kottayam",
                   IsActive = true
               },
               new Hub
               {
                   Id = 6,
                   Name = "Clothsy Central Hub - Idukki",
                   Email = "clothsy.idukki@clothsy.in",
                   Phone = "+91 90000 00005",
                   Address = "Idukki, Kerala",
                   District = "Idukki",
                   IsActive = true
               },
               new Hub
               {
                   Id = 7,
                   Name = "Clothsy Central Hub - Ernakulam",
                   Email = "clothsy.ernakulam@clothsy.in",
                   Phone = "+91 90000 00006",
                   Address = "Ernakulam, Kerala",
                   District = "Ernakulam",
                   IsActive = true
               },
               new Hub
               {
                   Id = 8,
                   Name = "Clothsy Central Hub - Thrissur",
                   Email = "clothsy.thrissur@clothsy.in",
                   Phone = "+91 90000 00007",
                   Address = "Thrissur, Kerala",
                   District = "Thrissur",
                   IsActive = true
               },
               new Hub
               {
                   Id = 9,
                   Name = "Clothsy Central Hub - Palakkad",
                   Email = "clothsy.palakkad@clothsy.in",
                   Phone = "+91 90000 00008",
                   Address = "Palakkad, Kerala",
                   District = "Palakkad",
                   IsActive = true
               },
               new Hub
               {
                   Id = 10,
                   Name = "Clothsy Central Hub - Malappuram",
                   Email = "clothsy.malappuram@clothsy.in",
                   Phone = "+91 90000 00009",
                   Address = "Malappuram, Kerala",
                   District = "Malappuram",
                   IsActive = true
               },
               new Hub
               {
                   Id = 11,
                   Name = "Clothsy Central Hub - Kozhikode",
                   Email = "clothsy.kozhikode@clothsy.in",
                   Phone = "+91 90000 00010",
                   Address = "Kozhikode, Kerala",
                   District = "Kozhikode",
                   IsActive = true
               },
               new Hub
               {
                   Id = 12,
                   Name = "Clothsy Central Hub - Wayanad",
                   Email = "clothsy.wayanad@clothsy.in",
                   Phone = "+91 90000 00011",
                   Address = "Wayanad, Kerala",
                   District = "Wayanad",
                   IsActive = true
               },
               new Hub
               {
                   Id = 13,
                   Name = "Clothsy Central Hub - Kannur",
                   Email = "clothsy.kannur@clothsy.in",
                   Phone = "+91 90000 00012",
                   Address = "Kannur, Kerala",
                   District = "Kannur",
                   IsActive = true
               },
               new Hub
               {
                   Id = 14,
                   Name = "Clothsy Central Hub - Kasaragod",
                   Email = "clothsy.kasaragod@clothsy.in",
                   Phone = "+91 90000 00013",
                   Address = "Kasaragod, Kerala",
                   District = "Kasaragod",
                   IsActive = true
               }

            );
            // User configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Address configuration
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // -------------- DONATION --------------
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasOne(d => d.Donor)
                     .WithMany()
                     .HasForeignKey(d => d.DonorUserId)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.AssignedHub)
                    .WithMany()
                    .HasForeignKey(d => d.AssignedHubId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            // DonationImage 
            modelBuilder.Entity<DonationImage>(entity =>
            {
                entity.HasOne(di => di.Donation)
                    .WithMany(d => d.Images)
                    .HasForeignKey(di => di.DonationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // -------------- REQUEST --------------
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasOne(r => r.Requester)
                    .WithMany()
                    .HasForeignKey(r => r.RequesterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.DeliveryAddress)
                    .WithMany()
                    .HasForeignKey(r => r.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // -------------- SUPPORT TICKET --------------
            modelBuilder.Entity<SupportTicket>()
    .HasOne(st => st.User)
    .WithMany()
    .HasForeignKey(st => st.UserId)
    .OnDelete(DeleteBehavior.Cascade);

            // Configure DonationRequest relationships
            modelBuilder.Entity<DonationRequest>()
                .HasOne(dr => dr.Donation)
                .WithMany()
                .HasForeignKey(dr => dr.DonationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonationRequest>()
                .HasOne(dr => dr.Requester)
                .WithMany()
                .HasForeignKey(dr => dr.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonationRequest>()
                .HasOne(dr => dr.Address)
                .WithMany()
                .HasForeignKey(dr => dr.AddressId);
        }

        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<NotificationRead> NotificationReads { get; set; }

        //Hub and Admin portal
        public DbSet<WebUser> WebUsers { get; set; }




    }
}


