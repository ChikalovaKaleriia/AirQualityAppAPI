﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityApi.Api
{
    public class AirQualityResponse
    {
        public AirQualityResponse(params string[] errors)
        {
            Success = !errors?.Any() ?? true;
            Errors = errors;
        }

        public AirQuality AirQuality { get; set; }

        public bool Success { get; }
        public string[] Errors { get; }
    }
}
