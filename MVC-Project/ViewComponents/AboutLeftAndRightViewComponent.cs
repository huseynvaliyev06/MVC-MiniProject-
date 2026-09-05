using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutLeftAndRight;

namespace MVC_MiniProject.ViewComponents
{
    public class AboutLeftAndRightViewComponent : ViewComponent
    {
        private readonly IAboutLeftService _aboutLeftService;
        private readonly IAboutRightService _aboutRightService;
        public AboutLeftAndRightViewComponent(IAboutLeftService aboutLeftService, IAboutRightService aboutRightService)
        {
            _aboutLeftService = aboutLeftService;
            _aboutRightService = aboutRightService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutLeft = await _aboutLeftService.GetAllUIAsync();
            var aboutRight = await _aboutRightService.GetAllUIAsync();
            return View(new AboutLeftAndRightVM
            {
                AboutLeft = aboutLeft,
                AboutRight = aboutRight
            });
        }
    }
}
