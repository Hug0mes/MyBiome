using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Favorites
        {
         
                [Key]
                public long Id { get; set; }

		        [ForeignKey("Costumer")]
		        public long CostumerId { get; set; }

		        [ForeignKey("ProductSize")]
		        public long ProductSizeId { get; set; }
       
                public Customers Customers { get; set; }
                public ProductSize productSize { get; set; }

        }
}
