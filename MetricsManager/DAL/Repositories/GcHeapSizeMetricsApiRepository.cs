using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface IGcHeapSizeMetricsApiRepository : IRepositoryApi<GcHeapSizeMetricApi>
    {
    }

    public class GcHeapSizeMetricsApiRepository
    {
        public GcHeapSizeMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
    }
}
