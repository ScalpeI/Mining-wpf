using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Abstract
{
    public interface IMinearRepository
    {
        IEnumerable<Minear> Minears { get; set; }
        void UpdateEar(string Jsonstring);
    }
}
