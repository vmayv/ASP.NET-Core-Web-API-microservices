using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllGcHeapSizeMetricApiResponse
    {
        public List<GcHeapSizeMetricApiDto> Metrics { get; set; }
    }
}
