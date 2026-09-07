using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Areas.Admin.ViewModels;
using MVC_MiniProject.Data;
using MVC_MiniProject.Models;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Courses";
            var list = await _context.Courses
                .Include(c => c.Teacher)
                .Select(c => new AdminCourseIndexVM
                {
                    Id          = c.Id,
                    Name        = c.Name,
                    Price       = c.Price,
                    SalesCount  = c.SalesCount,
                    IsFeatured  = c.IsFeatured,
                    Tag         = c.Tag,
                    TeacherName = c.Teacher.FullName
                }).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "New Course";
            await PopulateTeachers();
            return View(new AdminCourseCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCourseCreateVM vm)
        {
            if (!ModelState.IsValid) { await PopulateTeachers(vm.TeacherId); return View(vm); }

            string? mainImageName = vm.MainImage;

            if (vm.MainImageFile != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                mainImageName = Guid.NewGuid() + Path.GetExtension(vm.MainImageFile.FileName);
                await using var s = new FileStream(Path.Combine(folder, mainImageName), FileMode.Create);
                await vm.MainImageFile.CopyToAsync(s);
            }

            await _context.Courses.AddAsync(new CourseInfo
            {
                Name        = vm.Name,
                Description = vm.Description,
                Price       = vm.Price,
                SalesCount  = vm.SalesCount,
                IsFeatured  = vm.IsFeatured,
                AuthorImage = vm.AuthorImage,
                Tag         = vm.Tag,
                MainImage   = mainImageName,
                TeacherName = vm.TeacherName,
                TeacherId   = vm.TeacherId
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Course created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Course";
            var c = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound();
            await PopulateTeachers(c.TeacherId);
            return View(new AdminCourseEditVM
            {
                Id               = c.Id,
                Name             = c.Name,
                Description      = c.Description,
                Price            = c.Price,
                SalesCount       = c.SalesCount,
                IsFeatured       = c.IsFeatured,
                AuthorImage      = c.AuthorImage,
                Tag              = c.Tag,
                MainImage        = c.MainImage,
                TeacherName      = c.TeacherName,
                TeacherId        = c.TeacherId,
                ExistingMainImage = c.MainImage
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminCourseEditVM vm)
        {
            if (!ModelState.IsValid) { await PopulateTeachers(vm.TeacherId); return View(vm); }

            var c = await _context.Courses.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (c == null) return NotFound();

            string? mainImageName = vm.MainImage;

            if (vm.MainImageFile != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                mainImageName = Guid.NewGuid() + Path.GetExtension(vm.MainImageFile.FileName);
                await using var s = new FileStream(Path.Combine(folder, mainImageName), FileMode.Create);
                await vm.MainImageFile.CopyToAsync(s);
            }

            c.Name        = vm.Name;
            c.Description = vm.Description;
            c.Price       = vm.Price;
            c.SalesCount  = vm.SalesCount;
            c.IsFeatured  = vm.IsFeatured;
            c.AuthorImage = vm.AuthorImage;
            c.Tag         = vm.Tag;
            c.MainImage   = mainImageName;
            c.TeacherName = vm.TeacherName;
            c.TeacherId   = vm.TeacherId;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Course updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c != null) { _context.Courses.Remove(c); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Course deleted.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateTeachers(int selected = 0)
        {
            var teachers = await _context.Teachers.ToListAsync();
            ViewBag.Teachers = new SelectList(teachers, "Id", "FullName", selected);
        }
    }
}
