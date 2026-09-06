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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        public SettingController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Settings";
            return View(await _context.Settings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Setting";
            return View(new AdminSettingCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSettingCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Settings.AddAsync(new Setting { Key = vm.Key, Value = vm.Value });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Setting created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Setting";
            var s = await _context.Settings.FindAsync(id);
            if (s == null) return NotFound();
            return View(new AdminSettingEditVM { Id = s.Id, Key = s.Key, Value = s.Value });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminSettingEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var s = await _context.Settings.FindAsync(vm.Id);
            if (s == null) return NotFound();
            s.Key = vm.Key; s.Value = vm.Value;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Setting updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _context.Settings.FindAsync(id);
            if (s != null) { _context.Settings.Remove(s); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Setting deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
