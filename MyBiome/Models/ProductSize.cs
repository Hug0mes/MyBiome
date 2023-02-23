using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class ProductSize
    {
                [Key]
                public int Id { get; set; }

		        [ForeignKey("Products")]
		        public int ProductID { get; set; }

                public decimal Price { get; set; }

                public int stock { get; set; }
         
                public OrderItems OrderItems { get; set; }
                public Products Products { get; set; }
    }
}
