namespace MVC_MiniProject.ViewModels.Courses
{
    public class CoursesDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public string MainImage { get; set; }
        public List<string> DetailImages { get; set; }
        public int TeacherId { get; set; }
        public string TeacherFullName { get; set; }
        public string TeacherPosition { get; set; }
        public string TeacherPhoto { get; set; }
    }
}
