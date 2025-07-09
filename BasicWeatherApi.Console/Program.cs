
namespace BasicWeatherApi.Console
{
    class Program
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
                    System.Console.Write($"{index+1}-{cities[index].Name} ");
                    index++;
                }
                System.Console.WriteLine();
            }
        }
        static async Task Main(string[] args)
        {
            List<City> cities = CityData.GetCityData();

            while (true)
            {
                System.Console.Clear();
                City city = SelectCity(cities);
                if (city == null)
                {
                    System.Console.WriteLine("Invalid city, press any key to continue...\n");
                    System.Console.ReadKey();
                    continue;
                }
                List<double> temps = await WeatherService.GetHourlyTemperaturesAsync(city);

                System.Console.Write("Enter hour (0–23):\n>>");
                int hour = int.Parse(System.Console.ReadLine()!);
                if (hour < 0 || hour > 23)
                {
                    System.Console.WriteLine("Invalid hour, press any key to continue...\n");
                    System.Console.ReadKey();
                    continue;
                }

                System.Console.WriteLine($"{city.Name} at {hour}:00 --> {temps[hour]} °C");

                System.Console.WriteLine("Press Enter to continue or press backspace to quit...");
                if (System.Console.ReadKey().Key == ConsoleKey.Backspace) break;
            }
        }
    }
}