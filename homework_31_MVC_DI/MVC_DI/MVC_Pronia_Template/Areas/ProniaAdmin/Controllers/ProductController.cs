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
                .Include(p => p.ProductImages)
                .Select(p => new GetAdminProductVM
                {
                    Name = p.Name,
                    Id = p.Id,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Image = p.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true).ImageURL
                })
                .ToListAsync();
            return View(products);
        }


        public async Task<IActionResult> Create()
        {

            CreateProductVM productVM = new CreateProductVM()
            {
                Categories = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync(),
                Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync(),
                Colors = await _context.Colors.Where(p => !p.IsDeleted).ToListAsync(),
                Sizes = await _context.Sizes.Where(p => !p.IsDeleted).ToListAsync()
            };

            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            productVM.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            productVM.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            productVM.Colors = await _context.Colors.Where(p => !p.IsDeleted).ToListAsync();
            productVM.Sizes = await _context.Sizes.Where(p => !p.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            //photos check

            if (!productVM.MainPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "File type is not correct");
                return View();
            }

            if (!productVM.MainPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError("MainPhoto", "File size is not correct");
                return View();
            }


            if (!productVM.HoverPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError("HoverPhoto", "File type is not correct");
                return View();
            }

            if (!productVM.HoverPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError("HoverPhoto", "File size is not correct");
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

            if (productVM.ColorIds is not null)
            {
                bool colorResult = productVM.ColorIds.Any(tId => !productVM.Colors.Exists(t => t.Id == tId));
                if (colorResult)
                {
                    ModelState.AddModelError("ColorIds", "Color does not exist");
                    return View(productVM);
                }
            }

            if (productVM.ColorIds is not null)
            {
                bool sizeResult = productVM.SizeIds.Any(tId => !productVM.Sizes.Exists(t => t.Id == tId));
                if (sizeResult)
                {
                    ModelState.AddModelError("ColorIds", "Color does not exist");
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

            if (productVM.ColorIds is not null)
            {
                product.ProductColors = productVM.ColorIds.Select(tId => new ProductColor
                {
                    ColorId = tId
                }).ToList();
            }

            if (productVM.SizeIds is not null)
            {
                product.ProductSizes = productVM.SizeIds.Select(tId => new ProductSize
                {
                    SizeId = tId
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

            Product product = await _context.Products.Include(p => p.ProductTags).Include(p => p.ProductColors).Include(p => p.ProductImages).Include(p => p.ProductSizes).FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

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
                Images = product.ProductImages.ToList(),
                ColorIds = product.ProductColors.Select(pt => pt.ColorId).ToList(),
                Colors = await _context.Colors.Where(p => !p.IsDeleted).ToListAsync(),
                SizeIds = product.ProductSizes.Select(pt => pt.SizeId).ToList(),
                Sizes = await _context.Sizes.Where(p => !p.IsDeleted).ToListAsync(),
            };

            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM productVM)
        {
            if (id == null || id <= 0) return BadRequest();

            Product existed = await _context.Products.Include(p => p.ProductImages).Include(p => p.ProductColors).Include(p => p.ProductTags).Include(p => p.ProductSizes).FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (existed == null) return NotFound();
            productVM.Images=existed.ProductImages.ToList();
            productVM.Categories = await _context.Categories.Where(p => !p.IsDeleted).ToListAsync();
            productVM.Tags = await _context.Tags.Where(p => !p.IsDeleted).ToListAsync();
            productVM.Colors = await _context.Colors.Where(p => !p.IsDeleted).ToListAsync();
            productVM.Sizes = await _context.Sizes.Where(p => !p.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            if (productVM.MainPhoto is not null)
            {
                if (!productVM.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "File type is not correct");
                    return View(productVM);
                }

                if (!productVM.MainPhoto.ValidateSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError("MainPhoto", "File size is not correct");
                    return View(productVM);
                }
            }

            if (productVM.HoverPhoto is not null)
            {
                if (!productVM.HoverPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError("HoverPhoto", "File type is not correct");
                    return View(productVM);
                }

                if (!productVM.HoverPhoto.ValidateSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError("HoverPhoto", "File size is not correct");
                    return View(productVM);
                }
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


            if (productVM.MainPhoto is not null)
            {
                ProductImage main = new()
                {
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                    IsPrimary = true,
                    ImageURL = await productVM.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
                };

                ProductImage existedMain = existed.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true);
                existedMain.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.ProductImages.Remove(existedMain);
                existed.ProductImages.Add(main);
            }


            if (productVM.MainPhoto is not null)
            {
                ProductImage hover = new()
                {
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                    IsPrimary = false,
                    ImageURL = await productVM.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images")
                };

                ProductImage existedHover = existed.ProductImages.FirstOrDefault(pi => pi.IsPrimary == false);
                existedHover.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.ProductImages.Remove(existedHover);
                existed.ProductImages.Add(hover);
            }

            if (productVM.ImagesIds is null)
            {
                productVM.ImagesIds = [];
            }

            var deleteImages = existed.ProductImages.Where(pi => !productVM.ImagesIds.Exists(tId => tId == pi.Id) && pi.IsPrimary == null).ToList();
            foreach (var item in deleteImages)
            {
                item.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.ProductImages.Remove(item);
            }


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

                    existed.ProductImages.Add(Image);
                }

                TempData["ErrorMessage"] = text;
            }

            if (productVM.TagIds is null) existed.ProductTags = []; 

            if (productVM.TagIds is not null)
            {
                bool tagResult = productVM.TagIds.Any(tId => !productVM.Tags.Exists(t => t.Id == tId));
                if (tagResult)
                {
                    ModelState.AddModelError("TagIds", "Tag does not exist");
                    return View(productVM);
                }

                existed.ProductTags.AddRange(productVM.TagIds.Where(tId => !existed.ProductTags.Any(pt => pt.TagId == tId))
                    .Select(tId => new ProductTag { TagId = tId }));
                _context.ProductTags.RemoveRange(existed.ProductTags.Where(pt => !productVM.TagIds.Exists(tId => tId == pt.TagId)).ToList());

            }


            if (productVM.ColorIds is null) existed.ProductColors = [];

            if (productVM.ColorIds is not null)
            {
                bool colorResult = productVM.ColorIds.Any(tId => !productVM.Colors.Exists(t => t.Id == tId));
                if (colorResult)
                {
                    ModelState.AddModelError("ColorIds", "Color does not exist");
                    return View(productVM);
                }

                existed.ProductColors.AddRange(productVM.ColorIds.Where(tId => !existed.ProductColors.Any(pt => pt.ColorId == tId))
                    .Select(tId => new ProductColor { ColorId = tId }));
                _context.ProductColors.RemoveRange(existed.ProductColors.Where(pt => !productVM.ColorIds.Exists(tId => tId == pt.ColorId)).ToList());

            }


            if (productVM.SizeIds is null) existed.ProductSizes = [];

            if (productVM.SizeIds is not null)
            {
                bool sizeResult = productVM.SizeIds.Any(tId => !productVM.Sizes.Exists(t => t.Id == tId));
                if (sizeResult)
                {
                    ModelState.AddModelError("SizeIds", "Size does not exist");
                    return View(productVM);
                }

                existed.ProductSizes.AddRange(productVM.SizeIds.Where(tId => !existed.ProductSizes.Any(pt => pt.SizeId == tId))
                    .Select(tId => new ProductSize { SizeId = tId }));
                _context.ProductSizes.RemoveRange(existed.ProductSizes.Where(pt => !productVM.SizeIds.Exists(tId => tId == pt.SizeId)).ToList());

            }



            //foreach (int tagId in productVM.TagIds)
            //{
            //    if (!existed.ProductTags.Any(pt => pt.TagId == tagId))
            //    {
            //        ProductTag productTag = new() { TagId = tagId };
            //        existed.ProductTags.Add(productTag);
            //    }
            //}


            existed.Name = productVM.Name;
            existed.Description = productVM.Description;
            existed.Price = productVM.Price.Value;
            existed.SKU = productVM.SKU;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id <= 0) return BadRequest();

            Product product = await _context.Products.Include(p => p.ProductImages).Include(p => p.ProductColors).Include(p => p.ProductTags).Include(p => p.ProductSizes).FirstOrDefaultAsync(c => c.Id == id);

            if (product == null) return NotFound();

            foreach (var item in product.ProductImages)
            {
                item.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
