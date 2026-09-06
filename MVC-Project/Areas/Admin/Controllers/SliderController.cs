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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Sliders";
            return View(await _context.Sliders.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Slider";
            return View(new AdminSliderCreateVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string folder = Path.Combine("wwwroot", "images");
            Directory.CreateDirectory(folder);

            string bannerName = "default.jpg", logoName = "logo.png";

            if (vm.SliderBanner != null)
            {
                bannerName = Guid.NewGuid() + Path.GetExtension(vm.SliderBanner.FileName);
                await using var s = new FileStream(Path.Combine(folder, bannerName), FileMode.Create);
                await vm.SliderBanner.CopyToAsync(s);
            }
            if (vm.Logo != null)
            {
                logoName = Guid.NewGuid() + Path.GetExtension(vm.Logo.FileName);
                await using var s = new FileStream(Path.Combine(folder, logoName), FileMode.Create);
                await vm.Logo.CopyToAsync(s);
            }

            await _context.Sliders.AddAsync(new Slider
            {
                Title = vm.Title, Description = vm.Description,
                SliderBanner = bannerName, Logo = logoName
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Slider created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Slider";
            var s = await _context.Sliders.FindAsync(id);
            if (s == null) return NotFound();
            return View(new AdminSliderEditVM
            {
                Id = s.Id, Title = s.Title, Description = s.Description,
                ExistingBanner = s.SliderBanner, ExistingLogo = s.Logo
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminSliderEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var slider = await _context.Sliders.FindAsync(vm.Id);
            if (slider == null) return NotFound();

            slider.Title = vm.Title; slider.Description = vm.Description;

            string folder = Path.Combine("wwwroot", "images");
            Directory.CreateDirectory(folder);

            if (vm.SliderBanner != null)
            {
                var name = Guid.NewGuid() + Path.GetExtension(vm.SliderBanner.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.SliderBanner.CopyToAsync(s);
                slider.SliderBanner = name;
            }
            if (vm.Logo != null)
            {
                var name = Guid.NewGuid() + Path.GetExtension(vm.Logo.FileName);
                await using var s = new FileStream(Path.Combine(folder, name), FileMode.Create);
                await vm.Logo.CopyToAsync(s);
                slider.Logo = name;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Slider updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _context.Sliders.FindAsync(id);
            if (s != null) { _context.Sliders.Remove(s); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Slider deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
