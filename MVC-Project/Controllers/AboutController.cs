using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;
using MVC_MiniProject.ViewModels.AboutLefts;
using MVC_MiniProject.ViewModels.AboutRights;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
