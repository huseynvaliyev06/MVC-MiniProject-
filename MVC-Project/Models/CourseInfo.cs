using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class CourseInfo : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeatured { get; set; }
        public string AuthorImage { get; set; }
        public string Tag { get; set; }
        public string MainImage { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseImage> CourseImages { get; set; }
    }
}
