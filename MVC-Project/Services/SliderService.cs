using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SliderUIVM>> GetAllUIAsync()
        {
            var sliders = await _context.Sliders.Select(m => new SliderUIVM
            {
                SliderBanner = m.SliderBanner,
                Logo = m.Logo,
                Title = m.Title,
                Description = m.Description,
            }).ToListAsync();

            return sliders;
        }
    }
}
