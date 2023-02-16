using System.ComponentModel.DataAnnotations;

namespace MyBiome.Models
{
        public class Category
        {
                [Key]
                public long Id { get; set; }
                public string Name { get; set; }
                
                public CategoriesProduct CategoriesProduct { get; set; }

        internal static bool CategoryExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
