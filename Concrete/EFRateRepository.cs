using MiningConsole.Abstract;
using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Concrete
{
    public class EFRateRepository : IRateRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Rate> Rates
        {
            get { return context.Rates; }
            set { }
        }

        public void SaveRate(Rate rate)
        {
            context.Rates.Attach(rate);
            context.Entry(rate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
