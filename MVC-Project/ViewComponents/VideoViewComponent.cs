using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class VideoViewComponent :ViewComponent
    {
        private readonly IVideoService _videoViewService;
        public VideoViewComponent(IVideoService videoService)
        {
            _videoViewService = videoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _videoViewService.GetAllUIAsync();
            return View(sliders);
        }
    }
}
