using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MyBiome.Models
{
    public partial class ProductsViewModel
    {
        public Products Products { get; set; }
        public IFormFile ImageFile1 { get; set; }
        public IFormFile ImageFile2 { get; set; }
        public IFormFile ImageFile3 { get; set; }

        public bool RemoverImagem { get; set; }

        public List<Products>? ProductsList { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SubCategory>? subCategories { get; set; }


    }
}
