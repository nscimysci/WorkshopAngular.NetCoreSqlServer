using System;
using System.Collections.Generic;

namespace APPAPI.Models
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public virtual Brands Brand { get; set; }
        public virtual Categories Category { get; set; }
    }
}
