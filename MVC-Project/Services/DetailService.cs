using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.Services
{
    public class DetailService : IDetailService
    {
        private readonly AppDbContext _context;

        public DetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CourseInformationUIVM> GetByIdAsync(int id)
        {
            var course = await _context.Courses.Where(m => m.Id == id).Include(c => c.CourseImages).Include(c => c.Teacher).Select(m => new CourseInformationUIVM
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Price = m.Price,
                SalesCount = m.SalesCount,
                IsFeature = m.IsFeature,
                IsNew = m.IsNew,
                TeacherName = m.Teacher.FullName,
                TeacherImage = m.Teacher.Image,
                MainImage = m.CourseImages.FirstOrDefault(c => c.IsMain).CourseImg,
                CourseImages = m.CourseImages.Select(image => new CoursesImageUIVM
                {
                    ImageName = image.CourseImg,
                    IsMain = image.IsMain
                }).ToList()
            }).FirstOrDefaultAsync();

            return course;
        }
    }
}
