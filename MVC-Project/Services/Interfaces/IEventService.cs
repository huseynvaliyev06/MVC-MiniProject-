
using MVC_Project.ViewModels.Events;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventUIVM>> GetAllUIAsync();
    }
}