using AuctionSemesterProject.AuctionModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSemesterProject.DataAccess.Interfaces;

namespace AuctionSemesterProject.DataAccess
{
    public class MemberDAO : IMemberAccess
    {
        private readonly string _connectionString;

        public MemberDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            var members = new List<Member>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        M.MemberID, M.FirstName, M.LastName, M.Birthday, M.PhoneNo, M.Email,
                        A.AddressID, A.StreetName, A.City, A.ZipCode
                    FROM Member M
                    LEFT JOIN Address A ON M.AddressID_FK = A.AddressID;";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        members.Add(new Member
                        {
                            MemberID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Birthday = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                            PhoneNo = reader.GetString(4),
                            Email = reader.GetString(5),
                            Address = new Address
                            {
                                AddressID = reader.GetInt32(6),
                                StreetName = reader.GetString(7),
                                City = reader.GetString(8),
                                ZipCode = reader.GetString(9)
                            }
                        });
                    }
                }
            }

            return members;
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        M.MemberID, M.FirstName, M.LastName, M.Birthday, M.PhoneNo, M.Email,
                        A.AddressID, A.StreetName, A.City, A.ZipCode
                    FROM Member M
                    LEFT JOIN Address A ON M.AddressID_FK = A.AddressID
                    WHERE M.MemberID = @MemberID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Member
                            {
                                MemberID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Birthday = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                                PhoneNo = reader.GetString(4),
                                Email = reader.GetString(5),
                                Address = new Address
                                {
                                    AddressID = reader.GetInt32(6),
                                    StreetName = reader.GetString(7),
                                    City = reader.GetString(8),
                                    ZipCode = reader.GetString(9)
                                }
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task CreateMemberAsync(Member member)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    INSERT INTO Member (FirstName, LastName, Birthday, PhoneNo, Email, AddressID_FK)
                    VALUES (@FirstName, @LastName, @Birthday, @PhoneNo, @Email, @AddressID_FK);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@Birthday", member.Birthday ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNo", member.PhoneNo);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@AddressID_FK", member.Address?.AddressID ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateMemberAsync(Member member)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    UPDATE Member
                    SET FirstName = @FirstName, LastName = @LastName, Birthday = @Birthday, 
                        PhoneNo = @PhoneNo, Email = @Email, AddressID_FK = @AddressID_FK
                    WHERE MemberID = @MemberID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@Birthday", member.Birthday ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNo", member.PhoneNo);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@AddressID_FK", member.Address?.AddressID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MemberID", member.MemberID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Member WHERE MemberID = @MemberID;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
