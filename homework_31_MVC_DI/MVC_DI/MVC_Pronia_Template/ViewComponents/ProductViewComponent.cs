using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;
using MVC_Pronia_Template.Utilities.Enums;
using NuGet.Protocol;

namespace MVC_Pronia_Template.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(SortType type)
        {
            IQueryable<Product> query = _context.Products.Where(p => !p.IsDeleted);
            switch (type)
            {
                case SortType.Name:
                    query= query.OrderBy(p => p.Name);
                    break;
                case SortType.Price:
                    query=query.OrderByDescending(p => p.Price);
                    break;
                case SortType.Newest:
                    query = query.OrderByDescending(p => p.CreateAt);
                    break;
            }

            query = query.Take(8).Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null));
            return View(await query.ToListAsync());
        }
    }
}
