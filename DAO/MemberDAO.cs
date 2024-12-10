using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSemesterProject.DataAccess
{
    public class MemberDAO
    {
        private readonly string _connectionString;

        public MemberDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Member", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
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

            return members;
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            Member? member = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Member WHERE memberID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
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

            return member;
        }

        public async Task CreateMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Member (firstName, lastName, birthday, phoneNo, email, addressID_FK) " +
                    "VALUES (@firstName, @lastName, @birthday, @phoneNo, @email, @addressID_FK)",
                    connection
                );

                command.Parameters.AddWithValue("@firstName", member.FirstName);
                command.Parameters.AddWithValue("@lastName", member.LastName);
                command.Parameters.AddWithValue("@birthday", member.Birthday);
                command.Parameters.AddWithValue("@phoneNo", member.PhoneNo);
                command.Parameters.AddWithValue("@email", member.Email);
                command.Parameters.AddWithValue("@addressID_FK", member.AddressID_FK);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateMemberAsync(int id, Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                    "UPDATE Member SET firstName = @firstName, lastName = @lastName, birthday = @birthday, phoneNo = @phoneNo, " +
                    "email = @email, addressID_FK = @addressID_FK WHERE memberID = @id",
                    connection
                );

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", member.FirstName);
                command.Parameters.AddWithValue("@lastName", member.LastName);
                command.Parameters.AddWithValue("@birthday", member.Birthday);
                command.Parameters.AddWithValue("@phoneNo", member.PhoneNo);
                command.Parameters.AddWithValue("@email", member.Email);
                command.Parameters.AddWithValue("@addressID_FK", member.AddressID_FK);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteMemberAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("DELETE FROM Member WHERE memberID = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
