namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminNewsIndexVM
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AuthorName { get; set; }
    }

    public class AdminNewsCreateVM
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class AdminNewsEditVM
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? Image { get; set; }
    }
}
