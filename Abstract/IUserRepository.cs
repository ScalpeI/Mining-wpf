using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; set; }
    }
}
