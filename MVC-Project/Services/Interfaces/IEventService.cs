using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventUIVM>> GetAllUIAsync();
    }
}
