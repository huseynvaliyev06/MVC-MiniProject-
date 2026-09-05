using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class SliderViewComponent :ViewComponent
    {
        private readonly ISliderService _sliderViewService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderViewService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderViewService.GetAllUIAsync();
            return View(sliders);
        }
    }
}
