using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
    public class CategoriesProduct
        {

        public long Id { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Products Products { get; set; }
        public int ProductId { get; set; }
    }
}
