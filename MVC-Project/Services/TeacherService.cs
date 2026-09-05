using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Teachers.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<TeacherUIVM>> GetAllUIAsync()
        {
            var teachers = await _context.Teachers.Include(m => m.Position).Select(m => new TeacherUIVM
            {
                FullName = m.FullName,
                Image = m.Image,
                TeacherPosition = m.Position.Name
            }).ToListAsync();
            return teachers;
        }
    }
}
