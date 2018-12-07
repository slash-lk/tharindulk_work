using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DataProviderFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Fun with Data Provider Factories ***\n");

            // Get Connection string/provider from *.config.
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            // Get the factory provider
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            // Now get the connection object
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    showError("Connection");
                    return;
                }

                Console.WriteLine($"Your connection object is a: {connection.GetType().Name}");

                connection.ConnectionString = connectionString;
                connection.Open();

                // Make command object
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    showError("Command");
                    return;
                }

                Console.WriteLine($"Your command object is a: {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Inventory";

                // Print out data with data reader
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    Console.WriteLine($"Your data reader object is a: {dataReader.GetType().Name}");

                    Console.WriteLine("\n*** Current Inventory ***");

                    while (dataReader.Read())
                    {
                        Console.WriteLine($"-> Car #{dataReader["CarId"]} is a {dataReader["Make"]}.");
                    }
                }
            }
            Console.ReadLine();
        }

        private static void showError(string objectName)
        {
            Console.WriteLine($"There was an issue creating the {objectName}.");
            Console.ReadLine();
        }
    }
}
