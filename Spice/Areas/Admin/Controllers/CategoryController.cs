using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Areas.Admin.Services;
using Spice.Areas.Admin.ViewModel;
using Spice.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper  _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> category = await _categoryService.ShowAlls();
            
            var categoryResult = _mapper.Map<List<CategoryVM>>(category);
            
            return View(categoryResult);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Store(createCategoryVM);
                
                return RedirectToAction(nameof(Index));
            }

            return View(createCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if(categoryId is null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.FindById(categoryId);

            if(category is null)
            {
                return NotFound();
            }

            var updateCategoryVM = _mapper.Map<UpdateCategoryVM>(category);

            return View(updateCategoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoryVM updateCategoryVM)
        {

            if (!ModelState.IsValid)
            {
                return View(updateCategoryVM);
            }

            Category category = await _categoryService.FindById(id);

            if(category is null)
            {
                return NotFound();
            }

            _mapper.Map(updateCategoryVM, category);

            await _categoryService.Update(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? categoryId)
        {
            if(categoryId is null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.FindById(categoryId);

            if (category is null)
            {
                return NotFound();
            }

            var categoryResult = _mapper.Map<CategoryVM>(category);

            return View(categoryResult);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.FindById(id);


            if(category is null)
            {
                return NotFound();
            }

            var categoryResult = _mapper.Map<CategoryVM>(category);

            return View(categoryResult);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? categoryId)
        {
            Category category = await _categoryService.FindById(categoryId);

            if(category is null)
            {
                return NotFound();
            }

             await _categoryService.Delete(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
