using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.ViewModels.SettingAndCourse
{
    public class SettingAndCourseVM
    {
        public Dictionary<string, string> Settings { get; set; }
        public IEnumerable<CourseInformationUIVM> CourseInfos { get; set; }
    }
}
