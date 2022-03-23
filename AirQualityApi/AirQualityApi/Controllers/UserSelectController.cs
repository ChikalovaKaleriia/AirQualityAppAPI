using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserSelectController : ControllerBase
    {
        #region Collection UserSelection
        private static string databaseName = "AirQualityApp";
        private static string UserSelectcollectionName = "UserSelect";

        public static MongoClient client = new MongoClient(Connector.MongoDBConnectionString);
        public static IMongoDatabase db = client.GetDatabase(databaseName);
        public static IMongoCollection<UserSelection> UserSelectCollection = db.GetCollection<UserSelection>(UserSelectcollectionName);
        #endregion

        private readonly ILogger<UserSelectController> _logger;

        public UserSelectController(ILogger<UserSelectController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{id}")]
        public async Task Post(string id)
        {
            var record = new UserSelection { Id = id };
            await UserSelectCollection.InsertOneAsync(record);
        }
    }
}
