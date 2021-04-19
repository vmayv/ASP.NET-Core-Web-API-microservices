﻿using MetricsManager.Requests;
using MetricsManager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllGcHeapSizeMetricApiResponse GetAllGcHeapSizeMetrics(GetAllGcHeapSizeMetricsApiRequest request);

        AllHddMetricApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        AllNetworkMetricApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);

        AllRamMetricApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    }
}
