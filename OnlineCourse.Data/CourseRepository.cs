using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Models;


namespace OnlineCourse.Data
{
    public class CourseRepository(OnlineCourseDbContext dbContext) : ICourseRepository
    {
        private readonly OnlineCourseDbContext dbContext = dbContext;

        public async Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            var query = dbContext.Courses.Include(c => c.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            var courses = await query.Select(s => new CourseModel()
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CourseType = s.CourseType,
                SeatsAvailable = s.SeatsAvailable,
                Duration = s.Duration,
                CategoryId = s.CategoryId,
                InstructorId = s.InstructorId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Thumbnail = s.Thumbnail,
                Category = new CourseCategoryModel()
                {
                    Id = s.Category.Id,
                    CategoryName = s.Category.CategoryName,
                    Description = s.Category.Description,
                    
                },
                UserRating = new UserRatingModel()
                {
                    CourseId = s.Id,
                    AverageRating = s.Reviews.Any() ? Convert.ToDecimal(s.Reviews.Average(r => r.Rating)) : 0,
                    TotalRatings =  s.Reviews.Count()
                }
            }).ToListAsync();
            

            return courses;
        }

        public async Task<CourseDetailModel> GetCourseDetailsAsync(int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.Id == courseId)
                .Select(c => new CourseDetailModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    InstructorId = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Thumbnail = c.Thumbnail,
                    Category = new CourseCategoryModel()
                    {
                        Id = c.Category.Id,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description, 
                    },
                    Reviews = c.Reviews.Select(r => new UserReviewModel()
                    {
                        CourseId = r.Id,
                        Username = r.User.Username,
                        Rating = r.Rating,
                        Comments = r.Comments,
                        ReviewDate = r.ReviewDate,
                    }).OrderByDescending(o => o.Rating).Take(10).ToList(),
                    SessionDetails = c.SessionDetails.Select(sd => new SessionDetailModel()
                    {
                        Id = sd.Id,
                        CourseId = sd.CourseId,
                        Title = sd.Title,
                        Description = sd.Description,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder,
                    }).OrderBy(o => o.VideoOrder).ToList(),
                    UserRating = new UserRatingModel()
                    {
                        CourseId = c.Id,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRatings = c.Reviews.Count()
                    }
                }).FirstOrDefaultAsync();

            return course;
        }
    }
}
