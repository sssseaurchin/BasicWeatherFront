namespace BasicWeatherApi.Console
{
    static class SettingsEditor
    {
        public static WeatherSettings GetWeatherSettingsFromUser()
        {
            WeatherSettings settings = new();

            Dictionary<string, (string label, Action toggle)> options = new()
            {
                ["1.a"] = ("Temperature", () => settings.Hourly["temperature_2m"] = !settings.Hourly["temperature_2m"]),
                ["1.b"] = ("Relative Humidity", () => settings.Hourly["relative_humidity_2m"] = !settings.Hourly["relative_humidity_2m"]),
                ["1.c"] = ("Apparent Temperature", () => settings.Hourly["apparent_temperature"] = !settings.Hourly["apparent_temperature"]),

                ["2.a"] = ("Maximum Temperature", () => settings.Daily["temperature_2m_max"] = !settings.Daily["temperature_2m_max"]),
                ["2.b"] = ("Minimum Temperature", () => settings.Daily["temperature_2m_min"] = !settings.Daily["temperature_2m_min"]),
                ["2.c"] = ("Sunrise - Sunset", () => settings.Daily["sunrise,sunset"] = !settings.Daily["sunrise,sunset"]),
                ["2.d"] = ("UV Index", () => settings.Daily["uv_index_max"] = !settings.Daily["uv_index_max"]),

                ["3.a"] = ("Show Current", () => settings.UseCurrentWeather = !settings.UseCurrentWeather),
                ["3.b"] = ("Display Custom Hour", () => settings.DisplayCustomHour = !settings.DisplayCustomHour)
            };

            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("Configure Weather Settings:\n");

                System.Console.WriteLine("1. Hourly Weather Variables");
                System.Console.WriteLine($"{Check(settings.Hourly["temperature_2m"])}a. Temperature");
                System.Console.WriteLine($"{Check(settings.Hourly["relative_humidity_2m"])}b. Relative Humidity");
                System.Console.WriteLine($"{Check(settings.Hourly["apparent_temperature"])}c. Apparent Temperature");

                System.Console.WriteLine("\n2. Daily Weather Variables");
                System.Console.WriteLine($"{Check(settings.Daily["temperature_2m_max"])}a. Maximum Temperature");
                System.Console.WriteLine($"{Check(settings.Daily["temperature_2m_min"])}b. Minimum Temperature");
                System.Console.WriteLine($"{Check(settings.Daily["sunrise,sunset"])}c. Sunrise - Sunset");
                System.Console.WriteLine($"{Check(settings.Daily["uv_index_max"])}d. UV Index");

                System.Console.WriteLine("\n3. Current Weather");
                System.Console.WriteLine($"{Check(settings.UseCurrentWeather)}a. Show Current");
                System.Console.WriteLine($"{Check(settings.DisplayCustomHour)}b. Display Custom Hour");

                System.Console.WriteLine("\n\tInput setting number to switch it (i.e '1.a')");
                System.Console.WriteLine("\tWrite 'exit' to return to the main menu");
                System.Console.Write("\t>> ");
                string? input = System.Console.ReadLine()?.Trim().ToLower();

                if (input == "exit")
                    break;

                if (input != null && options.TryGetValue(input, out var item))
                    item.toggle();
            }

            return settings;
        }

        private static string Check(bool isOn) => isOn ? "[X]" : "[ ]";
    }
}