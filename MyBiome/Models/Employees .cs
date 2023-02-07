using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiome.Models
{
        public class Employees
        {

		[Key]
		public long Id { get; set; }

		[ForeignKey("AppUser")]
		public long UserId { get; set; }

		public string Name { get; set; }

        public string Role { get; set; }

        public string Status { get; set; }

		public AppUser AppUser { get; set; }
		//public List<AppUser> AppUsers { get; set; }
	}
}
