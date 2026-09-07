namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminCourseIndexVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeatured { get; set; }
        public string Tag { get; set; }
        public string TeacherName { get; set; }
    }

    public class AdminCourseCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeatured { get; set; }
        public string AuthorImage { get; set; }
        public string Tag { get; set; }
        public string MainImage { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public IFormFile? MainImageFile { get; set; }
        public List<IFormFile>? DetailImages { get; set; }
    }

    public class AdminCourseEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeatured { get; set; }
        public string AuthorImage { get; set; }
        public string Tag { get; set; }
        public string MainImage { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public string? ExistingMainImage { get; set; }
        public IFormFile? MainImageFile { get; set; }
        public List<IFormFile>? DetailImages { get; set; }
    }
}
