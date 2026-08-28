using Microsoft.AspNetCore.Mvc;

namespace MVC_Project.Controllers
{
    public class CourseController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
