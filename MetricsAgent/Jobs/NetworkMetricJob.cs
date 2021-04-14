using MetricsAgent.DAL.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter();
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
