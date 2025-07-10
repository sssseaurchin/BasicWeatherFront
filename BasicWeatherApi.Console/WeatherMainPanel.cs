namespace BasicWeatherApi.Console
{

    public static class WeatherMainPanel
    {
        private static City? SelectCity(List<City> cities)
        {
            int paging = 10;
            PrintCities(cities);
            System.Console.Write("Enter city index:\n>>");
            int index = int.Parse(System.Console.ReadLine()!) - 1;
            if (index >= 0 && index < cities.Count)
            {
                return cities[index];
            }

            return null;
        }

        private static void PrintCities(List<City> cities)
        {
            int pageSize = 10;
            int total = cities.Count;
            int index = 0;


            for (int i = 0; i < pageSize && index < total; i++)
            {
                for (int j = 0; j < total; j += pageSize)
                {
                    string output = $"{index + 1}-{cities[index].Name} ";
                    System.Console.Write(output.PadRight(18));
                    index++;
                }

                System.Console.WriteLine();
            }
        }

        public static async Task Display()
        {
            List<City> cities = CityData.GetCityData();

            while (true)
            {
                System.Console.Clear();
                WeatherSettings settings = SettingsEditor.GetWeatherSettingsFromUser();
                City? city = SelectCity(cities);
                if (city == null)
                {
                    System.Console.WriteLine("Invalid city, press any key to continue...\n");
                    System.Console.ReadKey();
                    continue;
                }
                System.Console.Write("Enter hour (0–23):\n>>");
                int hour = int.Parse(System.Console.ReadLine()!);
                if (hour < 0 || hour > 23)
                {
                    System.Console.WriteLine("Invalid hour, press any key to continue...\n");
                    System.Console.ReadKey();
                    continue;
                }
                
                // change later vvv
                var json = await WeatherService.FetchWeatherJsonAsync(await WeatherService.GenerateRequestLink(city,settings));

                System.Console.Clear();
                System.Console.WriteLine($"[Weather for {city.Name}]\n");

                if (settings.UseCurrentWeather)
                {
                    var current = WeatherDataParser.ParseCurrentWeather(json);
                    foreach (var pair in current)
                        System.Console.WriteLine($"Current {pair.Key.Replace("_", " ")}: {pair.Value}");
                }

                if (settings.Hourly.Any(h => h.Value))
                {
                    var hourly = WeatherDataParser.ParseHourlyWeather(json, settings, hour);
                    System.Console.WriteLine("\n[Hourly Data]");
                    foreach (var pair in hourly)
                        System.Console.WriteLine($"{pair.Key.Replace("_", " ")} at {hour}:00: {pair.Value}");
                }

                if (settings.Daily.Any(d => d.Value))
                {
                    var daily = WeatherDataParser.ParseDailyWeather(json, settings);
                    System.Console.WriteLine("\n[Daily Data]");
                    foreach (var pair in daily)
                        System.Console.WriteLine($"{pair.Key.Replace("_", " ")} (Today): {pair.Value}");
                }

                // change later ^^^
                
                // System.Console.WriteLine($"{city.Name} at {hour}:00 --> {temps[hour]} °C");

                System.Console.Write("Press Enter to continue or write 'exit' to quit...\n>>");
                string? input = System.Console.ReadLine()?.Trim().ToLower();
                if (input == "exit")
                    break;

            }
        }
    }
}