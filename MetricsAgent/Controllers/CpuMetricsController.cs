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
using Core;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;
        private ICpuMetricsRepository _repository;


        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в CpuMetricsController");
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/cpu/from/1970-05-20/to/2022-01-05
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная дата в формате yyyy-mm-dd</param>
        /// <param name="toTime">Конечная дата в формате yyyy-mm-dd</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параметры</response> 
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByTimePeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {   
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);
            
            var response = new CpuMetricsByTimePeriodResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            return Ok(response);
        }

        [HttpGet("/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByTimePeriodByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}, percentile = {percentile}");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<CpuMetric>(request));
            _logger.LogInformation($"Add item. Parameters: Time = {request.Time}, Value = {request.Value}");
            return Ok();
        }
    }

}

