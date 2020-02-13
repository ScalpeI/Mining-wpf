using MiningConsole.Abstract;
using MiningConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<User> Users
        {
            get { return context.Users; }
            set { }
        }
    }
}
