﻿using System;
using System.Collections.Generic;

namespace SIENN.WebApi.Models
{
    public partial class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }

        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
