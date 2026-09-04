
using MVC_Project.ViewModels.Sliders;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderUIVM>> GetAllUIAsync();
    }
}