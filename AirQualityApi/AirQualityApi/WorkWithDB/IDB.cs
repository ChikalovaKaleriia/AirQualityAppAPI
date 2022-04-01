using AirQualityApi.Models.Domain;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.WorkWithDB
{
    public interface IDB
    {
        public  Task<List<UserSelection>> GetSelectedCities();
        public  Task<List<City>> GetAllCities();
        public List<Quality> GetAllQuality(BsonDocument filter);
    }
}
