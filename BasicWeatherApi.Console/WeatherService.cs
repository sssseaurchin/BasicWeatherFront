using System.Net.Http;
using System.Text.Json;

namespace BasicWeatherApi.Console
{
    public static class WeatherService
    {
        private static readonly HttpClient HttpClient = new();

        /*public static async Task<List<double>> GetHourlyTemperaturesAsync(City city)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={city.Latitude}&longitude={city.Longitude}&hourly=temperature_2m&timezone=auto&forecast_days=1";
            var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(content);
            
            var temps = root.RootElement.GetProperty("hourly").GetProperty("temperature_2m");

            List<double> temperatureList = [];
            foreach (var element in temps.EnumerateArray())
            {
                double temp = element.GetDouble();
                temperatureList.Add(temp);
            }

            return temperatureList;
        }*/
        public static async Task<string> FetchWeatherJsonAsync(string url)
        {
            var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> GenerateRequestLink(City city, WeatherSettings settings)
        {
            string baseUrl = $"https://api.open-meteo.com/v1/forecast?latitude={city.Latitude}&longitude={city.Longitude}";

            List<string> hourlySelected = new List<string>();
            List<string> dailySelected = new List<string>();

            foreach (var index in settings.Hourly)
            {
                if (index.Value == true)
                {
                    hourlySelected.Add(index.Key);
                }
            }

            foreach (var index in settings.Daily)
            {
                if (index.Value == true)
                {
                    dailySelected.Add(index.Key);
                }
            }
            
            if (hourlySelected.Count > 0)
            {
                baseUrl += $"&hourly={string.Join("%2C", hourlySelected)}";
            }

            if (dailySelected.Count > 0)
            {
                baseUrl += $"&daily={string.Join("%2C", dailySelected)}";
            }

            if (settings.UseCurrentWeather)
                baseUrl += "&current_weather=true";

            baseUrl += "&timezone=auto";
            return baseUrl;
            // HttpResponseMessage response = await HttpClient.GetAsync(baseUrl);
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadAsStringAsync();
        }

    }
}