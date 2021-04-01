using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {

    }
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public void Create(NetworkMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public NetworkMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetric> GetByTimePeriod(string fromDate, string toDate)
        {
            throw new NotImplementedException();
        }
    }
}
