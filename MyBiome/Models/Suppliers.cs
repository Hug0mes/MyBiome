using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class Suppliers
        {
            [Key] 
            public long Id { get; set; }

            public string CompanyName { get; set; }

            public string ContactName { get; set; }

            public string ContactEmail { get; set; }

            public string phone { get; set; }

            public string StreetAddress { get; set; } 

            public string City { get; set; }

            public string State { get; set; }

            public string PostalCode { get; set; }

            
        }
}
