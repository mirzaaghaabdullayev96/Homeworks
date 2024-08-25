using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Services.Interfaces;

namespace MVC_Pronia_Template.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            return await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
        }
    }
}
