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
            var course = await _context.Courses
                .Where(m => m.Id == id)
                .Include(c => c.Teacher)
                .Select(m => new CourseInformationUIVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    SalesCount = m.SalesCount,
                    IsFeatured = m.IsFeatured,
                    AuthorImage = m.AuthorImage,
                    Tag = m.Tag,
                    MainImage = m.MainImage,
                    TeacherName = m.Teacher.FullName,
                    TeacherImage = m.Teacher.Image
                })
                .FirstOrDefaultAsync();

            return course;
        }
    }
}
