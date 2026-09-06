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
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Positions";
            return View(await _context.Positions.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Position";
            return View(new AdminPositionCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminPositionCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Positions.AddAsync(new Position { Name = vm.Name });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Position created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Position";
            var p = await _context.Positions.FindAsync(id);
            if (p == null) return NotFound();
            return View(new AdminPositionEditVM { Id = p.Id, Name = p.Name });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminPositionEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var p = await _context.Positions.FindAsync(vm.Id);
            if (p == null) return NotFound();
            p.Name = vm.Name;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Position updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Positions.FindAsync(id);
            if (p != null) { _context.Positions.Remove(p); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Position deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
