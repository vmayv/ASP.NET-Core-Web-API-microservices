using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManagerClient.DTO;

namespace MetricsManagerClient.Responses
{
    public class AllGcHeapSizeMetricResponse
    {
        public List<GcHeapSizeMetricDto> Metrics { get; set; }
    }
}
