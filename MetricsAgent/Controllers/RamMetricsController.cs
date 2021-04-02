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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в RamMetricsController");
            _repository = repository;
        }

        [HttpGet("/avaliable/")]
        public IActionResult GetAvailableRam()
        {
            _logger.LogInformation($"GET");
            var metrics = _repository.GetLast();

            var response = new RamMetricsGetLastResponse()
            {
                Id = metrics.Id,
                Value = metrics.Value,
                Time = metrics.Time
            };

            return Ok(response);

        }

        [HttpGet("/avaliable/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new RamMetricsByTimePeriodResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
