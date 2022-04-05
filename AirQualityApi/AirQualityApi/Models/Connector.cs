using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace AirQualityApi.Models
{
    public class Connector
    {
        private static Connector instance;
        /// <summary>
        /// Connection string to API 
        /// </summary>
        public string MongoDBConnectionString { get; private set; }
        private Connector()
        {
            MongoDBConnectionString = ConfigurationManager.ConnectionStrings["MongoDBConnectionString"].ConnectionString;
        }
        public static Connector GetInstance()
        {
            if (instance == null)
                instance = new Connector();
            return instance;
        }
    }
}
