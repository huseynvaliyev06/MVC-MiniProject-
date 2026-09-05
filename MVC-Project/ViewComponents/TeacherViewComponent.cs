using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly ITeacherService _teacherViewService;
        public TeacherViewComponent(ITeacherService teacherService)
        {
            _teacherViewService = teacherService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = await _teacherViewService.GetAllUIAsync();
            return View(teachers);
        }
    }
}
