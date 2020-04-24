using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Spice.App.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("CategoryName"),
                       Value = item.GetPropertyValue("CategoryId"),
                       Selected = item.GetPropertyValue("CategoryId").Equals(selectedValue.ToString())
                   };
        }
    }
}
