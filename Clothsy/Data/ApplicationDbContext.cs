using Microsoft.EntityFrameworkCore;
using Clothsy.Models.SignupModels;

namespace Clothsy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OtpLog> OtpLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.PhoneNumber).IsUnique().HasFilter("[PhoneNumber] IS NOT NULL");
                entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            });
        }
    }
}