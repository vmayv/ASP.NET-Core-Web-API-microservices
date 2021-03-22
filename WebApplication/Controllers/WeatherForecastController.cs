using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ValuesHolder holder;
        public WeatherForecastController(ValuesHolder holder)
        {
            this.holder = holder;
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("save")]
        public IActionResult Save([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            holder.Values.Add(new WeatherForecast(date, temperatureC));
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(holder.Values);
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            List<WeatherForecast> result = new List<WeatherForecast>();

            foreach (var value in holder.Values)
            {
                if (value.Date <= toDate && value.Date >= fromDate)
                {
                    result.Add(value);
                }
            }
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            holder.Values = holder.Values.Where(w => w.Date <= toDate && w.Date >= fromDate).ToList();
            return Ok();
        }

        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int newValue)
        {
            foreach (var value in holder.Values)
            {
                if (value.Date == date)
                {
                    value.TemperatureC = newValue;
                }
            }

            return Ok();
        }

    }
}
