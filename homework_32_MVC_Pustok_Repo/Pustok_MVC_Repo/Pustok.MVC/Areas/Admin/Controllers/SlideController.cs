using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Utilities.Enums;
using Pustok.Business.Utilities.Extension;
using Pustok.Business.ViewModels;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {

        private readonly ISlideService _slideService;
        private readonly IMapper _mapper;

        public SlideController(ISlideService slideService, IMapper mapper)
        {
            _slideService = slideService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _slideService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slideVM.SlidePhoto.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError("SlidePhoto", "File size is not correct");
                return View();
            }

            if (!slideVM.SlidePhoto.ValidateType("image/"))
            {
                ModelState.AddModelError("SlidePhoto", "File type is not correct");
                return View();
            }



            await _slideService.CreateAsync(slideVM);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _slideService.GetByIdAsync(id) ?? throw new NullReferenceException();
            //UpdateSlideVM slideVM = new UpdateSlideVM()
            //{
            //    Title = data.Title,
            //    Subtitle = data.Subtitle,
            //    ExistedSlidePhoto = data.Image
            //};

            UpdateSlideVM slideVM = _mapper.Map<UpdateSlideVM>(data);
            return View(slideVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateSlideVM slideVM)
        {
            var data = await _slideService.GetByIdAsync(id) ?? throw new NullReferenceException();
            //slideVM.ExistedSlidePhoto = data.Image;

            if (!ModelState.IsValid)
            {
                return View(slideVM);
            }
            await _slideService.UpdateAsync(id, slideVM);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await _slideService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
