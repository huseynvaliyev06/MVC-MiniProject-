
using MVC_Project.ViewModels.Item;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemUIVM>> GetAllUIAsync();
    }
}