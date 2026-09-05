using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.CoursesAndSetting;

namespace MVC_MiniProject.ViewComponents
{
    public class CoursesAndSettingViewComponent :ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _coruseService;
        public CoursesAndSettingViewComponent(ISettingService settingService, ICourseService courseService)
        {
            _settingService = settingService;
            _coruseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingService.GetAllUIAsync();
            var courses = await _coruseService.GetAllUIAsync();
            return View(new CoursesAndSettingVM
            {
                Settings = settings,
                CoursesInformation = courses,
            });
        }
    }
}
