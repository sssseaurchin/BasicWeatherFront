namespace BasicWeatherApi.Web.Models
{
    public class WeatherViewModel(Weather weather, City city)
    {
        public Weather Weather { get; set; } = weather;
        public City City { get; set; } = city;
    }

}