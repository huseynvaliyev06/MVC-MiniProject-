using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Videos;

namespace MVC_MiniProject.Services
{
    public class VideoService : IVideoService
    {
        private readonly AppDbContext _context;
        public VideoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VideoUIVM> GetAllUIAsync()
        {
            var videos = await _context.Videos.OrderByDescending(m => m.Id).Select(m => new VideoUIVM
            {
                Url = m.Url,
                Name = m.Name
                ,
            }).FirstOrDefaultAsync();

            return videos;
        }
    }
}
