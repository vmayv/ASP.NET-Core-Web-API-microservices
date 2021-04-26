using MetricsManagerClient.Requests;
using MetricsManagerClient.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManagerClient.Client
{
    public interface IMetricsManagerClient
    {
        AllCpuMetricResponse GetAllCpuMetrics(GetAllCpuMetricsRequest request);

        AllGcHeapSizeMetricResponse GetAllGcHeapSizeMetrics(GetAllGcHeapSizeMetricsRequest request);

        AllHddMetricResponse GetAllHddMetrics(GetAllHddMetricsRequest request);

        AllNetworkMetricResponse GetAllNetworkMetrics(GetAllNetworkMetricsRequest request);

        AllRamMetricResponse GetAllRamMetrics(GetAllRamMetricsRequest request);

        AllAgentsResponse GetAllAgents();
    }
}
