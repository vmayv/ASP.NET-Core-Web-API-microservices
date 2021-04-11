using AutoMapper;
using MetricsAgent.DAL.Repositories;
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
        private readonly IMapper _mapper;
        private IRamMetricsRepository _repository;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в RamMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("avaliable/")]
        public IActionResult GetAvaliableRam()
        {
            var metrics = _repository.GetLast();

            var response = _mapper.Map<RamMetricsGetLastResponse>(metrics);

            _logger.LogInformation($"GET");
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
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<RamMetric>(request));
            _logger.LogInformation($"Add item. Parameters: Time = {request.Time}, Value = {request.Value}");
            return Ok();
        }
    }
}
