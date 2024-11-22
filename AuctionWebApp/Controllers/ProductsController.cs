using Microsoft.AspNetCore.Mvc;
using AuctionWebApp.Models;
using System.Collections.Generic;
using System.Linq;


namespace AuctionWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Temp list to store books, comics & manga
        private static List<Products> Products = new List<Products>
        {
            new Products { ID = 1, Title = "The Bible", Author = "God", Genre = "Religion", ISBN = "1234567890", CurrentHighestBid = 0.00M, Description = "The Bible is a collection of religious and sacred texts or scriptures", Type = "Book" },
            new Products { ID = 2, Title = "The Art of War", Author = "Sun Tzu", Genre = "Military", ISBN = "0987654321", CurrentHighestBid = 0.00M, Description = "The Art of War is an ancient Chinese military treatise dating from the Late Spring and Autumn Period", Type = "Book"}
       };


        // GET: api/Products
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? type = null)
        {
            if (string.IsNullOrEmpty(type))
            {
                return Ok(Products); // Return all products if no type is specified
            }

            var filteredProducts = Products
                .Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)) //Makes the search non-sensitive to capital letters
                .ToList();

            if (!filteredProducts.Any())
            {
                return NotFound(new { Message = $"No products found of type '{type}'." });
            }

            return Ok(filteredProducts);
        }



        // GET: api/Products/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = Products.FirstOrDefault(p => p.ID == id);
            if (product == null) return NotFound();
            return Ok(product);
        }


        // POST: api/Products
        [HttpPost]
        public IActionResult Create([FromBody] Products product)
        {
            product.ID = Products.Count + 1; // Automatically assign a new ID
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.ID }, product);
        }


        // PUT: api/Products/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Products updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.ID == id);
            if (product == null) return NotFound();

            // Updates the product's properties
            product.Title = updatedProduct.Title;
            product.Author = updatedProduct.Author;
            product.ISBN = updatedProduct.ISBN;
            product.Genre = updatedProduct.Genre;
            product.Description = updatedProduct.Description;
            product.Type = updatedProduct.Type;

            return Ok(product);
        }


        // DELETE: api/Products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.ID == id);
            if (product == null) return NotFound();

            Products.Remove(product);
            return NoContent();
        }


        [HttpPost("{id}/bids")]
        public IActionResult PlaceBid(int id, [FromBody] Bid bid)
        {
            var product = Products.FirstOrDefault(p => p.ID == id); // Find product by ID
            if (product == null) return NotFound();

            // Check if the new bid is higher than the current highest bid
            if (bid.BidAmount <= product.CurrentHighestBid)
            {
                return BadRequest(new { Message = "Bid must be higher than the current highest bid." });
            }

            // Update the product's current highest bid
            product.CurrentHighestBid = bid.BidAmount;

            // Add the bid to the product's bid history
            bid.ProductID = id;
            bid.ID = product.Bids.Count + 1; // Assigns an ID to the bid
            product.Bids.Add(bid);

            return Ok(new { Message = "Bid placed successfully", Product = product });
        }



    }
}
