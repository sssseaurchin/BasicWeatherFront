using BasicWeatherApi.Web.Models;
using BasicWeatherApi.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicWeatherApi.Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            //yeaa idk
            // return View(data);
            return View(_weatherService.GetAllWeatherData());
        }
        
    }
}