
namespace BasicWeatherApi.Console
{
    class Program
    {
        static City? SelectCity(List<City> cities)
        {
            for (int i = 0; i < cities.Count; i++)
                System.Console.WriteLine($"{i}-{cities[i].Name}");

            System.Console.Write("Enter city index:\n>>");
            int index = int.Parse(System.Console.ReadLine()!);
            if (index >= 0 && index < cities.Count)
            {
                return cities[index];
            }

            return null;
        }
        static async Task Main(string[] args)
        {
            List<City> cities = new List<City>
            {
                new("İzmir", 38.4127, 27.1384),
                new("İstanbul", 41.0138, 28.9497),
                new("Ankara", 39.9199, 32.8543),
                new("Adana", 36.9862, 35.3253),
                new("Kayseri", 38.7322, 35.4853),
                new("Eskişehir", 39.7767, 30.5206)
            };

            while (true)
            {
                City city = SelectCity(cities);
                if (city == null)
                {
                    System.Console.WriteLine("Invalid city, restarting...\n");
                    continue;
                }
                List<double> temps = await WeatherService.GetHourlyTemperaturesAsync(city);

                System.Console.Write("Enter hour (0–23):\n>>");
                int hour = int.Parse(System.Console.ReadLine()!);
                if (hour < 0 || hour > 23)
                {
                    System.Console.WriteLine("Invalid hour, restarting...\n");
                    continue;
                }

                System.Console.WriteLine($"{city.Name} at {hour}:00 --> {temps[hour]} °C");

                System.Console.WriteLine("Press Enter to continue or press backspace to quit...");
                if (System.Console.ReadKey().Key == ConsoleKey.Backspace) break;
            }
        }
    }
}