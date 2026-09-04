
using MVC_Project.ViewModels.AboutRights;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutRightService
    {
        Task<AboutRightUIVM> GetAllUIAsync();
    }
}