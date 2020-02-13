using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Concrete
{
    public class EFBtcRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Btc> Btc
        {
            get { return context.Btcs; }
            set { }
        }
        public void Create(string owner, double hash, string type)
        {
            context.Btcs.Add(new Btc { owner = owner, hash = hash, type = type, date = DateTime.Parse(DateTime.Now.ToUniversalTime().ToString("s").Substring(0, 17) + "00") });
            context.SaveChanges();
        }
    }
}
