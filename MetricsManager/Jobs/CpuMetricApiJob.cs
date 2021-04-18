
using MetricsManager.Client;
using MetricsManager.DAL.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class CpuMetricApiJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsRepository _repositoryAgent;
        private ICpuMetricsApiRepository _repositoryCpu;
    }
}
