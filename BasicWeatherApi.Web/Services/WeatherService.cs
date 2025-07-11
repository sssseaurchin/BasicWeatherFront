using BasicWeatherApi.Web.Models;

namespace BasicWeatherApi.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly HttpClient HttpClient = new();
        
        public async Task<string> FetchWeatherJsonAsync(string url)
        {
            var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        
        public string GenerateRequestLink(City city, WeatherSettings settings)
        {
            string baseUrl = $"https://api.open-meteo.com/v1/forecast?latitude={city.Latitude}&longitude={city.Longitude}";

            List<string> hourlySelected = new List<string>();
            List<string> dailySelected = new List<string>();

            foreach (var index in settings.Hourly)
            {
                if (index.Value)
                {
                    hourlySelected.Add(index.Key);
                }
            }

            foreach (var index in settings.Daily)
            {
                if (index.Value)
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
        }
        /*hourly
            double[] temp_2m
            int[] relative_humidity
            double[] apparent_temp_2m

        daily
            double max_temp
            double min_temp
            string sunrise
            string sunset
            double uv

        current
            double[] current_temp
            // custom hour can be grabbed from ^
            vvvv change to dictionaries later?*/
        private readonly Weather _weatherList = new Weather
        {

            temp_2m = 23.00,
            relative_humidity = 56,
            apparent_temp_2m = 23.00,

            max_temp = 32.34,
            min_temp = 21.56,
            sunrise = "05:57",
            sunset = "21:02",
            uv = 5.75,

            current_temp =
            [
                21.7, 22.3, 23.0, 25.2, 25.9, 26.5, 27.2, 28.0, 30.1, 31.1, 32.34, 32.1, 31.0, 30.0, 29.0, 26.0, 25.2,
                24.0, 23.9, 23.7, 23.5, 23.0, 22.7, 21.6
            ]


        };

        private readonly City _city = new City("İzmir",38.423652,27.142797);
        public WeatherViewModel GetAllWeatherData()
        {
            return new WeatherViewModel(_weatherList, _city);
        }
        
    }
}