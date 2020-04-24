using System;
using System.ComponentModel.DataAnnotations;

namespace Spice.Areas.Admin.ViewModel
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}