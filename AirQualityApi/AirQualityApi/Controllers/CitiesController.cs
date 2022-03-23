using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
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
        #region Collection City
        private static string databaseNameCity = "AirQualityApp";
        private static string collectionNameCity = "City";

        public static MongoClient clientCity = new MongoClient(Connector.MongoDBConnectionString);
        public static IMongoDatabase dbCity = clientCity.GetDatabase(databaseNameCity);
        public static IMongoCollection<City> collectionCity = dbCity.GetCollection<City>(collectionNameCity);
        #endregion

        private readonly ILogger<CitiesController> _logger;
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<City>> Get()
        {
            var filter = new BsonDocument();
            var cities = await collectionCity.Find(filter).ToListAsync();
            if (cities != null)
            {
                return cities;
            }

            else
            {
                string text = "";
                using (StreamReader reader = new StreamReader("Cities.json"))
                {
                    text = await reader.ReadToEndAsync();
                }
                ObservableCollection<City> citiesFromJson = JsonSerializer.Deserialize<ObservableCollection<City>>(text)!;
                collectionCity.InsertMany(citiesFromJson);
                cities = await collectionCity.Find(filter).ToListAsync();
                if (cities != null)
                {
                    return cities;
                }
            }
            return null;

        }

    }
}
