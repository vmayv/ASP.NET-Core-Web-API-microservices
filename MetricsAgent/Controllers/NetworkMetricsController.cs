﻿using MetricsAgent.DAL;
using MetricsAgent.DTO;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в NetworkMetricsController");
            _repository = repository;
        }

        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new NetworkMetricsByTimePeriodResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
