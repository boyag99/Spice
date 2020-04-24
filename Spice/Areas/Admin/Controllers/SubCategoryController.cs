using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spice.Areas.Admin.Services;
using Spice.Areas.Admin.ViewModel;
using Spice.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        List<SubCategoryVM> subCategoryVM = new List<SubCategoryVM>();

        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SubCategoryVM> subCategoryVM = await _subCategoryService.ShowAlls();

            if(subCategoryVM is null)
            {
                return NotFound();
            }

            return View(subCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryVM subCategoryAndCategoryVM = new SubCategoryAndCategoryVM
            {
                CategoryList = await _categoryService.ShowAlls(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _subCategoryService.GetSubCategoryList()
            };

            return View(subCategoryAndCategoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<SubCategory> doesSubCategoryExists = await _subCategoryService.GetSubCategoryAndCategory(model);
                if(doesSubCategoryExists.Count() > 0)
                {
                    StatusMessage = "Error : Sub Category exists under " + doesSubCategoryExists.First().Category.CategoryName + " category. Please use another name.";
                }
                else
                {
                    await _subCategoryService.Store(model);

                    return RedirectToAction(nameof(Index));
                }
            }

            SubCategoryAndCategoryVM subCategoryAndCategoryVM = new SubCategoryAndCategoryVM
            {
                CategoryList = await _categoryService.ShowAlls(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _subCategoryService.GetSubCategoryList(),
                StatusMessage = StatusMessage
            };

            return View(subCategoryAndCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            SubCategory subCategory = await _subCategoryService.GetSubCategoryById(id);

            if(subCategory is null)
            {
                return NotFound();
            }

            SubCategoryAndCategoryVM subCategoryAndCategoryVM = new SubCategoryAndCategoryVM
            {
                CategoryList = await _categoryService.ShowAlls(),
                SubCategory = subCategory,
                SubCategoryList = await _subCategoryService.GetSubCategoryList()
            };

            return View(subCategoryAndCategoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategoryAndCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<SubCategory> doesSubCategoryExists = await _subCategoryService.GetSubCategoryAndCategory(model);
                if(doesSubCategoryExists.Count() > 0)
                {
                    StatusMessage = "Error : Sub Category exists under " + doesSubCategoryExists.First().Category.CategoryName + " category. Please use another name.";
                }
                else
                {
                    SubCategory subCategory = await _subCategoryService.GetSubCategoryById(model.SubCategory.SubCategoryId);
                    
                    if(subCategory is null) {
                        return NotFound();
                    }

                    await _subCategoryService.Update(subCategory, model);

                    return RedirectToAction(nameof(Index));
                }
            }

            SubCategoryAndCategoryVM subCategoryAndCategoryVM = new SubCategoryAndCategoryVM
            {
                CategoryList = await _categoryService.ShowAlls(),
                SubCategory = model.SubCategory,
                SubCategoryList = await _subCategoryService.GetSubCategoryList(),
                StatusMessage = StatusMessage
            };

            return View(subCategoryAndCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            SubCategoryVM subCategoryVM = await _subCategoryService.GetSubAndCategoryById(id);

            return View(subCategoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            SubCategoryVM subCategoryVM = await _subCategoryService.GetSubAndCategoryById(id);

            if(subCategoryVM is null)
            {
                return NotFound();
            }

            return View(subCategoryVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            SubCategory subCategory = await _subCategoryService.GetSubCategoryById(id);

            if(subCategory is null)
            {
                return NotFound();
            }

            await _subCategoryService.Delete(subCategory);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int? categoryId)
        {
            List<SubCategory> subCategories = await _subCategoryService.GetSubCategory(categoryId);
            return Json(new SelectList(subCategories, "CategoryId", "SubCategoryName"));
        }
    }
}
