using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesW1.Models;

namespace SalesW1.Data
{
    public class SalesW1Context : DbContext
    {
        public SalesW1Context (DbContextOptions<SalesW1Context> options)
            : base(options)
        {
        }

        //public DbSet<SalesW1.Models.Department> Department { get; set; } => COM ou SEM SalesW1.Models FUNCIONA, por estar no mesmo namespace
        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
