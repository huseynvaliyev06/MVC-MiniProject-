using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutLefts;

namespace MVC_MiniProject.Services
{
    public class AboutLeftService : IAboutLeftService
    {
        private readonly AppDbContext _context;
        public AboutLeftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AboutLeftUIVM> GetAllUIAsync()
        {
            var left = await _context.AboutLefts.OrderByDescending(m => m.Id).Select(m => new AboutLeftUIVM
            {
                Title = m.Title,
                Description = m.Description,
                LeftImage = m.LeftImage
            }).FirstOrDefaultAsync();
            return left;
        }
    }
}
