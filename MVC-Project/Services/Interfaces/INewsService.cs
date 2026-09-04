
using MVC_Project.ViewModels.News;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsUIVM>> GetAllUIAsync();
    }
}