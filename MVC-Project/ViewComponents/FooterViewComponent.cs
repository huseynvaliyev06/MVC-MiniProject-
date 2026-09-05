using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingFooterService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingFooterService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingFooterService.GetAllUIAsync();
            return View(setting);
        }
    }
}
