using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data.Common;

namespace FillDataSetUsingSqlDataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with Data Adaptors ****\n");

            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            // Caller creates the DataSet object
            DataSet ds = new DataSet("AutoLot");

            // Inform adaptor of the Select command text and the connection
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Inventory", connectionString);

            // Now map DB column names to user-friendly names
            DataTableMapping tableMapping = adapter.TableMappings.Add("Inventory", "Current Inventory");
            tableMapping.ColumnMappings.Add("CarId", "Car ID");
            tableMapping.ColumnMappings.Add("PetName", "Name of Car");

            // Fill our DataSet with a new table, named Inventory
            adapter.Fill(ds, "Inventory");

            // Display contents of DataSet
            PrintDataSet(ds);
            Console.ReadLine();
        }


        private static void PrintDataSet(DataSet ds)
        {
            // Print out the DataSet name and any extended properties.
            Console.WriteLine($"DataSet is named: {ds.DataSetName}");

            foreach (DictionaryEntry de in ds.ExtendedProperties)
            {
                Console.WriteLine($"Key = {de.Key}, Value = {de.Value}");
            }

            Console.WriteLine();

            // Print out each table using rows and columns.
            foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine($"=> {dt.TableName} Table:");

                // Print out the column names.
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Console.Write($"{dt.Columns[curCol].ColumnName}\t");
                }
                Console.WriteLine("\n----------------------------------------");

                // Print the DataTable
                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                {
                    for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                    {
                        Console.Write($"{dt.Rows[curRow][curCol]}\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
