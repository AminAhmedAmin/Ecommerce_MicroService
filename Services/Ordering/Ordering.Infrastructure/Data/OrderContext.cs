using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.EmailAddress).HasMaxLength(100);
                entity.Property(e => e.AddressLine).HasMaxLength(200);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.State).HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(10);
                entity.Property(e => e.CardName).HasMaxLength(100);
                entity.Property(e => e.CardNumber).HasMaxLength(20);
                entity.Property(e => e.Expiration).HasMaxLength(10);
                entity.Property(e => e.CVV).HasMaxLength(4);
            });
        }
    }
}
