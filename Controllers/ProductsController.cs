using APIlogin.Data;
using APIlogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIlogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // This locks the entire controller! Only users with a Token can use it.
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // The Builder (from Program.cs) automatically injects our Database into this controller
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. READ ALL (GET: api/Products)
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. READ ONE (GET: api/Products/5)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound("Product not found.");

            return Ok(product);
        }

        // 3. CREATE (POST: api/Products)
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            // Returns a 201 Created status and the new product data
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // 4. UPDATE (PUT: api/Products/5)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id) return BadRequest("ID mismatch.");

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound("Product not found.");

            // Update the fields
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Description = updatedProduct.Description;

            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        // 5. DELETE (DELETE: api/Products/5)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound("Product not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product deleted successfully.");
        }
    }
}