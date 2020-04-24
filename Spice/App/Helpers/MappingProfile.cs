using System;
using AutoMapper;
using Spice.Areas.Admin.ViewModel;
using Spice.Models;

namespace Spice.App.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryVM>();
            CreateMap<Category, UpdateCategoryVM>();
            CreateMap<UpdateCategoryVM, Category>();
        }
    }
}