
using MVC_Project.ViewModels.Videos;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IVideoService
    {
        Task<VideoUIVM> GetAllUIAsync();
    }
}