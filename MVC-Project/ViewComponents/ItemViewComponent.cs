using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class ItemViewComponent :ViewComponent
    {
        private readonly IItemService _itemViewService;
        public ItemViewComponent(IItemService itemService)
        {
            _itemViewService = itemService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _itemViewService.GetAllUIAsync();
            return View(items);
        }
    }
}
