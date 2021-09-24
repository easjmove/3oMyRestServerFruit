using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string weatherForecastGet = "weatherForecastKey";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            IEnumerable<WeatherForecast> weatherForecastCollection = null;
            if (_memoryCache.TryGetValue(weatherForecastGet, out weatherForecastCollection))
            {
                return weatherForecastCollection;
            }

            weatherForecastCollection = CreateWeatherForecast();

            MemoryCacheEntryOptions cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
            _memoryCache.Set(weatherForecastGet, weatherForecastCollection, cacheOption);

            return weatherForecastCollection;
        }

        public IEnumerable<WeatherForecast> CreateWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
