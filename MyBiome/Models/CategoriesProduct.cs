using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
    public class CategoriesProduct
        {
		
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        
		[ForeignKey("Category")]
		public long CategoryId { get; set; }
		
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		public Products Products { get; set; }
		public Category Category { get; set; }
		 
	}
}
