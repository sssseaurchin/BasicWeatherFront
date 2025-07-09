# BasicWeatherApi.Console

This is a simple C# .NET console application that fetches and displays hourly temperature data for selected Turkish cities using [Open-Meteo](https://open-meteo.com/).

## Features
- Select a city from a predefined list
- View the temperature at a selected hour (0–23)
- Live data fetched from Open-Meteo
- Runs in a loop until you press backspace

## Structure
```
BasicWeatherApi.Console
│   
├───City.cs
├───Program.cs
├───README.md
└───WeatherService.cs
```

## Dependencies
- .NET 6.0+
- No external packages required (uses HttpClient and System.Text.Json)