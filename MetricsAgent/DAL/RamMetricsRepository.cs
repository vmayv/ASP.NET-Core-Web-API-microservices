using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }
    public class RamMetricsRepository : IRamMetricsRepository
    {
        public void Create(RamMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public RamMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }
    }
}
