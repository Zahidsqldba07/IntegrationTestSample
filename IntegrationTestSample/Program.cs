using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestSample
{
    class Program
    {        
        static void Main(string[] args)
        {
            Colors colors = new Colors();
            Console.WriteLine("Current Colors:");
            ListColors();
            Console.WriteLine("What color would you like inserted?");
            var colorAdd = Console.ReadLine();
            colors.AddColor(colorAdd);
            Console.WriteLine("New List of Colors:");
            ListColors();
            Console.ReadLine();
        }
        static void ListColors()
        {
            Colors colors = new Colors();
            var colorList = colors.GetAll();
            foreach (var color in colorList)
            {
                Console.WriteLine(color);
            }
        }
    }

    public class Colors
    {
        private string connectionString = @"Data Source = localhost;Initial Catalog = IntegrationTestDB; Integrated Security = true; Connection Timeout = 10;";

        public IEnumerable<string> GetAll()
        {
            ICollection<string> colors = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Colors", connection))
                {

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        colors.Add(reader.GetString(1));
                    }
                }
            }
            return colors;
        }

        public void AddColor(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction;
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Colors (Name) VALUES (@Name)", connection))
                    {
                        command.Transaction = transaction;                      
                        command.Parameters.Add(new SqlParameter("Name", name));                     
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    Console.WriteLine("Color not insert.");
                }
            }
        }
    }
}
