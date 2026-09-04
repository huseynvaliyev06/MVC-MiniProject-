

using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Models;
using MVC_Project.Models;

namespace MVC_MiniProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<CourseInfo> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<AboutLeft> AboutLefts { get; set; }
        public DbSet<AboutRight> AboutRights { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
