using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels;
using MVC_Pronia_Template.DAL;
using MVC_Pronia_Template.Models;
using MVC_Pronia_Template.Utilities.Enums;
using MVC_Pronia_Template.Utilities.Extension;
using NuGet.Packaging;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<GetAdminProductVM> products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary == true))
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .Select(p => new GetAdminProductVM
                {
                    Name = p.Name,
                    Id = p.Id,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Image = p.ProductImages.FirstOrDefault().ImageURL
                })
                .ToListAsync();
            return View(products);
        }


        public async Task<IActionResult> Create()
        {

            CreateProductVM productVM = new CreateProductVM()
            {
                Categories = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync(),
                Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync()
            };

            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            productVM.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            productVM.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            //photos check

            if (!productVM.MainPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }

            if (!productVM.MainPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError("Photo", "File size is not correct");
                return View();
            }


            if (!productVM.HoverPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }

            if (!productVM.HoverPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError("Photo", "File size is not correct");
                return View();
            }







            bool result = productVM.Categories.Any(c => c.Id == productVM.CategoryId && c.IsDeleted == false);

            if (!result)
            {
                ModelState.AddModelError("CategoryId", "Category does not exist");
                return View(productVM);
            }

            if (productVM.TagIds is not null)
            {
                bool tagResult = productVM.TagIds.Any(tId => !productVM.Tags.Exists(t => t.Id == tId));
                if (tagResult)
                {
                    ModelState.AddModelError("TagIds", "Tag does not exist");
                    return View(productVM);
                }
            }

            ProductImage mainImage = new()
            {
                CreateAt = DateTime.Now,
                IsDeleted = false,
                IsPrimary = true,
                ImageURL = await productVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
            };

            ProductImage hoverImage = new()
            {
                CreateAt = DateTime.Now,
                IsDeleted = false,
                IsPrimary = false,
                ImageURL = await productVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
            };




            Product product = new()
            {
                Name = productVM.Name,
                CategoryId = productVM.CategoryId.Value,
                SKU = productVM.SKU,
                Description = productVM.Description,
                Price = productVM.Price.Value,
                CreateAt = DateTime.Now,
                IsDeleted = false,
                ProductImages = new List<ProductImage>() { mainImage, hoverImage }
            };


            if (productVM.Photos is not null)
            {
                string text = string.Empty;
                foreach (IFormFile file in productVM.Photos)
                {
                    if (!file.ValidateType("image/"))
                    {
                        text += $"{file.FileName} named file type is not correct";
                        continue;
                    }

                    if (!file.ValidateSize(FileSize.MB, 1))
                    {
                        text += $"{file.FileName} named file size is not correct";
                        continue;
                    }
                    ProductImage Image = new()
                    {
                        CreateAt = DateTime.Now,
                        IsDeleted = false,
                        IsPrimary = null,
                        ImageURL = await file.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
                    };

                    product.ProductImages.Add(Image);
                }

                TempData["ErrorMessage"] = text;
            }



            if (productVM.TagIds is not null)
            {
                product.ProductTags = productVM.TagIds.Select(tId => new ProductTag
                {
                    TagId = tId
                }).ToList();
            }

            //foreach (var tId in productVM.TagIds)
            //{
            //    ProductTag pTag = new()
            //    {
            //        TagId = tId,
            //        Product = product
            //    };
            //    _context.ProductTags.Add(pTag);
            //}

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Product product = await _context.Products.Include(p => p.ProductTags).Include(p=>p.ProductImages).FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (product == null) return NotFound();

            UpdateProductVM productVM = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                TagIds = product.ProductTags.Select(pt => pt.TagId).ToList(),
                Categories = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync(),
                Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync(),
                Images=product.ProductImages.ToList()
            };

            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM productVM)
        {
            if (id == null || id <= 0) return BadRequest();

            Product existed = await _context.Products.Include(p => p.ProductTags).FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (existed == null) return NotFound();
            productVM.Categories = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync();
            productVM.Tags = await _context.Tags.Where(p => !p.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            if (existed.CategoryId != productVM.CategoryId)
            {
                bool result = productVM.Categories.Any(c => c.Id == productVM.CategoryId && c.IsDeleted == false);
                if (!result)
                {
                    ModelState.AddModelError("CategoryId", "Category does not exist");
                    return View(productVM);
                }
            }

            _context.ProductTags.RemoveRange(existed.ProductTags.Where(pt => !productVM.TagIds.Exists(tId => tId == pt.TagId)).ToList());
            existed.ProductTags.AddRange(productVM.TagIds.Where(tId => !existed.ProductTags.Any(pt => pt.TagId == tId))
                .Select(tId => new ProductTag { TagId = tId }));

            //foreach (int tagId in productVM.TagIds)
            //{
            //    if (!existed.ProductTags.Any(pt => pt.TagId == tagId))
            //    {
            //        ProductTag productTag = new() { Id = tagId };
            //        existed.ProductTags.Add(productTag);
            //    }
            //}

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
