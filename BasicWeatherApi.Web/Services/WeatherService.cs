using BasicWeatherApi.Web.Models;

namespace BasicWeatherApi.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly HttpClient HttpClient = new();
        public required WeatherSettings Settings { get; set; } = new ();
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


            baseUrl += "&current_weather=true";

            baseUrl += "&timezone=auto";
            return baseUrl;
        }

        private readonly City _city = new City("İzmir",38.423652,27.142797);
        public async Task<WeatherViewModel> GetAllWeatherDataAsync(City city)
        {
            city = city ?? _city;
            string json = await FetchWeatherJsonAsync(GenerateRequestLink(city, Settings));
            Weather weather = WeatherDataParser.AdaptToModel(json, Settings, DateTime.Now.Hour);
            return new WeatherViewModel(weather, city);
        }
    }
}