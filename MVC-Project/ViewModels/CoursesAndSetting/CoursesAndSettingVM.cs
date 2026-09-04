using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.ViewModels.CoursesAndSetting
{
    public class CoursesAndSettingVM
    {
        public IEnumerable<CourseInformationUIVM> CoursesInformation { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
