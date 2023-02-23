using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class Category
        {
                [Key]
                public int Id { get; set; }
                public string Name { get; set; }

        [ValidateNever]
                public CategoriesProduct CategoriesProduct { get; set; }

        internal static bool CategoryExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
