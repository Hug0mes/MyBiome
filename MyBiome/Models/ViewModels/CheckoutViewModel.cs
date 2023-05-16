using MyBiome.Models;

namespace MyBiome.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public Orders Orders { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
