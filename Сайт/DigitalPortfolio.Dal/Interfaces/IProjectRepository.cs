using DigitalPortfolio.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Dal.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        void Add1(Project entity);
    }
}
