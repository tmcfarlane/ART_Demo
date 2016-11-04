using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class ProductsController : ApiController
    {
        //todo need to move to persistent data to work on chaining test data generation
        List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IHttpActionResult AddProduct(Product product)
        {
            try
            {
                _products.Add(product);
                return Ok(_products);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
