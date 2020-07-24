using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHub.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async IAsyncEnumerable<WeatherForecast> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();

            foreach (var index in Enumerable.Range(1, 10))
            {
                var weatherForecast = new WeatherForecast
                {
                    Date = startDate.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };

                await Task.Delay(100);
                yield return weatherForecast;
            }
        }
    }
}
