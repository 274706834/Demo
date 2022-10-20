using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEntity
{
    public class DemoDbContext : DbContext
    {
    
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
          : base(options)
        {
        }

        public DbSet<MachinePayLoad> Machines { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
