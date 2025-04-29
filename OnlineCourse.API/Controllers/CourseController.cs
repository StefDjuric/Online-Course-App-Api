using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core.Models;
using OnlineCourse.Service;

namespace OnlineCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        private readonly ICourseService courseService = courseService;

        // Get api/Course
        [HttpGet]
        public async Task<ActionResult<List<CourseModel>>> GetAllCoursesAsync()
        {
            var courses = await courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // Get api/Course/Category?categoryId=""
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<List<CourseModel>>> GetAllCoursesAsync([FromRoute] int categoryId)
        {
            var courses = await courseService.GetAllCoursesAsync(categoryId);
            return Ok(courses);
        }

        // Get api/Course/Detail?courseId=""
        [HttpGet("Detail/{courseId}")]
        public async Task<ActionResult<CourseDetailModel>> GetCourseDetailsAsync([FromRoute] int courseId)
        {
            var courseDetails = await courseService.GetCourseDetailAsync(courseId);
            if (courseDetails == null)
            {
                return NotFound();
            }

            return Ok(courseDetails);
        }

    }
}
