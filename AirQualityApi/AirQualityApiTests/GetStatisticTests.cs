using AirQualityApi;
using AirQualityApi.Models.Domain;
using AirQualityApi.WorkWithDB;
using MongoDB.Bson;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AirQualityApiTests
{
    public class GetStatisticTests
    {
        private readonly IDB _idb;
        private readonly GetStatistic getStatistic;

        public GetStatisticTests()
        {
            _idb = Substitute.For<IDB>();
            _idb.GetSelectedCities().Returns( new List<UserSelection> { new UserSelection { Id = "623a13f2a4c14edcb3808a06" } });
            _idb.GetAllCities().Returns(new List<City> { new City { Id = "623a13f2a4c14edcb3808a06", Name = "Athens" } });
            getStatistic = new GetStatistic(_idb);
        }

        [Fact]
        public async void GetInfo_90_80_70_True()
        {
            //arrange
            BsonDocument filter = new BsonDocument("IdCity", "623a13f2a4c14edcb3808a06");
            _idb.GetAllQuality(filter).Returns(new List<Quality> { new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 90 }, 
                new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 80 },
                new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 70 } });

            //act
            var result = await getStatistic.GettingInfo();

            //assert 
            Assert.Equal("Air Quality is unstable", result[0].StringStatistic);

        }

        [Fact]
        public async void GetInfo_90_90_90_True()
        {
            //arrange
            BsonDocument filter = new BsonDocument("IdCity", "623a13f2a4c14edcb3808a06");
            _idb.GetAllQuality(filter).Returns(new List<Quality> { new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 90 },
                new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 90 },
                new Quality { IdCity = "623a13f2a4c14edcb3808a06", AirQuality = 90 } });

            //act
            var result = await getStatistic.GettingInfo();

            //assert 
            Assert.Equal("Air Quality is stabile", result[0].StringStatistic);

        }
    }
}
