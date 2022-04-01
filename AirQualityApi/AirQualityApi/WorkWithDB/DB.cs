using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace AirQualityApi.WorkWithDB
{
    public class DB : IDB
    {

        #region CityCollection

        private static string databaseNameCity = "AirQualityApp";
        private static string collectionNameCity = "City";

        public static MongoClient clientCity = new MongoClient(Connector.MongoDBConnectionString);
        public static IMongoDatabase dbCity = clientCity.GetDatabase(databaseNameCity);
        public static IMongoCollection<City> collectionCity = dbCity.GetCollection<City>(collectionNameCity);

        #endregion

        #region UserSelectCollection

        private static string databaseName = "AirQualityApp";
        private static string UserSelectcollectionName = "UserSelect";

        public static MongoClient client = new MongoClient(Connector.MongoDBConnectionString);
        public static IMongoDatabase db = client.GetDatabase(databaseName);
        public static IMongoCollection<UserSelection> UserSelectCollection = db.GetCollection<UserSelection>(UserSelectcollectionName);

        #endregion

        #region AirQualityCollection

        private static string databaseAirQualityName = "AirQualityApp";
        private static string AirQualityCollectionName = "AirQuality";

        public static MongoClient clientAQ = new MongoClient(Connector.MongoDBConnectionString);
        public static IMongoDatabase dbAQ = client.GetDatabase(databaseAirQualityName);
        public static IMongoCollection<Quality> AirQualityCollection = dbAQ.GetCollection<Quality>(AirQualityCollectionName);

        #endregion

        #region Get-Methods
        public async Task<List<UserSelection>> GetSelectedCities()
        {
            //All fecords from UserSelected collection
            List<UserSelection> SelectedCities = await UserSelectCollection.Find(_ => true).ToListAsync();
            return  SelectedCities;
        }
        
        public async Task<List<City>> GetAllCities()
        {
            //All records from City collection
            List<City> AllCities = await collectionCity.Find(_ => true).ToListAsync();
            return AllCities;
        }

        public List<Quality> GetAllQuality(BsonDocument filter)
        {
            //All records from AirQuality collection
            List<Quality> allQuality =  AirQualityCollection.Find(filter).ToList();
            return allQuality;
        }
        #endregion
    }
}
