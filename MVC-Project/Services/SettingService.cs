using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetAllUIAsync()
        {
            var settings = await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
            return settings;
        }
    }
}
