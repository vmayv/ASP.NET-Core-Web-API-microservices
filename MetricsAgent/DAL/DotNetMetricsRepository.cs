using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public void Create(DotNetMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<DotNetMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public DotNetMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DotNetMetric GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }

        public IList<DotNetMetric> GetByTimePeriod(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }
    }
}
