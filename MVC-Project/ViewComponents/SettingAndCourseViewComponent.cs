using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.SettingAndCourse;

namespace MVC_MiniProject.ViewComponents
{
    public class SettingAndCourseViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        public SettingAndCourseViewComponent(ISettingService settingService, ICourseService courseService)
        {
            _settingService = settingService;
            _courseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchText = null)
        {
            var settings = await _settingService.GetAllUIAsync();
            var courses = await _courseService.SearchAsync(searchText);
            return View(new SettingAndCourseVM
            {
                Settings = settings,
                CourseInfos = courses
            });
        }
    }
}
