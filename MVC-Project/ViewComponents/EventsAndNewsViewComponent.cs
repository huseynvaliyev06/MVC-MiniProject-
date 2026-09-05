using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.EventsAndNews;

namespace MVC_MiniProject.ViewComponents
{
    public class EventsAndNewsViewComponent :ViewComponent
    {
        private readonly IEventService _eventService;
        private readonly INewsService _newsService;
        public EventsAndNewsViewComponent(IEventService eventService, INewsService newsService)
        {
            _eventService = eventService;
            _newsService = newsService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var even = await _eventService.GetAllUIAsync();
            var news = await _newsService.GetAllUIAsync();
            return View(new EventsAndNewsVM
            {
                Events = even,
                News = news
            });
        }
    }
}
