using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherUIVM>> GetAllUIAsync();
        Task<bool> ExistAsync(int id);
    }
}
