using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Favorites
        {
         
                [Key]
                public int Id { get; set; }

		        [ForeignKey("Costumer")]
		        public int CostumerId { get; set; }

		         [ForeignKey("Product")]
		        public int ProductId { get; set; }
       
                public Customers Customers { get; set; }
               

        }
}
