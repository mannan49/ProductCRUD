using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Data;
using ProductCRUD.Models;
using ProductCRUD.Models.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace ProductCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ProductController (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(dbContext.Products);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var employee = dbContext.Products.Find(id);
            if (employee == null)
            {
                return NotFound("The product does not exist!");
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("category/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            var products = dbContext.Products.Where(p => p.Category.Equals(category)).ToList();
            if (products == null || products.Count == 0)
            {
                return NotFound($"No products found in category: {category}");
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult SearchProduct([FromQuery] string query)
        {
            var products = dbContext.Products
                .Where(p => p.Name.Contains(query) || p.Category.Contains(query) || p.Description.Contains(query)).ToList();

            if (products == null || products.Count == 0)
            {
                return NotFound($"No products found matching the search query: {query}");
            }
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var productEntity = new Product()
            {
                Name = addProductDto.Name,
                Image = addProductDto.Image,
                Description = addProductDto.Description,
                Category = addProductDto.Category,
                Price = addProductDto.Price,
                Created = addProductDto.Created,
                Rating = addProductDto.Rating,
                Available = addProductDto.Available,
            };
            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();
            return Ok(productEntity);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateProduct (Guid id, UpdateProductDto updateProductDto)
        {
            var Product = dbContext.Products.Find(id);
            if (Product == null)
            {
                return NotFound("The product does not exist");
            }
            Product.Name = updateProductDto.Name;
            Product.Image = updateProductDto.Image;
            Product.Description = updateProductDto.Description;
            Product.Category = updateProductDto.Category;
            Product.Price = updateProductDto.Price;
            Product.Created = updateProductDto.Created;
            Product.Rating = updateProductDto.Rating;
            Product.Available = updateProductDto.Available;
            dbContext.SaveChanges();
            return Ok(Product);
        }
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {

            var Product = dbContext.Products.Find(id);
            if (Product == null)
            {
                return NotFound("The product does not exist");
            }
            dbContext.Products.Remove(Product);
            dbContext.SaveChanges();
            return Ok("Product has been deleted");
        }
    }
}
