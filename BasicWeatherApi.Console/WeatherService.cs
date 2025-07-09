using System.Net.Http;
using System.Text.Json;

namespace BasicWeatherApi.Console
{
    public static class WeatherService
    {
        private static readonly HttpClient HttpClient = new();

        public static async Task<List<double>> GetHourlyTemperaturesAsync(City city)
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
        }
    }
}