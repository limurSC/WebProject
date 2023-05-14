using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Dal.Interfaces
{
    public interface IBaseRepository<T>
    {
		Task<bool> Add(T entity);

        IQueryable<T> GetAll();
        IEnumerable<T> Get();

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);
        public int Id();
    }
}
