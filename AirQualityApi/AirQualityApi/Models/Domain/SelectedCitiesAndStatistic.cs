using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Models.Domain
{
    public class SelectedCitiesAndStatistic
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StringStatistic { get; set; }
        public string Average { get; set; }
    }
}
