using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Orders
        {

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

		[ForeignKey("Customers")]
		public int CustomersId { get; set; }

		[ForeignKey("Shippers")]
		public int ShipperId { get; set; }

        public string OrderDate { get; set; }

        public int TotalAmount { get; set; }

        public string OrderStatus { get; set; }




        public Customers Customers { get; set; }

		public Shippers Shippers { get; set; }

        public OrderItems OrderItems { get; set; }

	}
}
