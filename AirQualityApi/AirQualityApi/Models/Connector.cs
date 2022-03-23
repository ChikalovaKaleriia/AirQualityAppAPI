using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace AirQualityApi.Models
{
    public class Connector
    {
        public static string MongoDBConnectionString = ConfigurationManager.ConnectionStrings["MongoDBConnectionString"].ConnectionString;
    }
}
