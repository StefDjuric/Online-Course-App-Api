using OnlineCourse.Core.Models;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Service
{
    public class CourseService(ICourseRepository courseRepository ) : ICourseService
    {
        private readonly ICourseRepository courseRepository = courseRepository;

        public Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            return courseRepository.GetAllCoursesAsync(categoryId);
        }

        public Task<CourseDetailModel> GetCourseDetailAsync(int courseId)
        {
            return courseRepository.GetCourseDetailsAsync(courseId);
        }
    }
}
