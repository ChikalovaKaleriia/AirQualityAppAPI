using AirQualityApi.Models.Domain;
using AirQualityApi.Provider;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQualityApi.Models;
using System.Timers;
using AirQualityApi.WorkWithDB;

namespace AirQualityApi
{
    public class BackgroundTask
    {
        /// <summary>
        /// Const for Task.Delay
        /// </summary>
        private const int TIMER = 10000;

        public static bool IsStarted { get; set; }

        public Task backgroundTask = new Task(async () => {

            IsStarted = true;
            //All records from UserSelect collection
            var idsFromUserSelectedDb = DB.UserSelectCollection.Find(_ => true).ToList();

            //All records from City collection
            var allCities = DB.collectionCity.Find(_ => true).ToList();

            //Quality List for Id and AirQuality
            List<Quality> airQualityForStatistic = new List<Quality>();
            var _airQualityProvider = new AirQualityProvider();


            while (true)
            {
                foreach (var ids in idsFromUserSelectedDb)
                {
                    //City with ids id
                    var city = allCities.FirstOrDefault(x => x.Id == ids.Id);

                    var _response = await _airQualityProvider.GetCurrentQualityAsync(city.Name);

                    //response with AirQuality
                    int thisQuality = _response.AirQuality.Quality;

                    //Adding new Quality to airQualityForStatistic List
                    airQualityForStatistic.Add(new Quality { IdCity = city.Id, AirQuality = thisQuality });
                }
                //Inserting airQualityForStatistic List to AirQuality collection
                DB.AirQualityCollection.InsertMany(airQualityForStatistic);
                await Task.Delay(TIMER);
            }
        });
    }
}
