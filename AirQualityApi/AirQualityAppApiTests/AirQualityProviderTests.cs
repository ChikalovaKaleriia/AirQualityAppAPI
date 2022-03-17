using AirQualityApi.Api;
using System;
using Xunit;

namespace AirQualityAppApiTests
{
    public class AirQualityProviderTests
    {
        AirQualityProvider airQualityProvider;

        public AirQualityProviderTests()
        {
            airQualityProvider = new AirQualityProvider();
        }

        [Fact]
        public async void GetCurrentQualityAsync_Dnipr_Error()
        {
            // Arrange
            string city = "Dnipr";
            string[] error = new string[2] {"error", "Unknown station" };

            //Act
            var result = await airQualityProvider.GetCurrentQualityAsync(city);

            //Assert
            Assert.Equal(error, result.Errors);
        }

        [Fact]
        public async void GetCurrentQualityAsync_Numbers_Error()
        {
            // Arrange
            string city = "123ff4";
            string[] error = new string[2] { "error", "Unknown station" };

            //Act
            var result = await airQualityProvider.GetCurrentQualityAsync(city);

            //Assert
            Assert.Equal(error, result.Errors);

        }

        [Fact]
        public async void GetCurrentQualityAsync_Berlin_Success()
        {
            // Arrange
            string city = "Berlin";

            //Act
            var result = await airQualityProvider.GetCurrentQualityAsync(city);

            //Assert
            Assert.NotNull(result.AirQuality.Quality.ToString());

        }
    }
}
