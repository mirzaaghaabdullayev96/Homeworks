using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _context.Sizes.Where(c => !c.IsDeleted).Include(c => c.ProductSizes).ToListAsync();
            return View(sizes);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSizeVM sizeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Sizes.AnyAsync(c => c.Name.Trim() == sizeVM.Name.Trim());

            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View();
            }

            Size size = new()
            {
                Name = sizeVM.Name,
                IsDeleted = false,
                CreateAt = DateTime.Now
            };

            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            Size existed = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            existed.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Size size = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);

            if (size == null) return NotFound();
            return View(size);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSizeVM sizeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null || id <= 0) return BadRequest();

            Size existed = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);

            if (existed == null) return NotFound();

            bool result = await _context.Sizes.AnyAsync(c => c.Name == sizeVM.Name && c.Id != id);

            if (result)
            {
                ModelState.AddModelError("Name", $"{sizeVM.Name} named Size already exists");
                return View();
            }

            existed.Name = sizeVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Sizes.AnyAsync(c => c.Name.Trim() == size.Name.Trim());

            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View();
            }

            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}
