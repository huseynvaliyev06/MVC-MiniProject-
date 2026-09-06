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
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Authors";
            return View(await _context.Authors.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Author";
            return View(new AdminAuthorCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminAuthorCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Authors.AddAsync(new Author { FullName = vm.FullName });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Author created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Author";
            var a = await _context.Authors.FindAsync(id);
            if (a == null) return NotFound();
            return View(new AdminAuthorEditVM { Id = a.Id, FullName = a.FullName });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminAuthorEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var a = await _context.Authors.FindAsync(vm.Id);
            if (a == null) return NotFound();
            a.FullName = vm.FullName;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Author updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _context.Authors.FindAsync(id);
            if (a != null) { _context.Authors.Remove(a); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Author deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
