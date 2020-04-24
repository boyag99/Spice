using System;
using System.ComponentModel.DataAnnotations;

namespace Spice.Areas.Admin.ViewModel
{
    public class CreateCategoryVM
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
