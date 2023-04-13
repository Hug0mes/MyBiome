using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Employees
        {

		[Key]
		public int Id { get; set; }

		
		public string UserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Name { get; set; }
		 
        public string Role { get; set; }

        public string Status { get; set; }

		
		//public List<AppUser> AppUsers { get; set; }
	}
}
