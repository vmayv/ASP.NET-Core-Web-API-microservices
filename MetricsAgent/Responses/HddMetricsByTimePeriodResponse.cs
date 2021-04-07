using MetricsAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
