using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Setting;

namespace MVC_MiniProject.ViewComponents
{
    public class HomeCourseViewComponent : ViewComponent
    {
        private readonly ISettingService _settingHomeCourseService;
        public HomeCourseViewComponent(ISettingService settingService)
        {
            _settingHomeCourseService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingHomeCourseService.GetAllUIAsync();
            return View(new SettingVM
            {
                Settings = settings
            });
        }
    }
}
