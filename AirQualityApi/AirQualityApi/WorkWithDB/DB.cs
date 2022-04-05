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
        static Connector connector = Connector.GetInstance();
        #region Names

        private const string databaseName = "AirQualityApp";
        private const string collectionNameCity = "City";
        private const string UserSelectcollectionName = "UserSelect";
        private const string AirQualityCollectionName = "AirQuality";
        #endregion

        #region Client and Database
        public static MongoClient client = new MongoClient(connector.MongoDBConnectionString);
        public static IMongoDatabase db = client.GetDatabase(databaseName);
        #endregion

        #region Collections
        public IMongoCollection<City> collectionCity = db.GetCollection<City>(collectionNameCity);

        public IMongoCollection<UserSelection> UserSelectCollection = db.GetCollection<UserSelection>(UserSelectcollectionName);

        public IMongoCollection<Quality> AirQualityCollection = db.GetCollection<Quality>(AirQualityCollectionName);
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
