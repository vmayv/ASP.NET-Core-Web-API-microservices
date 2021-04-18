using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllRamMetricApiResponse
    {
        public List<RamMetricApiDto> Metrics { get; set; }
    }
}
