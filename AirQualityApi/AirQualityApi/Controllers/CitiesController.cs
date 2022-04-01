using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using AirQualityApi.WorkWithDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirQualityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]  
        public async Task<List<City>> Get()
        {
            var cities = await DB.collectionCity.Find(_ => true).ToListAsync();
            if (cities != null)
            {
                return cities;
            }

            else
            {
                ObservableCollection<City> citiesFromJson = await JsonReader.JsonReadAsync();
                DB.collectionCity.InsertMany(citiesFromJson);
                cities = await DB.collectionCity.Find(_ => true).ToListAsync();
                if (cities != null)
                {
                    return cities;
                }
            }
            return null;
        }

    }
}
