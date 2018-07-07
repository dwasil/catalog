using System.Collections.Generic;
 
namespace Catalog.Models
{
    public class IndexData
    {
        public IEnumerable<Product> Products { get; set; }
        public PageData PageData { get; set; }
    }
}