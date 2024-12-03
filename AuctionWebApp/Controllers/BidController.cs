using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly string _connectionString;

        public BidController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                  ?? throw new InvalidOperationException("Connection string not found.");
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
            Bid ? bid = null;

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
        [HttpPost("create")]
        public IActionResult CreateBid(decimal newBid, int auctionID, int memberID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("PlaceBid", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@newBid", newBid);
                    command.Parameters.AddWithValue("@auctionID", auctionID);
                    command.Parameters.AddWithValue("@memberID", memberID);

                    command.ExecuteNonQuery();
                }

                return Ok(new { Message = "Bid placed successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while placing the bid.", Error = ex.Message });
            }
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
