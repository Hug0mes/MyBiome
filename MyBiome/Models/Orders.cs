using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Orders
        {

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

		[ForeignKey("Customers")]
		public long CustomersId { get; set; }

        public string OrderDate { get; set; }

		[ForeignKey("Shippers")]
		public long ShipperId { get; set; }
         
        public int TotalAmount { get; set; }

        public string OrderStatus { get; set; }


        public Customers Customers { get; set; }

		public Shippers Shippers { get; set; }

        public OrderItems OrderItems { get; set; }

	}
}
