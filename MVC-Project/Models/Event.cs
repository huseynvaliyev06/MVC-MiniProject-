namespace MVC_Project.Models
{
    public class Event: BaseEntity
    {
        public int Date { get; set; }
        public string Month { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
