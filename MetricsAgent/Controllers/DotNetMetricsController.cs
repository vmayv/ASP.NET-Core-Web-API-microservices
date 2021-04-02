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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в DotNetMetricsController");
            _repository = repository;
        }

        [HttpGet("/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountMetricsByTimePeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new DotNetMetricsByTimePeriodResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"Add item. Parameters: Time = {request.Time}, Value = {request.Value}");
            return Ok();
        }
    }
}
