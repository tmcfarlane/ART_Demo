using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductsApp.Resources;

namespace ProductsApp.Models
{
    public class Product : IResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Weight Weight { get; set; }
    }

    public class Weight : IResource
    {
        public double Value { get; set; }
        public string Units { get; set; }
    }
}