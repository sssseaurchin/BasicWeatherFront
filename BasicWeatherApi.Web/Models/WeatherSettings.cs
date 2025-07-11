namespace BasicWeatherApi.Web.Models
{
    public class WeatherSettings
    {
        public Dictionary<string, bool> Hourly = new()
        {
            { "temperature_2m", true },
            { "relative_humidity_2m", true },
            { "apparent_temperature", true }
        };
        
        public Dictionary<string, bool> Daily = new()
        {
            { "temperature_2m_max", true },
            { "temperature_2m_min", true },
            { "sunrise,sunset", true },
            {"uv_index_max", true }
        };
    }
}