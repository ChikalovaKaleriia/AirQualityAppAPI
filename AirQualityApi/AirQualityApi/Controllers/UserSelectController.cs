using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using AirQualityApi.WorkWithDB;

namespace AirQualityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserSelectController : ControllerBase
    {
        private readonly ILogger<UserSelectController> _logger;
        BackgroundTask Background;
        DB db = new DB();
        public UserSelectController(ILogger<UserSelectController> logger, BackgroundTask background)
        {
            _logger = logger;
            Background = background;
        }

        [HttpGet]
        public async Task<ObservableCollection<string>> Get()
        {
            ObservableCollection<string> selectedCities = new ObservableCollection<string>();
            var Cities = await db.UserSelectCollection.Find(_ => true).ToListAsync();
            if (Cities != null)
            {
                foreach (var c in Cities)
                {
                    selectedCities.Add(c.Id);
                }
                return selectedCities;
            }
            return null;
        }

        [HttpPost("{Id}")]
        public async Task Post(string Id)
        {
            var filter = new BsonDocument("Id", Id);
            var record = new UserSelection { Id = Id };
            if (db.UserSelectCollection.Find(filter).CountDocuments() == 0)
                await db.UserSelectCollection.InsertOneAsync(record);

        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var filter = new BsonDocument("_id", id);
            if (db.UserSelectCollection.Find(filter).CountDocuments() != 0)
                await db.UserSelectCollection.DeleteOneAsync(filter);
        }

    }
}
