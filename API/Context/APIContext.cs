using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
        }
        //public UIContext() : base()
        //{ }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\MedicaresDatabase.mdf;Integrated Security=True;Connect Timeout=30")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public DbSet<Entities.Doctor> Doctor { get; set; } = default!;
        public DbSet<Entities.Medication> Medication { get; set; } = default!;
        public DbSet<Entities.Patient> Patient { get; set; } = default!;
    }
}
