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
    public class CpuMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private ICpuMetricsRepository _repository;
        private PerformanceCounter _cpuCounter;

        public CpuMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<ICpuMetricsRepository>();
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new Models.CpuMetric { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
