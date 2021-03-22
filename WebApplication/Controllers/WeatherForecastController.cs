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
        private readonly ValuesHolder _holder;
        public WeatherForecastController(ValuesHolder holder)
        {
            this._holder = holder;
        }

        [HttpPost("save")]
        public IActionResult Save([FromQuery] string date, [FromQuery] int temperatureC)
        {
            _holder.Values.Add(new WeatherForecast(Convert.ToDateTime(date), temperatureC));
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            List<WeatherForecast> result = new List<WeatherForecast>();

            foreach (var value in _holder.Values)
            {
                if (value.Date <= Convert.ToDateTime(toDate) && value.Date >= Convert.ToDateTime(fromDate))
                {
                    result.Add(value);
                }
            }
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            _holder.Values = _holder.Values.Where(w => !(w.Date <= Convert.ToDateTime(toDate) && w.Date >= Convert.ToDateTime(fromDate))).ToList();
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string date, [FromQuery] int newValue)
        {
            foreach (var value in _holder.Values)
            {
                if (value.Date == Convert.ToDateTime(date))
                {
                    value.TemperatureC = newValue;
                }
            }
            return Ok();
        }

    }
}
