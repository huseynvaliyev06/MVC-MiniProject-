using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingHeaderService;
        public HeaderViewComponent(ISettingService settingService)
        {
            _settingHeaderService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingHeaderService.GetAllUIAsync();
            return View(setting);
        }
    }
}