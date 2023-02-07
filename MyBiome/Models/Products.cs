﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class Products
        {
                public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 2")]
        public string Description { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public long CategoryProductID { get; set; }


                [Required]
                [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
                [Column(TypeName = "decimal(8, 2)")]
                public decimal Price { get; set; }

                public string Status { get; set; }
    }
}