namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllUIAsync();
    }
}