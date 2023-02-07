namespace MyBiome.Models
{
        public class Orders
        {
        public long Id { get; set; }

        public long CostumerId { get; set; }

        public string OrderDate { get; set; }

        public long ShipperId { get; set; }

        public int TotalAmount { get; set; }

        public string OrderStatus { get; set; }


           
        }
}
