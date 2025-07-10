using System.Text.Json;

namespace BasicWeatherApi.Console
{
    public static class WeatherDataParser
    {
        public static Dictionary<string, double> ParseCurrentWeather(string json)
        {
            var result = new Dictionary<string, double>();
            var document = JsonDocument.Parse(json);
            
            if (document.RootElement.TryGetProperty("current_weather", out var current))
            {
                if (current.TryGetProperty("temperature", out var temp))
                {
                    result["temperature"] = temp.GetDouble();
                }
                /*
                if (current.TryGetProperty("windspeed", out var wind))
                {
                    result["windspeed"] = wind.GetDouble();
                }*/
            }

            return result;
        }
        public static Dictionary<string, double> ParseHourlyWeather(string json, WeatherSettings settings, int hour)
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

        public static Dictionary<string, string> ParseDailyWeather(string json, WeatherSettings settings)
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
    }
}
