using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Items;

namespace MVC_MiniProject.Services
{
    public class ItemService : IItemService
    {
        private AppDbContext _context;
        public ItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemUIVM>> GetAllUIAsync()
        {
            return await _context.Items.Select(m => new ItemUIVM
            {
                Image = m.Image
            }).ToListAsync();
        }
    }
}
