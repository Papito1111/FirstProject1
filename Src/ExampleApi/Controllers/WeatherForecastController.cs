using Microsoft.AspNetCore.Mvc;
using System;

namespace ExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "ColdToDeath", "Bra", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly Burza Lejewkit;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Burza lejewkit)
    {
        _logger = logger;
        Lejewkit = lejewkit;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
         int i = 5;
        Lejewkit.CzyPada();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{date}", Name = "GetWeatherForecastForAnotherDay")]
    public WeatherForecast Get(DateTime date)
    {
        throw new Exception("Ups");
        //return new WeatherForecast
        //{
        //    Date = new DateOnly(date.Year,date.Month, date.Day),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //};
    }
}
