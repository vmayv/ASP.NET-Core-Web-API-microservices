using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using MetricsManager.DAL.Repositories;
using AutoMapper;
using MetricsManager.Responses;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsApiRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsApiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в CpuMetricsController");
        }
        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени от заданного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/cpu/agent/8/from/1970-05-20/to/2022-01-05
        ///
        /// </remarks>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="fromTime">Начальная дата в формате yyyy-mm-dd</param>
        /// <param name="toTime">Конечная дата в формате yyyy-mm-dd</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени для заданного агента</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параметры</response>  
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetMetricFromDatabase(agentId, fromTime, toTime);

            var response = new AllCpuMetricApiResponse()
            {
                Metrics = new List<CpuMetricApiDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricApiDto>(metric));
            }
            _logger.LogInformation($"Parameters: agentId = {agentId}, fromTime = {fromTime}, toTime = {toTime}");
            return Ok(response);
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Parameters: agentId = {agentId}, fromTime = {fromTime}, toTime = {toTime}, percentile = {percentile}");
            return Ok();
        }
        /*
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Parameters: fromTime = {fromTime}, toTime = {toTime}, percentile = {percentile}");
            return Ok();
        }*/
    }
}
