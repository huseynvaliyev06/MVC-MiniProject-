using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class CourseImage : BaseEntity
    {
        public int CourseId { get; set; }
        public int ImageId { get; set; }
        public int AppImageId { get; set; }
        public int CourseInfoId { get; set; }
        public CourseInfo CourseInfo { get; set; }
    }
}
