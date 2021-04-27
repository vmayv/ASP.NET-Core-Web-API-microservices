using MetricsManagerClient.DTO;
using MetricsManagerClient.Requests;
using MetricsManagerClient.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManagerClient.Client
{
    public class MetricsManagerClient : IMetricsManagerClient
    {

        private readonly HttpClient _httpClient;
        //private readonly ILogger<MetricsManagerClient> _logger;

        public MetricsManagerClient(HttpClient httpClient)//, ILogger<MetricsManagerClient> logger)
        {
            _httpClient = httpClient;
            //_logger = logger;
        }

        public AllAgentsResponse GetAllAgents()
        {
            return new AllAgentsResponse
            {
                Agents = new List<AgentDto>
                {
                    new AgentDto
                    {
                        AgentId = 1,
                        AgentAddress = "123",
                    },
                    new AgentDto
                    {
                        AgentId = 2,
                        AgentAddress = "456",
                    },
                }
            };
        }

        public AllCpuMetricResponse GetAllCpuMetrics(GetAllCpuMetricsRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var agentId = request.AgentId;
            var agentAddress = request.AgentAddress;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{agentAddress}/api/metrics/cpu/agent/{agentId}/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllCpuMetricResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }

        public AllGcHeapSizeMetricResponse GetAllGcHeapSizeMetrics(GetAllGcHeapSizeMetricsRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ManagerBaseAddress}/api/metrics/dotnet/errors-count/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllGcHeapSizeMetricResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }

        public AllHddMetricResponse GetAllHddMetrics(GetAllHddMetricsRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ManagerBaseAddress}/api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }

            return null;
        }


        public AllNetworkMetricResponse GetAllNetworkMetrics(GetAllNetworkMetricsRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ManagerBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllNetworkMetricResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricResponse GetAllRamMetrics(GetAllRamMetricsRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ManagerBaseAddress}/api/metrics/ram/avaliable/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<AllRamMetricResponse>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return null;
        }
    }
}

