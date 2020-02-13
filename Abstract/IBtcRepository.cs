using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Abstract
{
    public interface IBtcRepository
    {
        IEnumerable<Btc> Btcs { get; set; }
        void Create(string owner, double hash, string type);
    }
}
