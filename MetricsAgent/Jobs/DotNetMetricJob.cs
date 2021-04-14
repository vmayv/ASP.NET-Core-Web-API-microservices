using MetricsAgent.DAL.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
