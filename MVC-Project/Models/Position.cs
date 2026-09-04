using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
