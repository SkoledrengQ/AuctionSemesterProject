using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=hildur.ucn.dk;Database=DMA-CSD-V23_10461224;User Id=DMA-CSD-V23_10461224;Password=Password1!;Encrypt=True;TrustServerCertificate=True;";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection successful!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine(); // Keeps the console open until you press Enter.
        }
    }
}
