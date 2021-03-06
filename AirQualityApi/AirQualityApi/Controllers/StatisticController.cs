using AirQualityApi.Models.Domain;
using AirQualityApi.WorkWithDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace AirQualityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly ILogger<StatisticController> _logger;
        public StatisticController(ILogger<StatisticController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public async Task<ObservableCollection<SelectedCitiesAndStatistic>> GetName()
        {
            IDB _db = new DB();
            GetStatistic getStatistic = new GetStatistic(_db);

            ObservableCollection<SelectedCitiesAndStatistic> statistic =  await getStatistic.GettingInfo();
            return statistic;
        }
    }
}
