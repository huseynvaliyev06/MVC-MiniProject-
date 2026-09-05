using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index(int id)
        {
            return View(id);
        }
    }
}
