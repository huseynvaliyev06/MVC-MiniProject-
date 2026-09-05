using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniProject.Controllers
{
    public class CourseController : Controller
    {
        public async Task<IActionResult> Index(string searchText)
        {
            return View(model: searchText);
        }
    }
}
