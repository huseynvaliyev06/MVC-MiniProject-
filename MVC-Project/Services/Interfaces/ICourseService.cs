
using MVC_Project.ViewModels.Courses;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseInformationUIVM>> GetAllUIAsync();
    }
}