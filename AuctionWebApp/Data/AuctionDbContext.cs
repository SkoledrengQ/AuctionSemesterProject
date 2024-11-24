using Microsoft.EntityFrameworkCore;
using AuctionWebApp.Models;

namespace AuctionWebApp.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
