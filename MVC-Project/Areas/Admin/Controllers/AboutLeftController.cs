using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Areas.Admin.ViewModels;
using MVC_MiniProject.Data;
using MVC_MiniProject.Models;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AboutLeftController : Controller
    {
        private readonly AppDbContext _context;
        public AboutLeftController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "About Left";
            return View(await _context.AboutLefts.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New About Left";
            return View(new AdminAboutLeftCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminAboutLeftCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string imageName = "about_1.jpg";
            if (vm.LeftImage != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                imageName = Guid.NewGuid() + Path.GetExtension(vm.LeftImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, imageName), FileMode.Create);
                await vm.LeftImage.CopyToAsync(s);
            }

            await _context.AboutLefts.AddAsync(new AboutLeft
            {
                Title = vm.Title, Description = vm.Description, LeftImage = imageName
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "About Left created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit About Left";
            var a = await _context.AboutLefts.FindAsync(id);
            if (a == null) return NotFound();
            return View(new AdminAboutLeftEditVM
            {
                Id = a.Id, Title = a.Title,
                Description = a.Description, ExistingImage = a.LeftImage
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminAboutLeftEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var a = await _context.AboutLefts.FindAsync(vm.Id);
            if (a == null) return NotFound();
            a.Title = vm.Title; a.Description = vm.Description;
            if (vm.LeftImage != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                var name = Guid.NewGuid() + Path.GetExtension(vm.LeftImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.LeftImage.CopyToAsync(s);
                a.LeftImage = name;
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "About Left updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _context.AboutLefts.FindAsync(id);
            if (a != null) { _context.AboutLefts.Remove(a); await _context.SaveChangesAsync(); }
            TempData["Success"] = "About Left deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
