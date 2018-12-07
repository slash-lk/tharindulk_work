using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Fun with Data Readers ***\n");

            // Create and open a connection
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=True;Pooling=False";

                connection.Open();

                // create a SQL command object
                string sql = "SELECT * FROM Inventory; SELECT * FROM Customers";
                SqlCommand myCommand = new SqlCommand(sql, connection);

                // Obtain a data reader using ExecuteReader
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    do
                    {
                        // Loop over the results.
                        while (myDataReader.Read())
                        {
                            //Console.WriteLine($"-> Make: {myDataReader["Make"]}, Pet Name: {myDataReader["PetName"]}," +
                            //    $" Color: {myDataReader["Color"]}.");

                            Console.WriteLine("*** Record ***");
                            for (int i = 0; i < myDataReader.FieldCount; i++)
                            {
                                Console.WriteLine($"{myDataReader.GetName(i)} = {myDataReader.GetValue(i)} ");
                            }
                            Console.WriteLine();
                        }
                    } while (myDataReader.NextResult());                                        
                }
                Console.ReadLine();
            }
        }
    }
}
