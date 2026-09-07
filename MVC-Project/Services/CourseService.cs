using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CoursesCreateVM model)
        {
            await _context.Courses.AddAsync(new CourseInfo
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                SalesCount = model.SalesCount,
                IsFeatured = model.IsFeatured,
                AuthorImage = model.AuthorImage,
                Tag = model.Tag,
                TeacherId = model.TeacherId
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new Exception("Course not found");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses
                .AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<CoursesVM>> GetAllAsync()
        {
            return await _context.Courses
                .Select(c => new CoursesVM
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseInformationUIVM>> GetAllUIAsync()
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeatured = c.IsFeatured,
                    AuthorImage = c.AuthorImage,
                    Tag = c.Tag,
                    MainImage = c.MainImage,
                    TeacherName = c.Teacher.FullName,
                    TeacherImage = c.Teacher.Image
                })
                .ToListAsync();
        }

        public async Task<CourseInformationUIVM> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Where(c => c.Id == id)
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeatured = c.IsFeatured,
                    AuthorImage = c.AuthorImage,
                    Tag = c.Tag,
                    MainImage = c.MainImage,
                    TeacherName = c.Teacher.FullName,
                    TeacherImage = c.Teacher.Image
                })
                .FirstOrDefaultAsync();

            if (course == null)
                throw new Exception("Course not found");

            return course;
        }

        public async Task<CoursesDetailVM> GetDetailAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new Exception("Course not found");

            return new CoursesDetailVM
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                SalesCount = course.SalesCount,
                IsFeatured = course.IsFeatured,
                AuthorImage = course.AuthorImage,
                Tag = course.Tag,
                TeacherId = course.TeacherId,
                TeacherFullName = course.Teacher.FullName,
                TeacherPosition = course.Teacher.Position.Name,
                TeacherPhoto = course.Teacher.Image
            };
        }

        public async Task<CoursesEditVM> GetForEditAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new Exception("Course not found");

            return new CoursesEditVM
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                SalesCount = course.SalesCount,
                IsFeatured = course.IsFeatured,
                AuthorImage = course.AuthorImage,
                Tag = course.Tag,
                TeacherId = course.TeacherId
            };
        }

        public async Task<IEnumerable<CourseInformationUIVM>> SearchAsync(string searchText)
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Where(c =>
                    string.IsNullOrWhiteSpace(searchText) ||
                    c.Name.Contains(searchText) ||
                    c.Description.Contains(searchText))
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeatured = c.IsFeatured,
                    AuthorImage = c.AuthorImage,
                    Tag = c.Tag,
                    MainImage = c.MainImage,
                    TeacherName = c.Teacher.FullName,
                    TeacherImage = c.Teacher.Image
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(CoursesEditVM model)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (course == null)
                throw new Exception("Course not found");

            course.Name = model.Name;
            course.Description = model.Description;
            course.Price = model.Price;
            course.SalesCount = model.SalesCount;
            course.IsFeatured = model.IsFeatured;
            course.AuthorImage = model.AuthorImage;
            course.Tag = model.Tag;
            course.TeacherId = model.TeacherId;

            await _context.SaveChangesAsync();
        }
    }
}
