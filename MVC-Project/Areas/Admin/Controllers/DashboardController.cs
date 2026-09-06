using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Dashboard";
            ViewBag.CourseCount   = await _context.Courses.CountAsync();
            ViewBag.TeacherCount  = await _context.Teachers.CountAsync();
            ViewBag.NewsCount     = await _context.News.CountAsync();
            ViewBag.EventCount    = await _context.Events.CountAsync();
            ViewBag.SliderCount   = await _context.Sliders.CountAsync();
            ViewBag.SettingCount  = await _context.Settings.CountAsync();
            return View();
        }
    }
}
