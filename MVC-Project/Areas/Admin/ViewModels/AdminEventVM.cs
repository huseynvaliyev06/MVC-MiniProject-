namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminEventCreateVM
    {
        public int Date { get; set; }
        public string Month { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AdminEventEditVM
    {
        public int Id { get; set; }
        public int Date { get; set; }
        public string Month { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
