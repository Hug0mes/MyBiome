using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Favorites
        {
         
                [Key]
                public int Id { get; set; }

		       
		        public int CostumerId { get; set; }

		     
		        public int ProductId { get; set; }
       
                public AppUser? AppUser { get; set; }
                public Products? Products { get; set; }

        }
}
