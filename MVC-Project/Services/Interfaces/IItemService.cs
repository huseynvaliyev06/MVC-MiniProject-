using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Items;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemUIVM>> GetAllUIAsync();
    }
}
