using System;
using System.ComponentModel.DataAnnotations;
using Spice.App.Helpers;

namespace Spice.Areas.Admin.ViewModel
{
    public class UpdateCategoryVM
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
