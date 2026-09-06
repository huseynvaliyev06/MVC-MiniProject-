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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Teachers";
            var list = await _context.Teachers.Include(t => t.Position)
                .Select(t => new AdminTeacherIndexVM
                {
                    Id = t.Id, FullName = t.FullName,
                    Image = t.Image, PositionName = t.Position.Name
                }).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "New Teacher";
            await PopulatePositions();
            return View(new AdminTeacherCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminTeacherCreateVM vm)
        {
            if (!ModelState.IsValid) { await PopulatePositions(vm.PositionId); return View(vm); }

            string imageName = "default.jpg";
            if (vm.Image != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                imageName = Guid.NewGuid() + Path.GetExtension(vm.Image.FileName);
                await using var s = new FileStream(Path.Combine(folder, imageName), FileMode.Create);
                await vm.Image.CopyToAsync(s);
            }

            await _context.Teachers.AddAsync(new Teacher
            {
                FullName = vm.FullName, PositionId = vm.PositionId, Image = imageName
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Teacher created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Teacher";
            var t = await _context.Teachers.FindAsync(id);
            if (t == null) return NotFound();
            await PopulatePositions(t.PositionId);
            return View(new AdminTeacherEditVM
            {
                Id = t.Id, FullName = t.FullName,
                PositionId = t.PositionId, ExistingImage = t.Image
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminTeacherEditVM vm)
        {
            if (!ModelState.IsValid) { await PopulatePositions(vm.PositionId); return View(vm); }

            var t = await _context.Teachers.FindAsync(vm.Id);
            if (t == null) return NotFound();

            t.FullName = vm.FullName; t.PositionId = vm.PositionId;

            if (vm.Image != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                var name = Guid.NewGuid() + Path.GetExtension(vm.Image.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.Image.CopyToAsync(s);
                t.Image = name;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Teacher updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.Teachers.FindAsync(id);
            if (t != null) { _context.Teachers.Remove(t); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Teacher deleted.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulatePositions(int selected = 0)
        {
            var positions = await _context.Positions.ToListAsync();
            ViewBag.Positions = new SelectList(positions, "Id", "Name", selected);
        }
    }
}
