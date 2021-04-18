using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface IHddMetricsApiRepository : IRepositoryApi<HddMetricApi>
    {
    }

    public class HddMetricsApiRepository
    {
        public HddMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
    }
}
