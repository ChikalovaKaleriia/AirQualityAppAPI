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
        private const string IdOfAthens = "623a13f2a4c14edcb3808a06";
        private  BsonDocument filter = new BsonDocument("IdCity", IdOfAthens);


        public GetStatisticTests()
        {
            _idb = Substitute.For<IDB>();
            _idb.GetSelectedCities().Returns( new List<UserSelection> { new UserSelection { Id = IdOfAthens } });
            _idb.GetAllCities().Returns(new List<City> { new City { Id = IdOfAthens, Name = "Athens" } });
            getStatistic = new GetStatistic(_idb);
        }

        [Fact]
        public async void GetInfo_IsUnstable_True()
        {
            //arrange
            _idb.GetAllQuality(filter).Returns(new List<Quality> { new Quality { IdCity = IdOfAthens, AirQuality = 90 }, 
                new Quality { IdCity = IdOfAthens, AirQuality = 80 },
                new Quality { IdCity = IdOfAthens, AirQuality = 70 } });

            //act
            var result = await getStatistic.GettingInfo();

            //assert 
            Assert.Equal("Air Quality is unstable", result[0].StringStatistic);

        }

        [Fact]
        public async void GetInfo_IsStable_True()
        {
            //arrange
            _idb.GetAllQuality(filter).Returns(new List<Quality> { new Quality { IdCity = IdOfAthens, AirQuality = 90 },
                new Quality { IdCity = IdOfAthens, AirQuality = 90 },
                new Quality { IdCity = IdOfAthens, AirQuality = 90 } });

            //act
            var result = await getStatistic.GettingInfo();

            //assert 
            Assert.Equal("Air Quality is stabile", result[0].StringStatistic);

        }
    }
}
