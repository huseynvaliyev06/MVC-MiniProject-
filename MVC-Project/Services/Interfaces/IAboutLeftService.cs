
using MVC_Project.ViewModels.AboutLefts;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutLeftService
    {
        Task<AboutLeftUIVM> GetAllUIAsync();
    }
}