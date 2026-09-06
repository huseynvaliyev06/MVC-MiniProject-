namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminSliderCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? SliderBanner { get; set; }
        public IFormFile? Logo { get; set; }
    }

    public class AdminSliderEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ExistingBanner { get; set; }
        public string? ExistingLogo { get; set; }
        public IFormFile? SliderBanner { get; set; }
        public IFormFile? Logo { get; set; }
    }
}
