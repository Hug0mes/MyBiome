using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Orders
        {

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        
        public string? UserId { get; set; }

        public List<CartItem>? CartItems { get; set; }

        public decimal GrandTotal { get; set; }

        public string Country { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string State { get; set; }

        public int PostalCode { get; set; }

        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        public string OrderNotes { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? OrderStatus { get; set; }   

     


	}
}
