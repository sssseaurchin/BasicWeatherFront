namespace BasicWeatherApi.Console
{
    public class WeatherSettings
    {
        public bool UseCurrentWeather = false;
        public bool DisplayCustomHour = true;
        
        public Dictionary<string, bool> Hourly = new()
        {
            { "temperature_2m", true },
            { "relative_humidity_2m", false },
            { "apparent_temperature", false }
        };
        
        public Dictionary<string, bool> Daily = new()
        {
            { "temperature_2m_max", false },
            { "temperature_2m_min", false },
            { "sunrise,sunset", false },
            {"uv_index_max", false }
        };
        
        /*public Dictionary<string, bool> Current = new()
        {
            { "temperature_2m", false },
            { "", true }
        }*/
    }
}