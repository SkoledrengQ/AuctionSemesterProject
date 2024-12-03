using AuctionSemesterProject.AuctionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuctionSemesterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly string _connectionString;

        public MemberController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                  ?? throw new InvalidOperationException("Connection string not found.");
        }

        // GET: api/Member
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Member", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    members.Add(new Member
                    {
                        MemberID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Birthday = reader.GetDateTime(3),
                        PhoneNo = reader.GetString(4),
                        Email = reader.GetString(5),
                        AddressID_FK = reader.GetInt32(6)
                    });
                }
            }

            return Ok(members);
        }

        // GET: api/Member/{id}
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            Member ? member = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Member WHERE memberID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    member = new Member
                    {
                        MemberID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Birthday = reader.GetDateTime(3),
                        PhoneNo = reader.GetString(4),
                        Email = reader.GetString(5),
                        AddressID_FK = reader.GetInt32(6)
                    };
                }
            }

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/Member
        [HttpPost]
        public IActionResult CreateMember([FromBody] Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Member (firstName, lastName, birthday, phoneNo, email, addressID_FK) VALUES (@firstName, @lastName, @birthday, @phoneNo, @email, @addressID_FK)", connection);
                command.Parameters.AddWithValue("@firstName", member.FirstName);
                command.Parameters.AddWithValue("@lastName", member.LastName);
                command.Parameters.AddWithValue("@birthday", member.Birthday);
                command.Parameters.AddWithValue("@phoneNo", member.PhoneNo);
                command.Parameters.AddWithValue("@email", member.Email);
                command.Parameters.AddWithValue("@addressID_FK", member.AddressID_FK);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberID }, member);
        }

        // PUT: api/Member/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, [FromBody] Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Member SET firstName = @firstName, lastName = @lastName, birthday = @birthday, phoneNo = @phoneNo, email = @email, addressID_FK = @addressID_FK WHERE memberID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", member.FirstName);
                command.Parameters.AddWithValue("@lastName", member.LastName);
                command.Parameters.AddWithValue("@birthday", member.Birthday);
                command.Parameters.AddWithValue("@phoneNo", member.PhoneNo);
                command.Parameters.AddWithValue("@email", member.Email);
                command.Parameters.AddWithValue("@addressID_FK", member.AddressID_FK);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        // DELETE: api/Member/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Member WHERE memberID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
