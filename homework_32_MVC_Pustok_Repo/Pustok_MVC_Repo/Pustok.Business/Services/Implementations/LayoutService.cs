using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LayoutService(IGenreRepository genreRepository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _genreRepository = genreRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await _genreRepository.GetAll().ToListAsync();
        }

        public async Task<AppUser> GetUser(string username)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(username);

            return user;
        }
    }
}
