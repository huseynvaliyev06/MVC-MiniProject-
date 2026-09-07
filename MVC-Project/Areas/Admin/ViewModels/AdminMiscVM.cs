namespace MVC_MiniProject.Areas.Admin.ViewModels
{
    public class AdminPositionCreateVM { public string Name { get; set; } }
    public class AdminPositionEditVM   { public int Id { get; set; } public string Name { get; set; } }

    public class AdminItemCreateVM { public string Image { get; set; } }
    public class AdminItemEditVM   { public int Id { get; set; } public string Image { get; set; } }

    public class AdminAuthorCreateVM { public string FullName { get; set; } }
    public class AdminAuthorEditVM   { public int Id { get; set; } public string FullName { get; set; } }

    public class AdminSettingEditVM  { public int Id { get; set; } public string Key { get; set; } public string Value { get; set; } }
    public class AdminSettingCreateVM{ public string Key { get; set; } public string Value { get; set; } }
}
