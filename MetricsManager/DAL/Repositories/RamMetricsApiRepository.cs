using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface IRamMetricsApiRepository : IRepositoryApi<RamMetricApi>
    {
    }

    public class RamMetricsApiRepository
    {
        public RamMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
    }
}
