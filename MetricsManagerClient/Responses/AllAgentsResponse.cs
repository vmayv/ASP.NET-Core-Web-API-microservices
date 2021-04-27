using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManagerClient.DTO;

namespace MetricsManagerClient.Responses
{
    public class AllAgentsResponse
    {
        public List<AgentDto> Agents { get; set; }
    }
}
