namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminAboutLeftCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? LeftImage { get; set; }
    }

    public class AdminAboutLeftEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? LeftImage { get; set; }
    }

    public class AdminAboutRightCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? RightImage { get; set; }
    }

    public class AdminAboutRightEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? RightImage { get; set; }
    }
}
