using OnlineCourse.Core.Entities;
using OnlineCourse.Core.Models;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Service
{
    public class CourseCategoryService(ICourseCategoryRepository categoryRepository) : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository categoryRepository = categoryRepository;

        public async Task<CourseCategoryModel?> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            return new CourseCategoryModel()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public async Task<List<CourseCategoryModel>> GetCategoriesAsync()
        {
            var categories = await categoryRepository.GetCourseCategoriesAsync();
            var modelCategories = categories.Select(cat => new CourseCategoryModel() {
                Id = cat.Id,
                CategoryName = cat.CategoryName,
                Description = cat.Description
            }).ToList();

            return modelCategories;
        }
    }
}
