using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Core.Entities.Order> Orders { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken       = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "bane";//TODO: Get current user from identity server for ex.
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "bane";//TODO: Get current user from identity server for ex.
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        } 
    }
}
