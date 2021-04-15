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
            _networkCounter = new PerformanceCounter("Сетевой интерфейс", "Всего байт/с", "Realtek PCIe GBE Family Controller");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var allBytesPerSecond = Convert.ToInt32(_networkCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new Models.NetworkMetric { Time = time, Value = allBytesPerSecond });

            return Task.CompletedTask;
        }
    }
}
