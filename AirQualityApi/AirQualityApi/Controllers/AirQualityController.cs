using AirQualityApi.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirQualityController : ControllerBase
    {
        private readonly ILogger<AirQualityController> _logger;

        public AirQualityController(ILogger<AirQualityController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<int>> Get(string city)
        {
            var airQualityProvider = new AirQualityProvider();
            var response = await airQualityProvider.GetCurrentQualityAsync(city);
            return response.AirQuality.Quality;
        }
       
    }
}
