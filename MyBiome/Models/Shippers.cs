using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class Shippers
        {
                [Key]
                public long Id { get; set; }

                public string CompanyName { get; set; }

                public decimal price { get; set; }
         
                public string phone { get; set;}

                public Orders Orders { get; set; }

        }
}
