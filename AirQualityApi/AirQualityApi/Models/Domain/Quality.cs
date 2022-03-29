using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Models.Domain
{
    public class Quality
    {
        /// <summary>
        /// City Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// City Air Quality
        /// </summary>
        public string AirQuality { get; set; }

    }
}
