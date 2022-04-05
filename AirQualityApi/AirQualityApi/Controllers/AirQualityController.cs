using AirQualityApi;
using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using AirQualityApi.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AirQualityApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirQualityController : ControllerBase
    {
        private readonly ILogger<AirQualityController> _logger;
        BackgroundTask Background;
        public AirQualityController(ILogger<AirQualityController> logger, BackgroundTask background)
        {
            _logger = logger;
            Background = background;

        }

        [HttpGet("{city}")]
        public async Task<ActionResult<string>> Get(string city)
        {
            var _airQualityProvider = new AirQualityProvider();
            var _response = await _airQualityProvider.GetCurrentQualityAsync(city);
            if (_response.Errors != null)
                return _response.Errors[0];

            return _response.AirQuality.Quality.ToString();
        }

        [HttpPost]
        public void Post()
        {
            Background.Run();
        }
    }
}
