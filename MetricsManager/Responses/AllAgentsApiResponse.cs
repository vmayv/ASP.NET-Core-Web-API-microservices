using MetricsManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllAgentsApiResponse
    {
        public List<AgentDto> Agents { get; set; }
    }
}
