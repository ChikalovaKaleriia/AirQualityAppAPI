using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Models.Domain
{
    public class Quality
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// City Id
        /// </summary>
        public string IdCity { get; set; }

        /// <summary>
        /// City Air Quality
        /// </summary>
        public int AirQuality { get; set; }

    }
}
