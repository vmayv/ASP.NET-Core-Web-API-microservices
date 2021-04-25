using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManagerClient.DTO;

namespace MetricsManagerClient.Responses
{
    public class AllRamMetricResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
