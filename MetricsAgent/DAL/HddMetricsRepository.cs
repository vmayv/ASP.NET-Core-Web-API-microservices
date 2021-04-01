using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        public void Create(HddMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<HddMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public HddMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<HddMetric> GetByTimePeriod(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }
    }
}
