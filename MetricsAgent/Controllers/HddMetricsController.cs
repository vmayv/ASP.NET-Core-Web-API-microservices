using MetricsAgent.DAL;
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
            return Ok();
        }

        [HttpGet("/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new CpuMetricsByTimePeriodResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
