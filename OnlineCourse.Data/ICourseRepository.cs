using OnlineCourse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public interface ICourseRepository
    {
        public Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null);

        public Task<CourseDetailModel> GetCourseDetailsAsync(int courseId);
    }
}
