using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UI.Models;

namespace UI.Data
{
    public class UIContext : DbContext
    {
        public UIContext (DbContextOptions<UIContext> options)
            : base(options)
        {
        }
        //public UIContext() : base()
        //{ }
        public DbSet<UI.Models.Doctor> Doctor { get; set; } = default!;
        public DbSet<UI.Models.Medication> Medication { get; set; } = default!;
        public DbSet<UI.Models.Patient> Patient { get; set; } = default!;
    }
}
