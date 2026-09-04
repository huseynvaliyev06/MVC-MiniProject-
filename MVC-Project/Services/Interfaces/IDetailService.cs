using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IDetailService
    {
        Task<CourseInformationUIVM> GetByIdAsync(int id);
    }
}
