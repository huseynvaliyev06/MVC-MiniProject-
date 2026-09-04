using MVC_MiniProject.ViewModels.Courses;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseInformationUIVM>> GetAllUIAsync();
        Task<CourseInformationUIVM> GetByIdAsync(int id);
        Task<IEnumerable<CourseInformationUIVM>> SearchAsync(string searchText);
        Task<IEnumerable<CoursesVM>> GetAllAsync();
        Task CreateAsync(CoursesCreateVM model);
        Task<bool> ExistAsync(string name);
        Task DeleteAsync(int id);
        Task<CoursesEditVM> GetForEditAsync(int id);
        Task UpdateAsync(CoursesEditVM model);
        Task<CoursesDetailVM> GetDetailAsync(int id);
    }
}
