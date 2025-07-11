using System.Text.Json;
using BasicWeatherApi.Web.Models;

namespace BasicWeatherApi.Web.Services
{
    public static class WeatherDataParser
    {
        private static double? ParseCurrentWeather(string json)
        {
            var document = JsonDocument.Parse(json);
            
            if (document.RootElement.TryGetProperty("current_weather", out var current))
            {
                if (current.TryGetProperty("temperature", out var temp))
                {
                    return temp.GetDouble();
                }
            }
            return null;
        }
        private static Dictionary<string, double> ParseHourlyWeather(string json, WeatherSettings settings, int hour)
        {
            var result = new Dictionary<string, double>();
            var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty("hourly", out JsonElement hourly)) return result;

            foreach (var item in settings.Hourly)
            {
                if (item.Value == true) 
                {
                    string key = item.Key;

                    if (hourly.TryGetProperty(key, out JsonElement dataArray))
                    {
                        if (dataArray.GetArrayLength() > hour)
                        {
                            double value = dataArray[hour].GetDouble();
                            result[key] = value;
                        }
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, string> ParseDailyWeather(string json, WeatherSettings settings)
        {
            var result = new Dictionary<string, string>();
            var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty("daily", out var daily)) return result;

            if (daily.TryGetProperty("sunrise", out var sunrises) && daily.TryGetProperty("sunset", out var sunsets))
            {
                if (sunrises.GetArrayLength() > 0 &&  sunsets.GetArrayLength() > 0)
                {
                    // change later vvv
                    var sunriseTime = sunrises[0].GetString()!.Substring(11,5);
                    var sunsetTime = sunsets[0].GetString()!.Substring(11,5);
                    // change later ^^^
                    
                    result["sunrise"] = sunriseTime ?? "N/A";
                    result["sunset"] = sunsetTime ?? "N/A";
                }
            }
            
            foreach (var item in settings.Daily)
            {
                if (item.Value == true) 
                {
                    string key = item.Key;

                    if (daily.TryGetProperty(key, out var dataArray))
                    {
                        if (dataArray.GetArrayLength() > 0)
                        {
                            double value = dataArray[0].GetDouble();
                            result[key] = value.ToString();
                        }
                    }
                }
            }

            return result;
        }

        public static Weather AdaptToModel(string json, WeatherSettings settings, int hour)
        {
            var model = new Weather();

            model.current_temp = ParseCurrentWeather(json);

            // Hourly data
            var hourly = ParseHourlyWeather(json, settings, hour);
            var fullHourly = JsonDocument.Parse(json)
                .RootElement.GetProperty("hourly");
            
            if (fullHourly.TryGetProperty("relative_humidity_2m", out var humidityArray))
            {
                model.relative_humidity = humidityArray.EnumerateArray()
                    .Select(e => e.GetInt32())
                    .ToArray();
            }

            if (fullHourly.TryGetProperty("apparent_temperature", out var apparentArray))
            {
                model.apparent_temp_2m = apparentArray.EnumerateArray()
                    .Select(e => e.GetDouble())
                    .ToArray();
            }

            model.temp_2m = fullHourly.GetProperty("temperature_2m").EnumerateArray()
                .Select(e => e.GetDouble())
                .ToArray();

            var daily = ParseDailyWeather(json, settings);
            if (daily.TryGetValue("temperature_2m_min", out var min))
                model.min_temp = double.Parse(min);

            if (daily.TryGetValue("temperature_2m_max", out var max))
                model.max_temp = double.Parse(max);

            if (daily.TryGetValue("uv_index_max", out var uv))
                model.uv = double.Parse(uv);

            if (daily.TryGetValue("sunrise", out var sr))
                model.sunrise = sr;

            if (daily.TryGetValue("sunset", out var ss))
                model.sunset = ss;

            return model;
        }
    }
}
