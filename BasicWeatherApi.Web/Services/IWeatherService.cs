using BasicWeatherApi.Web.Models;

namespace BasicWeatherApi.Web.Services
{
    public interface IWeatherService
    {
        string GenerateRequestLink(City city, WeatherSettings settings);
        Task<string> FetchWeatherJsonAsync(string url);
        Task<WeatherViewModel> GetAllWeatherDataAsync();
    }
}