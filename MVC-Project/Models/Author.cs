using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class Author : BaseEntity
    {
        public string FullName { get; set; }
        public ICollection<News> News { get; set; }
    }
}
