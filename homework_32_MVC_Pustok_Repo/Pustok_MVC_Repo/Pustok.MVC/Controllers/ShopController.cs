using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Enums;
using Pustok.Core.Models;
using Pustok.Core.Repositories;
using Pustok.Data.DAL;
using Pustok.MVC.ViewModels;

namespace Pustok.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly IBookImageRepository _bookImageRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        public ShopController(IBookService bookService,
            IGenreService genreService,
            IAuthorService authorService,
            IBookImageRepository bookImageRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
                        AppDbContext appDbContext)
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
            _bookImageRepository = bookImageRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            List<CheckoutVM> checkoutVMs = new List<CheckoutVM>();



            AppUser appUser = null;
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            string basketItemsStr = HttpContext.Request.Cookies["Items"];
            List<BasketItem> userBasketItems = [];

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (appUser is null)
            {
                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);
                    checkoutVMs = basketItems.Select(bi => new CheckoutVM()
                    {
                        Book = _bookService.GetByIdAsync(bi.BookId).Result,
                        Count = bi.Count
                    }).ToList();

                }
            }
            else
            {
                userBasketItems = await _appDbContext.BasketItems.Include(x => x.Book).Where(x => x.AppUserId == appUser.Id && x.IsDeleted == false).ToListAsync();
                checkoutVMs = userBasketItems.Select(ubi => new CheckoutVM { Book = ubi.Book, Count = ubi.Count }).ToList();
            }


            OrderVM orderVM = new OrderVM()
            {
                CheckoutVMs = checkoutVMs,
                EmailAddress = appUser?.Email,
                Fullname = appUser?.FullName,
                Phone = appUser?.PhoneNumber
            };

            return View(orderVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM vm)
        {
            AppUser appUser = null;

            List<CheckoutVM> checkoutVMs = new List<CheckoutVM>();
            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            string basketItemsStr = HttpContext.Request.Cookies["Items"];
            List<BasketItem> userBasketItems = [];


            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }


            Order order = new Order()
            {
                Fullname = vm.Fullname,
                Phone = vm.Phone,
                EmailAddress = vm.EmailAddress,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Note = vm.Note,
                ZipCode = vm.ZipCode,
                Country = vm.Country,
                IsDeleted = false,
                Address = vm.Address,
                AppUserId = appUser?.Id,
                OrderItems = [],
                OrderStatus = OrderStatus.Pending,
                City = vm.City,
                TotalPrice = 0
            };




            if (appUser is null)
            {
                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemVM>>(basketItemsStr);


                    foreach (var item in basketItems)
                    {

                        Book book = await _bookService.GetByIdAsync(item.BookId);

                        OrderItem orderItem = new()
                        {

                            Title = book.Title,
                            CostPrice = book.CostPrice,
                            BookId = book.Id,
                            SalePrice = book.SalePrice,
                            DiscountPercent = book.DiscountPercent,
                            Count = item.Count,
                            Order = order,
                            PriceAfterDiscount = book.PriceAfterDiscount,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            IsDeleted = false,
                        };

                        order.TotalPrice += orderItem.PriceAfterDiscount * orderItem.Count;
                        order.OrderItems.Add(orderItem);

                    }


                }
            }
            else
            {
                userBasketItems = await _appDbContext.BasketItems.Include(x => x.Book).Where(x => x.AppUserId == appUser.Id && x.IsDeleted == false).ToListAsync();

                foreach (var item in userBasketItems)
                {
                    Book book = await _bookService.GetByIdAsync(item.BookId);

                    OrderItem orderItem = new()
                    {

                        Title = book.Title,
                        CostPrice = book.CostPrice,
                        BookId = book.Id,
                        SalePrice = book.SalePrice,
                        DiscountPercent = book.DiscountPercent,
                        Count = item.Count,
                        Order = order,
                        PriceAfterDiscount = book.PriceAfterDiscount,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    order.TotalPrice += orderItem.PriceAfterDiscount * orderItem.Count;
                    order.OrderItems.Add(orderItem);
                }
            }


            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
