using MyBiome.Models;

namespace MyBiome.Models.ViewModels
{
    public class MyOrdersViewModel
    {
        public List<Products> Products { get; set; }
        public List<int> Quantities { get; set; }
        public Orders Orders { get; set; }

    }
}
