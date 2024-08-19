using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;
using MVC_Pronia_Template.ViewModels;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace MVC_Pronia_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDbConnection _dbConnection;
        public HomeController(AppDbContext appDbContext, IDbConnection dbConnection)
        {
            _appDbContext = appDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<IActionResult> Index()
        {

            HomeVM homeVM = new()
            {
                Slides = await _appDbContext.Slides.OrderBy(s => s.Order).ToListAsync(),


                Products = await _appDbContext.Products
                    .OrderByDescending(p => p.CreateAt)
                    .Take(8)
                    .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                    .ToListAsync()
            };

            //string productsQuery = "select * from Products as P\r\nJOIN ProductImages pi ON p.Id = pi.ProductId AND pi.IsPrimary IS NOT NULL\r\nOrder by P.CreateAt DESC";
            //homeVM.Products = (await _dbConnection.QueryAsync<Product>(productsQuery)).ToList();

            //string slidesQuery = "SELECT * FROM Slides ORDER BY [Order]";
            //homeVM.Slides = (await _dbConnection.QueryAsync<Slide>(slidesQuery)).ToList();




            return View(homeVM);
        }
    }
}
