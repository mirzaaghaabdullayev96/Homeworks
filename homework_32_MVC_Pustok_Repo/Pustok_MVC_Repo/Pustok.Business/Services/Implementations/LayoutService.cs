using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using Pustok.Data.DAL;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly IDatabase _database;
        private readonly IGenreRepository _genreRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public LayoutService(IGenreRepository genreRepository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            AppDbContext appDbContext,
            IConnectionMultiplexer redis)
        {
            _genreRepository = genreRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _database=redis.GetDatabase();
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await _genreRepository.GetAll().ToListAsync();
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            //return await _appDbContext.Settings.ToDictionaryAsync(x => x.Key, c => c.Value);
            var hashEntries = await _database.HashGetAllAsync("Settings");
            return hashEntries.ToDictionary(entry => (string)entry.Name, entry => (string)entry.Value);
        }

        public async Task<AppUser> GetUser(string username)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(username);

            return user;
        }

    }
}
