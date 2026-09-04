using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderUIVM>> GetAllUIAsync();
    }
}
