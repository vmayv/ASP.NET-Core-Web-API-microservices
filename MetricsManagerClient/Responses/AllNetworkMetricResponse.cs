using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManagerClient.DTO;

namespace MetricsManagerClient.Responses
{
    public class AllNetworkMetricResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
