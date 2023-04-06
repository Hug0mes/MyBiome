using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MyBiome.Models
{
    public partial class CategoryViewModel
    {
        public Category Category { get; set; }

        public IFormFile ImageFile { get; set; }
    
        public bool RemoverImagem { get; set; }

    }
}
