using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class ForecastDTO
    {
        public string Icon { get; set; }
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public string Date { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
    }
}
