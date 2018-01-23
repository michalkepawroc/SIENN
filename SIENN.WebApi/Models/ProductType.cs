using System;
using System.Collections.Generic;

namespace SIENN.WebApi.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public int ProductTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
