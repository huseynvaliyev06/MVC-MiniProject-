namespace MVC_MiniProject.ViewModels.Courses
{
    public class CoursesCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public int TeacherId { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> DetailImages { get; set; }
    }
}
