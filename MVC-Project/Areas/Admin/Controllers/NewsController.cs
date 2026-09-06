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
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        public NewsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "News";
            var list = await _context.News.Include(n => n.Author)
                .Select(n => new AdminNewsIndexVM
                {
                    Id = n.Id, Date = n.Date, Description = n.Description,
                    Image = n.Image, AuthorName = n.Author.FullName
                }).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "New News";
            await PopulateAuthors();
            return View(new AdminNewsCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminNewsCreateVM vm)
        {
            if (!ModelState.IsValid) { await PopulateAuthors(vm.AuthorId); return View(vm); }

            string imageName = "default.jpg";
            if (vm.Image != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                imageName = Guid.NewGuid() + Path.GetExtension(vm.Image.FileName);
                await using var s = new FileStream(Path.Combine(folder, imageName), FileMode.Create);
                await vm.Image.CopyToAsync(s);
            }

            await _context.News.AddAsync(new News
            {
                Date = vm.Date, Description = vm.Description,
                AuthorId = vm.AuthorId, Image = imageName
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "News created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit News";
            var n = await _context.News.FindAsync(id);
            if (n == null) return NotFound();
            await PopulateAuthors(n.AuthorId);
            return View(new AdminNewsEditVM
            {
                Id = n.Id, Date = n.Date, Description = n.Description,
                AuthorId = n.AuthorId, ExistingImage = n.Image
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminNewsEditVM vm)
        {
            if (!ModelState.IsValid) { await PopulateAuthors(vm.AuthorId); return View(vm); }

            var n = await _context.News.FindAsync(vm.Id);
            if (n == null) return NotFound();

            n.Date = vm.Date; n.Description = vm.Description; n.AuthorId = vm.AuthorId;

            if (vm.Image != null)
            {
                string folder = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(folder);
                var name = Guid.NewGuid() + Path.GetExtension(vm.Image.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.Image.CopyToAsync(s);
                n.Image = name;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "News updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var n = await _context.News.FindAsync(id);
            if (n != null) { _context.News.Remove(n); await _context.SaveChangesAsync(); }
            TempData["Success"] = "News deleted.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateAuthors(int selected = 0)
        {
            var authors = await _context.Authors.ToListAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName", selected);
        }
    }
}
