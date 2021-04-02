﻿using MetricsAgent.DAL;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using MetricsAgent.Requests;
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
        public IActionResult GetMetricsByTimePeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new RamMetricsByTimePeriodResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                Time = DateTimeOffset.Parse(request.Time),
                Value = request.Value
            });
            _logger.LogInformation($"Add item. Parameters: Time = {request.Time}, Value = {request.Value}");
            return Ok();
        }
    }
}
