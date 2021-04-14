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
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter();
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
