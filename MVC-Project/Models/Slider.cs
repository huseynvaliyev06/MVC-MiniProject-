using MVC_Project.Models;

namespace MVC_MiniProject.Models
{
    public class Slider : BaseEntity
    {
        public string SliderBanner { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
