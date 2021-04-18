﻿using Core.Interfaces;
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
    }
}
