using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class Category
    {        
        public long Id { get; set; }        
        
        [Required]        
        public string Name { get; set; }
        public long ParentId { get; set; }

        public List<Category> SubCategories { get; set; }        
    }

    public class Product
    {
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(1024)]
        public string Picture { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Quantity { get; set; }       

        [Required]             
        public long CategoryId { get; set; }

        public Category Category { get; set; }
    }
}