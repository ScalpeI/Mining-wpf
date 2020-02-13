using MiningConsole.Abstract;
using MiningConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Concrete
{
    class EFMrrRepository : IMrrRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Mrr> Mrrs
        {
            get { return context.Mrrs; }
            set { }
        }
        public void CreateMrr(int idrig,string owner, double hash,string type)
        {
            DateTime dt = DateTime.Parse(DateTime.Now.ToUniversalTime().ToString("s").Substring(0, 17) + "00");
            if (context.Mrrs.Where(x => x.idrig == idrig && x.owner == owner && x.date == dt).Count()== 0)
            {
                context.Mrrs.Add(new Mrr { idrig = idrig, owner = owner, hash = hash, type = type, date = DateTime.Parse(DateTime.Now.ToUniversalTime().ToString("s").Substring(0, 17) + "00") });
                context.SaveChanges();
            }
        }
        public void DeleteMrr(Mrr mrr)
        {
            context.Entry(mrr).State = EntityState.Deleted;
            context.Mrrs.Remove(mrr);
            context.SaveChanges();
        }
    }
}
