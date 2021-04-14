﻿using MetricsAgent.DAL.Repositories;
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
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter();
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
