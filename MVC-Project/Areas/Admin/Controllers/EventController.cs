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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Events";
            return View(await _context.Events.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Event";
            return View(new AdminEventCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminEventCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _context.Events.AddAsync(new Event
            {
                Date = vm.Date, Month = vm.Month,
                Title = vm.Title, Description = vm.Description
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Event created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Event";
            var e = await _context.Events.FindAsync(id);
            if (e == null) return NotFound();
            return View(new AdminEventEditVM
            {
                Id = e.Id, Date = e.Date, Month = e.Month,
                Title = e.Title, Description = e.Description
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminEventEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var e = await _context.Events.FindAsync(vm.Id);
            if (e == null) return NotFound();
            e.Date = vm.Date; e.Month = vm.Month;
            e.Title = vm.Title; e.Description = vm.Description;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Event updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e != null) { _context.Events.Remove(e); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Event deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
