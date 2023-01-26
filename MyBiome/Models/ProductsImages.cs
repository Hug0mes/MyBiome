using MyBiome.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyBiome.Models
{
        public class ProductsImages
        {
                public long Id { get; set; }

        public long ProductId { get; set; }

        public string Image { get; set; } = "noimage.png";

        public int mumber { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
