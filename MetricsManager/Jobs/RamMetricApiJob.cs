using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using System.Threading.Tasks;
using MetricsManager.Client;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;
using MetricsManager.DAL.Models;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricApiJob : IJob
    {
        private readonly IMetricsAgentClient _agentClient;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IRamMetricsApiRepository _repository;

        public RamMetricApiJob(IMetricsAgentClient agentClient, IAgentsRepository agentsRepository, IRamMetricsApiRepository repository)
        {
            _agentClient = agentClient;
            _repository = repository;
            _agentsRepository = agentsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agentsList = _agentsRepository.GetAgentList();
            if (agentsList.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var agent in agentsList)
            {
                DateTimeOffset fromTime = _repository.GetLastTime(agent.AgentId);
                DateTimeOffset toTime = DateTimeOffset.UtcNow;

                var request = new GetAllRamMetricsApiRequest
                {
                    ClientBaseAddress = agent.AgentAddress,
                    FromTime = fromTime,
                    ToTime = toTime
                };

                var metrics = _agentClient.GetAllRamMetrics(request);
                
                if (metrics.Metrics.Count == 0)
                {
                    return Task.CompletedTask;
                }

                foreach (var metric in metrics.Metrics)
                {
                    _repository.Create(new RamMetricApi { Time = metric.Time, Value = metric.Value, AgentId = agent.AgentId });
                }

            }

            return Task.CompletedTask;
        }
    }
}
