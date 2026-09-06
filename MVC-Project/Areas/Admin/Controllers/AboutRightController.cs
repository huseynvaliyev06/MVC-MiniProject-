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
    public class AboutRightController : Controller
    {
        private readonly AppDbContext _context;
        public AboutRightController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "About Right";
            return View(await _context.AboutRights.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New About Right";
            return View(new AdminAboutRightCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminAboutRightCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string imageName = "about_1.jpg";
            if (vm.RightImage != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                imageName = Guid.NewGuid() + Path.GetExtension(vm.RightImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, imageName), FileMode.Create);
                await vm.RightImage.CopyToAsync(s);
            }

            await _context.AboutRights.AddAsync(new AboutRight
            {
                Title = vm.Title, Description = vm.Description, RightImage = imageName
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "About Right created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit About Right";
            var a = await _context.AboutRights.FindAsync(id);
            if (a == null) return NotFound();
            return View(new AdminAboutRightEditVM
            {
                Id = a.Id, Title = a.Title,
                Description = a.Description, ExistingImage = a.RightImage
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminAboutRightEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var a = await _context.AboutRights.FindAsync(vm.Id);
            if (a == null) return NotFound();
            a.Title = vm.Title; a.Description = vm.Description;
            if (vm.RightImage != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                var name = Guid.NewGuid() + Path.GetExtension(vm.RightImage.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.RightImage.CopyToAsync(s);
                a.RightImage = name;
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "About Right updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _context.AboutRights.FindAsync(id);
            if (a != null) { _context.AboutRights.Remove(a); await _context.SaveChangesAsync(); }
            TempData["Success"] = "About Right deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
