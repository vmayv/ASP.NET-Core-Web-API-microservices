﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricApiResponse
    {
        public List<NetworkMetricApiDto> Metrics { get; set; }
    }
}
