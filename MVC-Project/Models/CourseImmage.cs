using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class CourseImage : BaseEntity
    {
        public string CourseImg { get; set; }
        public bool IsMain { get; set; }
        public int CourseInfoId { get; set; }
        public CourseInfo CourseInfo { get; set; }
    }
}
