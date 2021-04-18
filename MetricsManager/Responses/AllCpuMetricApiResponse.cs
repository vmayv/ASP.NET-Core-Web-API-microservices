using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllCpuMetricApiResponse
    {
        public List<CpuMetricApiDto> Metrics { get; set; }
    }
}
