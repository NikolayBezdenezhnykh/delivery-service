using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DeliveryDbContext : DbContext
    {
        public DbSet<ReserveCourier> ReserveCouriers { get; set; }

        public DbSet<AvailableCourier> AvailableCouriers { get; set; }

        public DeliveryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
