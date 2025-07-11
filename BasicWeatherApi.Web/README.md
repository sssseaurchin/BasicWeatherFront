# BasicWeatherApi.Web

BasicWeatherApi.Web is a simple ASP.NET Core Web Application that fetches and displays weather data based on user settings using [open-meteo](https://open-meteo.com/).
> [!NOTE]
> Deployed site link will be available after i figure out how to do it...

<div align="center">
  <img width="360" height="335" alt="sample" src="https://github.com/user-attachments/assets/bb442d36-70b9-4515-b616-aeb8c5973278" />
</div>

## Features

- Follows the Model-View-Controller architecture.
- Fetch weather forecasts for a selected city
- City-based location selection ~~and filtering~~
> Will be implemented soon!
- Display weather data such as humidity, UV index, max/min temperature, sunset/sunrise.

## Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

```
git clone https://github.com/sssseaurchin/BasicWeatherFront.git
cd BasicWeatherApi.Web
dotnet run
```
The locally hosted link will be http://localhost:5292 (unless edited on your end).

## Project Structure
```
Copy code
BasicWeatherApi.Web/
├── Controllers/
│   └── WeatherController.cs       # Main controller for weather endpoint
├── Models/
│   ├── City.cs                    # Represents city data
│   └── ErrorViewModel.cs         # Used for error handling views
├── Data/
│   └── CityData.cs               # Static data source for cities
├── Views/                        # Razor views (optional in current version)
├── wwwroot/                      # Static files (CSS/JS)
├── appsettings.json              # Configurations
└── Program.cs                    # Entry point
``` 
### API Reference
Open-Meteo API docs: https://open-meteo.com/en/docs

### Future Improvements

- Add user editable filter options
- Add displaying multiple day forecasts as an option
- Improve city search with autocomplete
