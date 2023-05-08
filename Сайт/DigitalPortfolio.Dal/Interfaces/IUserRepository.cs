using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Dal.Interfaces
{
    public interface IUserRepository<User> : IBaseRepository<User>
    {
        int Count();
    }
}
