using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBiome.Models;
using MyBiome.Infrastructure;
using MyBiome.Models.ViewModels;

namespace MyBiome.Helpers
{ 
    public class DBHelper
    {

        static public IEnumerable<SelectListItem> FillCategories(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
                IEnumerable<SelectListItem> listaCategories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Name
                    }).ToList();
            return listaCategories;
        }

        static public IEnumerable<SelectListItem> FillSubCategories(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill SubCategorias List
            IEnumerable<SelectListItem> listSubCategories = context.SubCategories
            .OrderBy(c => c.Name)
            .Select(c =>
                new SelectListItem
                {
                    Value = Convert.ToString(c.Id),
                    Text = c.Name
                }).ToList();
            return listSubCategories;
        }
    }
}
