using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsUIVM>> GetAllUIAsync();
    }
}
