using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Color> colors = await _context.Colors.Where(c => !c.IsDeleted).Include(c => c.ProductColors).ToListAsync();
            return View(colors);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSizeVM colorVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Colors.AnyAsync(c => c.Name.Trim() == colorVM.Name.Trim());

            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View();
            }

            Color color = new()
            {
                Name = colorVM.Name,
                IsDeleted = false,
                CreateAt = DateTime.Now
            };

            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            Color existed = await _context.Colors.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null) return NotFound();
            existed.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Color color = await _context.Colors.FirstOrDefaultAsync(c => c.Id == id);

            if (color == null) return NotFound();
            return View(color);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateColorVM colorVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null || id <= 0) return BadRequest();

            Color existed = await _context.Colors.FirstOrDefaultAsync(c => c.Id == id);

            if (existed == null) return NotFound();

            bool result = await _context.Colors.AnyAsync(c => c.Name == colorVM.Name && c.Id != id);

            if (result)
            {
                ModelState.AddModelError("Name", $"{colorVM.Name} named Color already exists");
                return View();
            }

            existed.Name = colorVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
