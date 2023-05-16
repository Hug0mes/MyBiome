
using MyBiome.Models;
using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class CartItem
        {
                [Key]
                public int Id { get; set; }
                public int ProductId { get; set; }
                public string ProductName { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
                public decimal Total
                {
                        get { return Quantity * Price; }
                }
                public string Image { get; set; }

                public CartItem()
                {

                }

                public CartItem(Products product)
                {
                        ProductId = product.Id;
                        ProductName = product.Name;
                        Price = product.Price;
                        Quantity = 1;
                        Image = product.Image1;
        }

        }
}
