using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly string _connectionString;

        public BidController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/Bid
        [HttpGet]
        public IActionResult GetAllBids()
        {
            List<Bid> bids = new List<Bid>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Bid", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bids.Add(new Bid
                    {
                        Amount = reader.GetDecimal(0),
                        MemberID_FK = reader.GetInt32(1),
                        AuctionID_FK = reader.GetInt32(2)
                    });
                }
            }

            return Ok(bids);
        }

        // GET: api/Bid/{id}
        [HttpGet("{id}")]
        public IActionResult GetBidById(int id)
        {
            Bid bid = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Bid WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    bid = new Bid
                    {
                        Amount = reader.GetDecimal(0),
                        MemberID_FK = reader.GetInt32(1),
                        AuctionID_FK = reader.GetInt32(2)
                    };
                }
            }

            if (bid == null)
                return NotFound();

            return Ok(bid);
        }

        // POST: api/Bid
        [HttpPost]
        public IActionResult CreateBid([FromBody] Bid bid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Bid (amount, memberID_FK, auctionID_FK) VALUES (@amount, @memberID_FK, @auctionID_FK)", connection);
                command.Parameters.AddWithValue("@amount", bid.Amount);
                command.Parameters.AddWithValue("@memberID_FK", bid.MemberID_FK);
                command.Parameters.AddWithValue("@auctionID_FK", bid.AuctionID_FK);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetBidById), new { id = bid.AuctionID_FK }, bid);
        }

        // PUT: api/Bid/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBid(int id, [FromBody] Bid bid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Bid SET amount = @amount, memberID_FK = @memberID_FK WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@amount", bid.Amount);
                command.Parameters.AddWithValue("@memberID_FK", bid.MemberID_FK);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        // DELETE: api/Bid/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBid(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Bid WHERE auctionID_FK = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
