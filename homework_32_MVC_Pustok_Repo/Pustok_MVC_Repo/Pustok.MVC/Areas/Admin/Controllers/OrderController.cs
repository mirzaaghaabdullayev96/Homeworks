﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions.CommonExceptions;
using Pustok.Business.ExternalServices.Interfaces;
using Pustok.Core.Enums;
using Pustok.Data.DAL;

namespace Pustok.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;

        public OrderController(AppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            var datas = await _appDbContext.Orders.Include(x => x.OrderItems).ToListAsync();

            return View(datas);
        }



        public async Task<IActionResult> Details(int? id)
        {
            var datas = await _appDbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

            return View(datas);
        }

        public async Task<IActionResult> Accept(int? Id)
        {
            var data = await _appDbContext.Orders.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == Id) ?? throw new EntityNotFoundException("Order not found");
            data.OrderStatus = (OrderStatus)1;
            await _appDbContext.SaveChangesAsync();

            //await _emailService.SendMailAsync(data.EmailAddress, "Order confirmed", data.AppUser.FullName, "Your order has been confirmed");


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Reject(int? Id)
        {
            var data = await _appDbContext.Orders.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == Id) ?? throw new EntityNotFoundException("Order not found");
            data.OrderStatus = (OrderStatus)2;
            await _appDbContext.SaveChangesAsync();

            //await _emailService.SendMailAsync(data.EmailAddress, "Order rejected", data.AppUser.FullName, "Your order has been confirmed");

            return RedirectToAction(nameof(Index));
        }
    }
}