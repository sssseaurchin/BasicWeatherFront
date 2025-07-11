using BasicWeatherApi.Web.Data;
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

        [HttpGet]
        public async Task<IActionResult> Index(int? cityIndex)
        {
            var cityList = CityData.GetCityData();
            int index = cityIndex.GetValueOrDefault(0);

            if (index < 0 || index >= cityList.Count)
                index = 0;

            var data = await _weatherService.GetAllWeatherDataAsync(cityList[index]);

            ViewBag.CityList = cityList;
            ViewBag.CityIndex = index;

            return View(data);
        }

        [HttpPost]
        public IActionResult Index(int cityIndex)
        {
            return RedirectToAction("Index", new { cityIndex });
        }
        
    }
}