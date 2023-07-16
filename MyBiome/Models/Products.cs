  using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyBiome.Infrastructure.Validation;

namespace MyBiome.Models
{
        public class Products
        {
        [Key]
        public int Id { get; set; }
         
        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 2")]
        public string Description { get; set; }

        [Required]     
        public int Price { get; set; }

        public string Status { get; set; }

        public string Height { get; set; }

        public string Whidh { get; set; }

        public int  Stock { get; set; }

        public string Image1 { get; set; } = "noimage.png";

        public string Image2 { get; set; } = "noimage.png";

        public string Image3 { get; set; } = "noimage.png";

    
        public int SubCategoryId { get; set; }

        public SubCategory? SubCategory { get; set; }

        [NotMapped]
        public List<SubCategory>? SubCategories { get; set; }
        public List<Favorites>? Favorites { get; set; }
    }
}
