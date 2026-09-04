
using MVC_Project.ViewModels.Teachers;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherUIVM>> GetAllUIAsync();
    }
}