using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data;
using OnlineCourse.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class CourseCategoryRepository(OnlineCourseDbContext dbContext) : ICourseCategoryRepository
    {
        private readonly OnlineCourseDbContext dbContext = dbContext;

        public Task<CourseCategory?> GetByIdAsync(int id)
        {
            var category = dbContext.CourseCategories.FindAsync(id).AsTask();
            return category;
        }

        public Task<List<CourseCategory>> GetCourseCategoriesAsync()
        {
            var categories = dbContext.CourseCategories.ToListAsync();
            return categories;
        }
    }
}
