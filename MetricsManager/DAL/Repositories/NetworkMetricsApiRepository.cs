using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface INetworkMetricsApiRepository : IRepositoryApi<NetworkMetricApi>
    {
    }

    public class NetworkMetricsApiRepository
    {
        public NetworkMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
    }
}
