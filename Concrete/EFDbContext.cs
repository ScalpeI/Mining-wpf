using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Mrr> Mrrs { get; set; }
        public DbSet<Sp> Sps { get; set; }
        public DbSet<Btc> Btcs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Minear> Minears { get; set; }
        public EFDbContext() : base("EFDbContext")
        {

        }
    }
}
