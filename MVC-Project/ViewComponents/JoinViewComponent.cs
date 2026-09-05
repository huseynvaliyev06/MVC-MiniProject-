using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Setting;

namespace MVC_MiniProject.ViewComponents
{
    public class JoinViewComponent : ViewComponent
    {
        private readonly ISettingService _settingJoinService;
        public JoinViewComponent(ISettingService settingService)
        {
            _settingJoinService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingJoinService.GetAllUIAsync();
            return View(new SettingVM
            {
                Settings = settings
            });
        }
    }
}
