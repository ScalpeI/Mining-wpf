using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Abstract
{
    public interface IMrrRepository
    {
        IEnumerable<Mrr> Mrrs { get; set; }

        void CreateMrr(int idrig, string owner, double hash, string type);
        void DeleteMrr(Mrr mrr);
    }
}
