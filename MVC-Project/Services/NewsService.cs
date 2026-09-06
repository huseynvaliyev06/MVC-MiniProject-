using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;
        public NewsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsUIVM>> GetAllUIAsync()
        {
            var news = await _context.News.Include(m => m.Author).Select(m => new NewsUIVM
            {
                Date = m.Date,
                Description = m.Description,
                AuthorName = m.Author.FullName,
                Image = m.Image,
                Title = m.Title
            }).ToListAsync();

            return news;
        }
    }
}
