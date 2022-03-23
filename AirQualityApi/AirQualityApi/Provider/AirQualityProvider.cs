﻿using AirQualityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirQualityApi.Provider
{
    public class AirQualityProvider : IAirQualityProvider, IDisposable
    {
        // token that should be used via browsing the air quality via https://aqicn.org/
        private const string Token = "b7133ec0f32d99f445b882dc34df0cda71a2aa53";
        // base URL for the source site
        private const string BaseUrl = "https://api.waqi.info/feed/{1}/?token={0}";

        private HttpClient _httpClient;

        public AirQualityProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<AirQualityResponse> GetCurrentQualityAsync(string city)
        {
            // build url
            var url = string.Format(BaseUrl, Token, city);

            try
            {
                // try to get the value
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // get string value and parse
                    var stringContent = await response.Content.ReadAsStringAsync();
                    var airQualityApiResponse = JsonSerializer.Deserialize<AirQualityApiResponse>(stringContent);

                    // check response is ok
                    if (airQualityApiResponse.status != "ok")
                        return new AirQualityResponse(airQualityApiResponse.status, airQualityApiResponse.data?.ToString());

                    // try to get required air quality value
                    var airQualityData = airQualityApiResponse.data is JsonElement
                        ? JsonSerializer.Deserialize<AirQualityData>(airQualityApiResponse.data.ToString())
                        : airQualityApiResponse.data as AirQualityData;

                    var value = airQualityData?.forecast?.daily?.pm25?.FirstOrDefault(x => x.day.Date == DateTime.Today)?.avg;

                    if (value != null)
                    {
                        return new AirQualityResponse()
                        {
                            AirQuality = new AirQuality()
                            {
                                Quality = value.Value
                            }
                        };

                    }
                }
                else
                {
                    return new AirQualityResponse($"There was an error while fetching the air quality for {city}. Response StatusCode: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                return new AirQualityResponse(e.Message);
            }

            return new AirQualityResponse($"Cannot fetch the air quality for {city}");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
