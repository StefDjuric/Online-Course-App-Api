using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Data.Entities;

namespace OnlineCourse.Controllers
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
        private readonly OnlineCourseDbContext dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, OnlineCourseDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
           var courses = dbContext.Courses.ToList();
           return Ok(courses);
        }
    }
}
