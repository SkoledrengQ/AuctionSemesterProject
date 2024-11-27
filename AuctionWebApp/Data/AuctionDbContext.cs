using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuctionWebApp.Models;

public class AuctionDbContext : IdentityDbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
        : base(options) { }

    public DbSet<Products> Products { get; set; }
    public DbSet<Bid> Bids { get; set; }
}
