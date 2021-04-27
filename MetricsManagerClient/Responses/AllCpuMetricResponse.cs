using MetricsManagerClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManagerClient.Responses
{
    public class AllCpuMetricResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
