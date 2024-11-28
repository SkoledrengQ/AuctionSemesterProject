using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionWebApp.Models;
using AuctionWebApp.Data;
using System.Linq;

namespace AuctionWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public ProductsController(AuctionDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? type = null, [FromQuery] bool onlyActiveAuctions = false)
        {
            var query = _context.Products.AsQueryable(); // Query from database

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            }

            if (onlyActiveAuctions)
            {
                query = query.Where(p =>
                    (!p.AuctionStartDate.HasValue || p.AuctionStartDate <= DateTime.UtcNow) &&
                    (!p.AuctionEndDate.HasValue || p.AuctionEndDate > DateTime.UtcNow));
            }

            var productsWithCountdown = query.Select(p => new
            {
                p.ID,
                p.Title,
                p.Author,
                p.Genre,
                p.ISBN,
                p.Description,
                p.Type,
                p.CurrentHighestBid,
                AuctionStartDate = p.AuctionStartDate,
                AuctionEndDate = p.AuctionEndDate,
                RemainingTime = p.AuctionEndDate.HasValue
                    ? (double?)(p.AuctionEndDate.Value - DateTime.UtcNow).TotalSeconds
                    : null
            }).ToList();

            if (!productsWithCountdown.Any())
            {
                return NotFound(new { Message = "No matching products found." });
            }

            return Ok(productsWithCountdown);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products
                .Include(p => p.Bids) // Include related bids
                .FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            var productWithCountdown = new
            {
                product.ID,
                product.Title,
                product.Author,
                product.Genre,
                product.ISBN,
                product.Description,
                product.Type,
                product.CurrentHighestBid,
                AuctionStartDate = product.AuctionStartDate,
                AuctionEndDate = product.AuctionEndDate,
                RemainingTime = product.AuctionEndDate.HasValue
                     ? (double?)(product.AuctionEndDate.Value - DateTime.UtcNow).TotalSeconds
                     : null
            };

            return Ok(productWithCountdown);
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult Create([FromBody] Products product)
        {
            _context.Products.Add(product); // Adds product to database
            _context.SaveChanges(); // Saves the changes
            return CreatedAtAction(nameof(Get), new { id = product.ID }, product);
        }

        // PUT: api/Products/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Products updatedProduct)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null) return NotFound();

            // Update the product's properties
            product.Title = updatedProduct.Title;
            product.Author = updatedProduct.Author;
            product.ISBN = updatedProduct.ISBN;
            product.Genre = updatedProduct.Genre;
            product.Description = updatedProduct.Description;
            product.Type = updatedProduct.Type;

            _context.SaveChanges(); // Saves the changes
            return Ok(product);
        }

        // DELETE: api/Products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null) return NotFound();

            _context.Products.Remove(product); // Remove from database
            _context.SaveChanges(); // Saces the changes
            return NoContent();
        }

        // POST: api/Products/{id}/bids
        [HttpPost("{id}/bids")]
        public IActionResult PlaceBid(int id, [FromBody] Bid bid)
        {
            if (bid == null || bid.BidAmount <= 0)
            {
                return BadRequest(new { Message = "Invalid bid. Bid amount must be greater than zero." });
            }

            var product = _context.Products.Include(p => p.Bids).FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            // Additional auction checks

            product.CurrentHighestBid = bid.BidAmount;

            bid.ProductID = id;
            product.Bids.Add(bid);

            _context.SaveChanges(); // Saves the changes
            return Ok(new { Message = "Bid placed successfully", Product = product });
        }

        [HttpGet("{id}/bids")]
        public IActionResult GetBids(int id)
        {
            // Fetch the product along with its bids
            var product = _context.Products
                .Include(p => p.Bids) // Include related bids
                .FirstOrDefault(p => p.ID == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            // Orders bids by amount in descending order
            var sortedBids = product.Bids
                .OrderByDescending(b => b.BidAmount)
                .Select(b => new
                {
                    b.BidID,
                    b.ProductID,
                    b.Bidder,
                    b.BidAmount
                })
                .ToList();

            return Ok(sortedBids); // Returns the sorted bids
        }

    }
}
