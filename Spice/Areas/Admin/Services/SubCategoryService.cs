using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spice.Areas.Admin.ViewModel;
using Spice.App.Data;
using Spice.Models;

namespace Spice.Areas.Admin.Services
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryVM>> ShowAlls();
        Task<SubCategory> GetSubCategoryById(int? subCategoryId);
        Task<SubCategoryVM> GetSubAndCategoryById(int? subCategoryId);
        Task<List<string>> GetSubCategoryList();
        Task<List<SubCategory>> GetSubCategory(int? categoryId);
        Task<IEnumerable<SubCategory>> GetSubCategoryAndCategory(SubCategoryAndCategoryVM subCategoryAndCategoryVM);
        Task<SubCategory> Store(SubCategoryAndCategoryVM subCategoryAndCategoryVM);
        Task<SubCategory> Update(SubCategory subCategory, SubCategoryAndCategoryVM model);
        Task<SubCategory> Delete(SubCategory subCategory);
    }
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubCategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<SubCategoryVM>> ShowAlls()
        {
            List<SubCategoryVM> subCategoryVM = await _applicationDbContext.SubCategory
                                                .Select(s => new SubCategoryVM
                                                {
                                                    SubCategoryId = s.CategoryId,
                                                    SubCategoryName = s.SubCategoryName,
                                                    CategoryName = s.Category.CategoryName
                                                })
                                                .ToListAsync();
                                                

            return subCategoryVM;
        }


        public async Task<SubCategory> GetSubCategoryById(int? subCategoryId)
        {
            SubCategory subCategory = await _applicationDbContext.SubCategory.FirstOrDefaultAsync(s => s.SubCategoryId == subCategoryId);

            return subCategory;
        }

        public async Task<SubCategoryVM> GetSubAndCategoryById(int? subCategoryId)
        {
            SubCategoryVM subCategoryVM = await _applicationDbContext.SubCategory
                                            .Select(s => new SubCategoryVM
                                            {
                                                SubCategoryId = s.CategoryId,
                                                SubCategoryName = s.SubCategoryName,
                                                CategoryName = s.Category.CategoryName
                                            })
                                            .FirstOrDefaultAsync(s => s.SubCategoryId == subCategoryId);

            return subCategoryVM;
        }

        public async Task<List<string>> GetSubCategoryList()
        {
            List<string> subCategoryList = await _applicationDbContext.SubCategory
                                                .OrderBy(p => p.SubCategoryName)
                                                .Select(p => p.SubCategoryName)
                                                .Distinct()
                                                .ToListAsync();

            return subCategoryList;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoryAndCategory(SubCategoryAndCategoryVM subCategoryAndCategoryVM)
        {
            IEnumerable<SubCategory> subCategory = await _applicationDbContext.SubCategory
                                            .Include(s => s.Category)
                                            .Where(s => s.SubCategoryName.Equals(subCategoryAndCategoryVM.SubCategory.SubCategoryName) &&
                                                                                    s.Category.CategoryId == subCategoryAndCategoryVM.SubCategory.CategoryId).ToListAsync();

            return subCategory;
        }

        public async Task<SubCategory> Store(SubCategoryAndCategoryVM subCategoryAndCategoryVM)
        {
            _applicationDbContext.SubCategory.Add(subCategoryAndCategoryVM.SubCategory);
            await _applicationDbContext.SaveChangesAsync();

            return subCategoryAndCategoryVM.SubCategory;
        }

        public async Task<List<SubCategory>> GetSubCategory(int? categoryId)
        {
            List<SubCategory> subCategories = await (from subCategory in _applicationDbContext.SubCategory
                                               where subCategory.CategoryId == categoryId
                                               select subCategory).ToListAsync();

            return subCategories;
        }

        public async Task<SubCategory> Update(SubCategory subCategory, SubCategoryAndCategoryVM model)
        {
            subCategory.SubCategoryName = model.SubCategory.SubCategoryName;

            await _applicationDbContext.SaveChangesAsync();

            return subCategory;
        }

        public async Task<SubCategory> Delete(SubCategory subCategory)
        {
            _applicationDbContext.SubCategory.Remove(subCategory);
            await _applicationDbContext.SaveChangesAsync();

            return subCategory;
        }
    }
}
