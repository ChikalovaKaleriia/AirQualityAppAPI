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
            var _airQualityProvider = new AirQualityProvider();
            var _response = await _airQualityProvider.GetCurrentQualityAsync(city);
            return _response.AirQuality.Quality;
        }
    }
}
