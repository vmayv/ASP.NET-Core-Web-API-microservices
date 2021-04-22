
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricApiJob : IJob
    {
        private readonly IMetricsAgentClient _agentClient;
        private readonly IAgentsRepository _agentsRepository;
        private readonly ICpuMetricsApiRepository _repository;

        public CpuMetricApiJob(IMetricsAgentClient agentClient, IAgentsRepository agentsRepository, ICpuMetricsApiRepository repository)
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

                var request = new GetAllCpuMetricsApiRequest
                {
                    ClientBaseAddress = agent.AgentAddress,
                    FromTime = fromTime,
                    ToTime = toTime
                };

                var metrics = _agentClient.GetAllCpuMetrics(request);
                
                if (metrics.Metrics.Count == 0)
                {
                    return Task.CompletedTask;
                }

                foreach (var metric in metrics.Metrics)
                {
                    _repository.Create(new CpuMetricApi { Time = metric.Time, Value = metric.Value, AgentId = agent.AgentId});
                }

            }

            return Task.CompletedTask;
        }
    }
}
