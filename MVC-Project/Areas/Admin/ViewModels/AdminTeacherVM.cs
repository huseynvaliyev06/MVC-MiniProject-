namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminTeacherIndexVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string PositionName { get; set; }
    }

    public class AdminTeacherCreateVM
    {
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class AdminTeacherEditVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? Image { get; set; }
    }
}
