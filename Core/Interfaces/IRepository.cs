using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);

        IList<T> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate);
    }

}
