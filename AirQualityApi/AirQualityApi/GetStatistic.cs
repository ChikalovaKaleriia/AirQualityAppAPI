using AirQualityApi.Models.Domain;
using AirQualityApi.WorkWithDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi
{
    public class GetStatistic
    {
        public IDB db;

        public GetStatistic(IDB _db)
        {
            db = _db;
        }
        public async Task<ObservableCollection<SelectedCitiesAndStatistic>> GettingInfo()
        {
            //Result 
            ObservableCollection<SelectedCitiesAndStatistic> SelectedCityInfo = new ObservableCollection<SelectedCitiesAndStatistic>();

            //All fecords from UserSelected collection
            var SelectedCities = await db.GetSelectedCities();

            //All records from City collection
            var AllCities = await db.GetAllCities();

            // List for resulf from GettingStatistic method
            List<string> resultStatistic = new List<string>();

            foreach (var selcit in SelectedCities)
            {
                //City, that have save Id
                var city = AllCities.FirstOrDefault(x => x.Id == selcit.Id);
                
                resultStatistic = GettingStatistic(city.Id);

                //Result for city
                SelectedCityInfo.Add(new SelectedCitiesAndStatistic { Id = city.Id, Name = city.Name,
                    StringStatistic = resultStatistic[0], Average = resultStatistic[1] });
            }

            return SelectedCityInfo;

        }

        public  List<string> GettingStatistic(string id)
        {
            //Const description of statistic
            const string UNSTABLE = "Air Quality is unstable";
            const string STABILE = "Air Quality is stabile";

            //Result list
            List<string> statisticInfo = new List<string>();
            List<int> quality = new List<int>();

            //Sum of the Air Quality
            int SumOfQuality = 0;
            var filter = new BsonDocument("IdCity", id);

            //All records from AirQuality collection
            var allQuality = db.GetAllQuality(filter);

            //Count of records in allQuality
            var count = allQuality.Count();

            foreach (var al in allQuality)
            {
                SumOfQuality += al.AirQuality;
                quality.Add(al.AirQuality);
            }

            //Min value of Quality
            int min = quality.Min();

            //Maw value of the Quality
            int max = quality.Max();
            
            if(min == max) statisticInfo.Add(STABILE); 

            else statisticInfo.Add(UNSTABLE);

            //Adding the average of Air Quality
            statisticInfo.Add((SumOfQuality / count).ToString());

            return statisticInfo;
        }
    }
}
