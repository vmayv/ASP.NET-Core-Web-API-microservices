﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllGcHeapSizeMetricApiResponse
    {
        public List<GcHeapSizeMetricApiDto> Metrics { get; set; }
    }
}
