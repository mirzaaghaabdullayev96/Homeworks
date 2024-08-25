namespace MVC_Pronia_Template.Services.Interfaces
{
    public interface ILayoutService
    {
         Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
