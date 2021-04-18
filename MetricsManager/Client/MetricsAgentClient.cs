using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public AllCpuMetricApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllCpuMetricApiResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllGcHeapSizeMetricApiResponse GetAllGcHeapSizeMetrics(GetAllGcHeapSizeMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/dotnet/errors-count/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllGcHeapSizeMetricApiResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllHddMetricApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.TotalSeconds;
            var toParameter = request.ToTime.TotalSeconds;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricApiResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }


        public AllNetworkMetricApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllNetworkMetricApiResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/ram/avaliable/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllRamMetricApiResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}

