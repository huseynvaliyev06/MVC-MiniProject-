using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Areas.Admin.ViewModels;
using MVC_MiniProject.Data;
using MVC_Project.Models;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ItemController : Controller
    {
        private readonly AppDbContext _context;
        public ItemController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Items";
            return View(await _context.Items.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Item";
            return View(new AdminItemCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Items.AddAsync(new Item { Name = vm.Name });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Item created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Item";
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();
            return View(new AdminItemEditVM { Id = item.Id, Name = item.Name });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var item = await _context.Items.FindAsync(vm.Id);
            if (item == null) return NotFound();
            item.Name = vm.Name;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Item updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null) { _context.Items.Remove(item); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Item deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
