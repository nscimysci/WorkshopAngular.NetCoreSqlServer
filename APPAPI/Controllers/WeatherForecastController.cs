using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APPAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public DatabaseContext Context { get; }
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DatabaseContext Context)
        {
            _logger = logger;
            this.Context = Context;
        }

        [HttpGet("getProductAll")]
        public async Task<IActionResult> getProductAll()
        {
            try
            {
                var data = await this.Context.Products.ToListAsync();
                return Ok(new { result = data, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "", message = ex.Message });
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
