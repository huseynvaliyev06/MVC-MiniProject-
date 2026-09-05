using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class DetailCourseViewComponent : ViewComponent
    {
        private readonly IDetailService _detailCourseService;
        public DetailCourseViewComponent(IDetailService detailService)
        {
            _detailCourseService = detailService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var detail = await _detailCourseService.GetByIdAsync(id);
            return View(detail);
        }
    }
}
