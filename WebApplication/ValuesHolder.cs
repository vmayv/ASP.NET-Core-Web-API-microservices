using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class ValuesHolder
    {
        public List<WeatherForecast> Values { get; set; }

        public ValuesHolder()
        {
            Values = new List<WeatherForecast>();
        }
    }
}
