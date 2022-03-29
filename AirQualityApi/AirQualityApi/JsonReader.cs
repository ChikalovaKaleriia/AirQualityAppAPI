using AirQualityApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirQualityApi
{
    public class JsonReader
    {
        public static async Task<ObservableCollection<City>> JsonReadAsync()
        {
            string text = "";
            using (StreamReader reader = new StreamReader("Cities.json"))
            {
                text = await reader.ReadToEndAsync();
            }
            ObservableCollection<City> citiesFromJson = JsonSerializer.Deserialize<ObservableCollection<City>>(text);
            return citiesFromJson;
        }
    }
}
