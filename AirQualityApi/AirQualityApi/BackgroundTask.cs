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

        // all available cities (list cannot be changed by user)
        private List<City> _allCities;
        private List<UserSelection> _selectedCities;
        private DB db = new DB();

        public bool IsStarted { get; private set; }

        public void Run()
        {
            // make sure keeping statistic is not already started
            if (IsStarted) return;

            // run untracked task
            // NOTE: usually tasks are controlled somehow.
            // Now we can leave it as it is, but be careful with such using later
            new Task(async () => await KeepStatistic()).Start();
        }

        public async void RefreshSelectedCitiesList()
        {
            // refresh list only in case when task is working
            if (!IsStarted) return;

            // selected cities for keeping statistic
            _selectedCities = await db.GetSelectedCities();
        }

        private async Task KeepStatistic()
        {
            // make sure keeping statistic is not already started
            if (IsStarted) return;

            // keeping city statistic is started
            IsStarted = true;

            // all existing cities
            _allCities = await db.GetAllCities();

            // load selected cities list
            RefreshSelectedCitiesList();

            // air quality per city
            var airQualityForStatistic = new List<Quality>();
            var airQualityProvider = new AirQualityProvider();

            // endless loop for keeping statistic 
            while (true)
            {
                foreach (var ids in _selectedCities)
                {
                    // load selected cities list
                    RefreshSelectedCitiesList();
                    // get city details
                    var city = _allCities.FirstOrDefault(x => x.Id == ids.Id);

                    // get air quality of the city
                    var response = await airQualityProvider.GetCurrentQualityAsync(city.Name);

                    // add quality to the result list
                    airQualityForStatistic.Add(new Quality { IdCity = city.Id, AirQuality = response.AirQuality.Quality });
                }

                // save air quality per city into the database
                await db.AirQualityCollection.InsertManyAsync(airQualityForStatistic);
                // cleanup collection after saving the data
                airQualityForStatistic.Clear();
                // wait some time before the next loop iteration
                await Task.Delay(TIMER);
            }
        }
    }
}
