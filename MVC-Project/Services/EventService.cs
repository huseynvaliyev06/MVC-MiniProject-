using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventUIVM>> GetAllUIAsync()
        {
            var events = await _context.Events.Select(m => new EventUIVM
            {
                Date = m.Date,
                Month = m.Month,
                Title = m.Title,
                Description = m.Description,
            }).ToListAsync();

            return events;
        }
    }
}
