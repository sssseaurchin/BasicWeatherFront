# BasicWeatherApi.Console

This is a simple C# .NET console application that fetches and displays weather data for selected Turkish cities using [Open-Meteo](https://open-meteo.com/).

## Features
- Set the weather data to fetch from 'hourly', 'daily' and 'current'
- Select a city from a predefined list
- View selected fields' data
- Live data fetched from Open-Meteo
- Runs in a loop until you write 'exit'

## Structure
```
BasicWeatherApi.Console
│   
├───City.cs
├───CityData.cs
├───Program.cs
├───README.md
├───SettingsEditor.cs
├───WeatherDataParser.cs
├───WeatherMainPanel.cs
├───WeatherService.cs
└───WeatherSettings.cs
```
## Compiling
Run this in the console directory
```cmd
dotnet publish -c Release -r win-x64 --self-contained
```
The .exe can be located in 
**BasicWeatherApi\BasicWeatherApi.Console\bin\Release\net8.0\win-x64\BasicWeatherApi.Console.exe**

## Dependencies
- .NET 6.0+
- No external packages required (uses HttpClient and System.Text.Json)
