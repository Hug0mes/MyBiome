using MyBiome.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyBiome.Models
{
        public class ProductsImages
        {
        [Key]
        public long Id { get; set; }

		[ForeignKey("Products")]
		public long ProductId { get; set; }

        public string Image { get; set; } = "noimage.png";
         
        public int mumber { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        public Products Products { get; set; }  
    }
}
