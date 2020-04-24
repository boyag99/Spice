using System;
using System.ComponentModel.DataAnnotations;

namespace Spice.Areas.Admin.ViewModel
{
    public class SubCategoryVM
    {
        public int SubCategoryId { get; set; }

        [Display(Name = "Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
