using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Customers
        {
                [Key] 
                public long Id { get; set; }

                public string UserId { get; set; }
                public AppUser AppUser { get; set; }

		        public string FirstName { get; set; }

                public string LastName { get; set; }

                public string StreetAddress { get; set; }

                public string District { get; set; }

                public string City { get; set; }

                public string PostalCode { get; set; }

                public int phone { get; set; }


                public Orders Orders { get; set; }
	}
}
