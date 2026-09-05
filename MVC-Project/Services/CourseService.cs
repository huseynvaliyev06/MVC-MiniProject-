
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
            var courseImages = new List<CourseImage>();

            string folderPath = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Main Image
            if (model.MainImage != null)
            {
                string mainImageName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(model.MainImage.FileName);

                string mainPath = Path.Combine(folderPath, mainImageName);

                using (var stream = new FileStream(mainPath, FileMode.Create))
                {
                    await model.MainImage.CopyToAsync(stream);
                }

                courseImages.Add(new CourseImage
                {
                    CourseImg = mainImageName,
                    IsMain = true
                });
            }

            // Detail Images
            if (model.DetailImages != null && model.DetailImages.Count > 0)
            {
                foreach (var file in model.DetailImages)
                {
                    string imageName =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(file.FileName);

                    string path = Path.Combine(folderPath, imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    courseImages.Add(new CourseImage
                    {
                        CourseImg = imageName,
                        IsMain = false
                    });
                }
            }

            await _context.Courses.AddAsync(new CourseInfo
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                SalesCount = model.SalesCount,
                IsFeature = model.IsFeature,
                IsNew = model.IsNew,
                TeacherId = model.TeacherId,
                CourseImages = courseImages
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses
                .AnyAsync(c => c.Title == name);
        }

        public async Task<IEnumerable<CoursesVM>> GetAllAsync()
        {
            var courses = await _context.Courses
                .Select(c => new CoursesVM
                {
                    Id = c.Id,
                    Name = c.Title
                })
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<CourseInformationUIVM>> GetAllUIAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.CourseImages)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeature = c.IsFeature,
                    IsNew = c.IsNew,
                    TeacherName = c.Teacher.FullName,
                    MainImage = c.CourseImages
                        .FirstOrDefault(x => x.IsMain).CourseImg,
                    TeacherImage = c.Teacher.Image
                })
                .ToListAsync();

            return courses;
        }

        public async Task<CourseInformationUIVM> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseImages)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Where(c => c.Id == id)
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeature = c.IsFeature,
                    IsNew = c.IsNew,
                    TeacherName = c.Teacher.FullName,
                    MainImage = c.CourseImages
                        .FirstOrDefault(x => x.IsMain).CourseImg,
                    TeacherImage = c.Teacher.Image
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            return course;
        }

        public async Task<CoursesDetailVM> GetDetailAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseImages)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var mainImage = course.CourseImages
                .FirstOrDefault(x => x.IsMain);

            var detailImages = course.CourseImages
                .Where(x => !x.IsMain)
                .Select(x => x.CourseImg)
                .ToList();

            var courseDetail = new CoursesDetailVM
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                SalesCount = course.SalesCount,
                IsFeature = course.IsFeature,
                IsNew = course.IsNew,

                MainImage = mainImage != null
                    ? mainImage.CourseImg
                    : "",

                DetailImages = detailImages,

                TeacherId = course.TeacherId,
                TeacherFullName = course.Teacher.FullName,
                TeacherPosition = course.Teacher.Position.Name,
                TeacherPhoto = course.Teacher.Image
            };

            return courseDetail;
        }

        public async Task<CoursesEditVM> GetForEditAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseImages)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var model = new CoursesEditVM
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                SalesCount = course.SalesCount,
                IsFeature = course.IsFeature,
                IsNew = course.IsNew,
                TeacherId = course.TeacherId
            };

            return model;
        }

        public async Task<IEnumerable<CourseInformationUIVM>> SearchAsync(string searchText)
        {
            var courses = await _context.Courses
                .Include(c => c.CourseImages)
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Position)
                .Where(c =>
                    string.IsNullOrWhiteSpace(searchText) ||
                    c.Title.Contains(searchText) ||
                    c.Description.Contains(searchText))
                .Select(c => new CourseInformationUIVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    SalesCount = c.SalesCount,
                    IsFeature = c.IsFeature,
                    IsNew = c.IsNew,
                    TeacherName = c.Teacher.FullName,
                    MainImage = c.CourseImages
                        .FirstOrDefault(x => x.IsMain).CourseImg,
                    TeacherImage = c.Teacher.Image
                })
                .ToListAsync();

            return courses;
        }

        public async Task UpdateAsync(CoursesEditVM model)
        {
            var course = await _context.Courses
                .Include(c => c.CourseImages)
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            course.Title = model.Title;
            course.Description = model.Description;
            course.Price = model.Price;
            course.SalesCount = model.SalesCount;
            course.IsFeature = model.IsFeature;
            course.IsNew = model.IsNew;
            course.TeacherId = model.TeacherId;

            string folderPath = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Main Image
            if (model.MainImage != null)
            {
                string mainImageName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(model.MainImage.FileName);

                string mainPath = Path.Combine(folderPath, mainImageName);

                using (var stream = new FileStream(mainPath, FileMode.Create))
                {
                    await model.MainImage.CopyToAsync(stream);
                }

                var existingMain = course.CourseImages
                    .FirstOrDefault(x => x.IsMain);

                if (existingMain != null)
                {
                    existingMain.CourseImg = mainImageName;
                }
                else
                {
                    course.CourseImages.Add(new CourseImage
                    {
                        CourseImg = mainImageName,
                        IsMain = true
                    });
                }
            }

            // Detail Images
            if (model.DetailImages != null && model.DetailImages.Count > 0)
            {
                foreach (var file in model.DetailImages)
                {
                    string imageName =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(file.FileName);

                    string path = Path.Combine(folderPath, imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    course.CourseImages.Add(new CourseImage
                    {
                        CourseImg = imageName,
                        IsMain = false
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}

