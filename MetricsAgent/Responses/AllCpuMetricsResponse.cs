﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
