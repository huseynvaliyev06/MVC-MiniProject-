using MVC_MiniProject.ViewModels.Events;
using MVC_MiniProject.ViewModels.News;


namespace MVC_MiniProject.ViewModels.EventsAndNews
{
    public class EventsAndNewsVM
    {
        public IEnumerable<EventUIVM> Events { get; set; }
        public IEnumerable<NewsUIVM> News { get; set; }
    }
}
