using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuctionWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly string _connectionString;

        public AuctionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/Auction
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            List<Auction> auctions = new List<Auction>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Auction", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    auctions.Add(new Auction
                    {
                        AuctionID = reader.GetInt32(0),
                        StartPrice = reader.GetDecimal(1),
                        MinBid = reader.GetDecimal(2),
                        EndingBid = reader.GetDecimal(3),
                        CurrentHighestBid = reader.GetDecimal(4),
                        BuyNowPrice = reader.GetDecimal(5),
                        NoOfBids = reader.GetInt32(6),
                        TimeExtension = reader.GetString(7),
                        EmployeeID_FK = reader.GetInt32(8),
                        ItemID_FK = reader.GetInt32(9)
                    });
                }
            }

            return Ok(auctions);
        }

        // GET: api/Auction/{id}
        [HttpGet("{id}")]
        public IActionResult GetAuctionById(int id)
        {
            Auction auction = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Auction WHERE auctionID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    auction = new Auction
                    {
                        AuctionID = reader.GetInt32(0),
                        StartPrice = reader.GetDecimal(1),
                        MinBid = reader.GetDecimal(2),
                        EndingBid = reader.GetDecimal(3),
                        CurrentHighestBid = reader.GetDecimal(4),
                        BuyNowPrice = reader.GetDecimal(5),
                        NoOfBids = reader.GetInt32(6),
                        TimeExtension = reader.GetString(7),
                        EmployeeID_FK = reader.GetInt32(8),
                        ItemID_FK = reader.GetInt32(9)
                    };
                }
            }

            if (auction == null)
                return NotFound();

            return Ok(auction);
        }

        // POST: api/Auction
        [HttpPost]
        public IActionResult CreateAuction([FromBody] Auction auction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Auction (startPrice, minBid, endingBid, currentHighestBid, buyNowPrice, noOfBids, timeExtension, employeeID_FK, itemID_FK) VALUES (@startPrice, @minBid, @endingBid, @currentHighestBid, @buyNowPrice, @noOfBids, @timeExtension, @employeeID_FK, @itemID_FK)", connection);
                command.Parameters.AddWithValue("@startPrice", auction.StartPrice);
                command.Parameters.AddWithValue("@minBid", auction.MinBid);
                command.Parameters.AddWithValue("@endingBid", auction.EndingBid);
                command.Parameters.AddWithValue("@currentHighestBid", auction.CurrentHighestBid);
                command.Parameters.AddWithValue("@buyNowPrice", auction.BuyNowPrice);
                command.Parameters.AddWithValue("@noOfBids", auction.NoOfBids);
                command.Parameters.AddWithValue("@timeExtension", auction.TimeExtension);
                command.Parameters.AddWithValue("@employeeID_FK", auction.EmployeeID_FK);
                command.Parameters.AddWithValue("@itemID_FK", auction.ItemID_FK);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionID }, auction);
        }

        // PUT: api/Auction/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAuction(int id, [FromBody] Auction auction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Auction SET startPrice = @startPrice, minBid = @minBid, endingBid = @endingBid, currentHighestBid = @currentHighestBid, buyNowPrice = @buyNowPrice, noOfBids = @noOfBids, timeExtension = @timeExtension, employeeID_FK = @employeeID_FK, itemID_FK = @itemID_FK WHERE auctionID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@startPrice", auction.StartPrice);
                command.Parameters.AddWithValue("@minBid", auction.MinBid);
                command.Parameters.AddWithValue("@endingBid", auction.EndingBid);
                command.Parameters.AddWithValue("@currentHighestBid", auction.CurrentHighestBid);
                command.Parameters.AddWithValue("@buyNowPrice", auction.BuyNowPrice);
                command.Parameters.AddWithValue("@noOfBids", auction.NoOfBids);
                command.Parameters.AddWithValue("@timeExtension", auction.TimeExtension);
                command.Parameters.AddWithValue("@employeeID_FK", auction.EmployeeID_FK);
                command.Parameters.AddWithValue("@itemID_FK", auction.ItemID_FK);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        // DELETE: api/Auction/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAuction(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Auction WHERE auctionID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
