using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyBiome.Models
{
        public class OrderItems
        {
            [Key]
            public int Id { get; set; }

		    [ForeignKey("Orders")]
		    public int OrderId { get; set; }

         
		    [ForeignKey("ProductSize")]
		    public int ProductSizeId { get; set; }

            public int Quantity { get; set; }

            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
            [Column(TypeName = "decimal(8, 2)")]
            public decimal Price { get; set; }

            public Orders Orders { get; set; }
            public ProductSize ProductSize { get; set; }
            
    }
}
