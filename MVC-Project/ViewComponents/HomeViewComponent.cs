using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Setting;

namespace MVC_MiniProject.ViewComponents
{
    public class HomeViewComponent : ViewComponent
    {
        private readonly ISettingService _settingHomeService;
        public HomeViewComponent(ISettingService settingService)
        {
            _settingHomeService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingHomeService.GetAllUIAsync();
            return View(new SettingVM
            {
                Settings = settings
            });
        }
    }
}
