﻿using Core.Interfaces;
using Dapper;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public interface ICpuMetricsApiRepository : IRepositoryApi<CpuMetricApi>
    {
    }

    public class CpuMetricsApiRepository
    {
        public CpuMetricsApiRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
    }
}
