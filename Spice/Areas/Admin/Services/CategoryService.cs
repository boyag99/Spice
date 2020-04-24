using System;
using System.Collections.Generic;
using Spice.App.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spice.Areas.Admin.ViewModel;
using Spice.App.Data;
using Spice.Models;

namespace Spice.Areas.Admin.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> ShowAlls();
        Task<Category> FindById(int? categoryId);
        Task<Category> Store(CreateCategoryVM createCategoryVM);
        Task<Category> Update(Category category);
        Task<Category> Delete(Category category);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Category>> ShowAlls()
        {
            List<Category> category = await _applicationDbContext.Category.ToListAsync();

            return category;
        }

        public async Task<Category> FindById(int? categoryId)
        {
            Category category = await _applicationDbContext.Category.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            return category;
        }

        public async Task<Category> Store(CreateCategoryVM createCategoryVM)
        {
            Category category = new Category(createCategoryVM.CategoryName);
            _applicationDbContext.Category.Add(category);
            await _applicationDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _applicationDbContext.Category.Update(category);
            await _applicationDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(Category category)
        {
            _applicationDbContext.Category.Remove(category);
            await _applicationDbContext.SaveChangesAsync();

            return category;
        }
    }
}
