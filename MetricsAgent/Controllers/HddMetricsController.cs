using MetricsAgent.DAL;
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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в HddMetricsController");
            _repository = repository;
        }

        [HttpGet("/left/")]
        public IActionResult GetHddLeft()
        {
            _logger.LogInformation($"GET");
            var metrics = _repository.GetLast();

            var response = new HddMetricsGetLastResponse()
            {
                Id = metrics.Id,
                Value = metrics.Value,
                Time = metrics.Time
            };

            return Ok(response);
        }

        [HttpGet("/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new HddMetricsByTimePeriodResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetric
            {
                Time = DateTimeOffset.Parse(request.Time),
                Value = request.Value
            });

            return Ok();
        }
    }
}
