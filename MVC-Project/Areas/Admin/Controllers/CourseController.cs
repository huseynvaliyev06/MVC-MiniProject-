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

        // GET /Admin/Course
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Courses";
            var list = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.CourseImages)
                .Select(c => new AdminCourseIndexVM
                {
                    Id          = c.Id,
                    Title       = c.Title,
                    Price       = c.Price,
                    SalesCount  = c.SalesCount,
                    IsFeature   = c.IsFeature,
                    IsNew       = c.IsNew,
                    TeacherName = c.Teacher.FullName,
                    MainImage   = c.CourseImages.FirstOrDefault(x => x.IsMain).CourseImg
                }).ToListAsync();
            return View(list);
        }

        // GET /Admin/Course/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "New Course";
            await PopulateTeachers();
            return View(new AdminCourseCreateVM());
        }

        // POST /Admin/Course/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCourseCreateVM vm)
        {
            if (!ModelState.IsValid) { await PopulateTeachers(vm.TeacherId); return View(vm); }

            var images = new List<CourseImage>();
            string folder = Path.Combine("wwwroot", "images");
            Directory.CreateDirectory(folder);

            if (vm.MainImage != null)
            {
                var name = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.MainImage.CopyToAsync(s);
                images.Add(new CourseImage { CourseImg = name, IsMain = true });
            }
            if (vm.DetailImages != null)
                foreach (var f in vm.DetailImages)
                {
                    var name = Guid.NewGuid() + Path.GetExtension(f.FileName);
                    await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                    await f.CopyToAsync(s);
                    images.Add(new CourseImage { CourseImg = name, IsMain = false });
                }

            await _context.Courses.AddAsync(new CourseInfo
            {
                Title = vm.Title, Description = vm.Description,
                Price = vm.Price, SalesCount = vm.SalesCount,
                IsFeature = vm.IsFeature, IsNew = vm.IsNew,
                TeacherId = vm.TeacherId, CourseImages = images
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Course created.";
            return RedirectToAction(nameof(Index));
        }

        // GET /Admin/Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Course";
            var c = await _context.Courses.Include(x => x.CourseImages).FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound();
            await PopulateTeachers(c.TeacherId);
            return View(new AdminCourseEditVM
            {
                Id = c.Id, Title = c.Title, Description = c.Description,
                Price = c.Price, SalesCount = c.SalesCount,
                IsFeature = c.IsFeature, IsNew = c.IsNew, TeacherId = c.TeacherId,
                ExistingMainImage = c.CourseImages.FirstOrDefault(x => x.IsMain)?.CourseImg
            });
        }

        // POST /Admin/Course/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminCourseEditVM vm)
        {
            if (!ModelState.IsValid) { await PopulateTeachers(vm.TeacherId); return View(vm); }

            var c = await _context.Courses.Include(x => x.CourseImages).FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (c == null) return NotFound();

            c.Title = vm.Title; c.Description = vm.Description;
            c.Price = vm.Price; c.SalesCount = vm.SalesCount;
            c.IsFeature = vm.IsFeature; c.IsNew = vm.IsNew; c.TeacherId = vm.TeacherId;

            string folder = Path.Combine("wwwroot", "images");
            Directory.CreateDirectory(folder);

            if (vm.MainImage != null)
            {
                var name = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.MainImage.CopyToAsync(s);
                var existing = c.CourseImages.FirstOrDefault(x => x.IsMain);
                if (existing != null) existing.CourseImg = name;
                else c.CourseImages.Add(new CourseImage { CourseImg = name, IsMain = true });
            }
            if (vm.DetailImages != null)
                foreach (var f in vm.DetailImages)
                {
                    var name = Guid.NewGuid() + Path.GetExtension(f.FileName);
                    await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                    await f.CopyToAsync(s);
                    c.CourseImages.Add(new CourseImage { CourseImg = name, IsMain = false });
                }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Course updated.";
            return RedirectToAction(nameof(Index));
        }

        // POST /Admin/Course/Delete/5
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
