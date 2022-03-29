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

namespace AirQualityApi
{
    public class DB
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

    }
}
