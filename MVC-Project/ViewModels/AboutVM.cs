using MVC_MiniProject.ViewModels.AboutLefts;
using MVC_MiniProject.ViewModels.AboutRights;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.ViewModels
{
    public class AboutVM
    {
        public IEnumerable<TeacherUIVM> Teachers { get; set; }
    }
}
