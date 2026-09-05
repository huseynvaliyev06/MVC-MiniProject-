using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutRights;

namespace MVC_MiniProject.Services
{
    public class AboutRightService : IAboutRightService
    {
        private readonly AppDbContext _context;
        public AboutRightService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AboutRightUIVM> GetAllUIAsync()
        {
            var rights = await _context.AboutRights.OrderByDescending(m => m.Id).Select(m => new AboutRightUIVM
            {
                Title = m.Title,
                Description = m.Description,
                RightImage = m.RightImage
            }).FirstOrDefaultAsync();

            return rights;
        }
    }
}
