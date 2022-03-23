using AirQualityApi.Models;
using AirQualityApi.Models.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AirQualityApi
{
    public class DB
    {

        #region Private
        /// <summary>
        /// Connection string to server
        /// </summary>
        private static string _connectionString = Connector.MongoDBConnectionString;

        /// <summary>
        /// Name of database
        /// </summary>
        private static string _databaseName = "AirQualityApp";

        /// <summary>
        /// Name of collection
        /// </summary>
        private static string _collectionName = "Cities";
        #endregion

        /// <summary>
        /// Connection to server
        /// </summary>
        public static MongoClient client = new MongoClient(_connectionString);

        /// <summary>
        /// Creation of database
        /// </summary>
        public static IMongoDatabase db = client.GetDatabase(_databaseName);

        /// <summary>
        /// Http client for connection to API
        /// </summary>
        HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Creation of collection
        /// </summary>
        public static IMongoCollection<City> collection = db.GetCollection<City>(_collectionName);

        #region Methods
        public async void TimerSearch()
        {
           
            //foreach (var sc in SelectedCities)
            //{
            //    // url for connecting to API
            //    string url = "https://localhost:44387/airquality/" + sc.Name;

            //    // Getting the response from API
            //    HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

            //    if (responseMessage.IsSuccessStatusCode)
            //    {
            //Creation of new record for database
            var record = new City { Name = "Athens"};

            //Insert of this record to database
            await collection.InsertOneAsync(record);
            //    }
            //}
        }
        #endregion
    }
}
