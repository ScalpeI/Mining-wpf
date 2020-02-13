using MiningConsole.Abstract;
using MiningConsole.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Globalization;

namespace MiningConsole.Concrete
{
    public class EFMinearRepository : IMinearRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Minear> Minears
        {
            get { return context.Minears; }
            set { }
        }
        public void UpdateEar(string Jsonstring)
        {
            try
            {
                JObject obj = JObject.Parse(Jsonstring);
                dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                if (jsonDe["amount_standard_earn"].ToString() != "0")
                {
                    context.Minears.Add(new Minear { mining_earnings = "", fpps_mining_earnings = "0.0000" + jsonDe["amount_standard_earn"].ToString() });
                }
                else
                {
                    context.Minears.Add(new Minear { mining_earnings = "", fpps_mining_earnings = context.Minears.OrderByDescending(x=>x.id).Take(2).Skip(1).Select(x=>x.fpps_mining_earnings).FirstOrDefault()});
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + " " +ex);
            }
        }
    }
}
