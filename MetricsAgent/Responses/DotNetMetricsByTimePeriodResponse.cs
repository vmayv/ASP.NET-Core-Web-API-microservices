﻿using MetricsAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class DotNetMetricsByTimePeriodResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
