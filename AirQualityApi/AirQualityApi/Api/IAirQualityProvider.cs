using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Api
{
    public interface IAirQualityProvider
    {
        Task<AirQualityResponse> GetCurrentQualityAsync(string city);
    }
}
