using System;
using System.Collections.Generic;
using Spice.Models;

namespace Spice.Areas.Admin.ViewModel
{
    public class SubCategoryAndCategoryVM
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<string> SubCategoryList { get; set; }
        public string StatusMessage { get; set; }
    }
}
