using Core.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using MetricsManager.Responses;
using MetricsManager.DTO;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;
        private readonly IMapper _mapper;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogInformation(1, "NLog встроен в AgentsController");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentAddress)
{
            _repository.RegisterAgent(new AgentInfo { AgentAddress = agentAddress.AgentAddress });
            _logger.LogInformation($"Parameters: agentAddress = {agentAddress}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Parameters: agentId = {agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Parameters: agentId = {agentId}");
            return Ok();
        }

        [HttpGet("getagentslist")]
        public IActionResult GetAgentsList()
        {
            var agentList = _repository.GetAgentList();
            var response = new AllAgentsApiResponse()
            {
                Agents = new List<AgentDto>()
            };

            foreach (var agent in agentList)
            {
                response.Agents.Add(_mapper.Map<AgentDto>(agent));
            }
            return Ok(response);
        }

    }
}
