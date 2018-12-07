using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.ConnectedLayer;
using AutoLotDAL.Models;
using System.Configuration;
using System.Data;

namespace AutoLotCUIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** The AutoLot Console UI ****\n");

            // Get connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            bool userDone = false;
            string userCommand = "";

            // Create our InventoryDAL object
            InventoryDAL invDAL = new InventoryDAL();
            invDAL.OpenConnection(connectionString);

            // Keep asking for input until user presses the Q key
            try
            {
                ShowInstructions();

                do
                {
                    Console.Write("\n Please enter your command: ");
                    userCommand = Console.ReadLine();
                    Console.WriteLine();

                    // if userCommand is not null, convert to uppercase. If it's null, return ""
                    switch (userCommand?.ToUpper()??"")
                    {
                        case "I":
                            InsertNewCar(invDAL);
                            break;
                        case "U":
                            UpdateCarPetName(invDAL);
                            break;
                        case "D":
                            DeleteCar(invDAL);
                            break;
                        case "L":
                            ListInventory(invDAL);
                            break;
                        case "S":
                            ShowInstructions();
                            break;
                        case "P":
                            LookUpPetName(invDAL);
                            break;
                        case "Q":
                            userDone = true;
                            break;
                        default:
                            Console.WriteLine("Bad data! Try again");
                            break;
                    }
                } while (!userDone);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                invDAL.CloseConnection();
            }
        }

        private static void LookUpPetName(InventoryDAL invDAL)
        {
            throw new NotImplementedException();
        }

        private static void ListInventory(InventoryDAL invDAL)
        {
            // Get the list of inventory
            DataTable dt = invDAL.GetAllInventoryAsDataTable();

            // Pass DataTable to helper function to display
            DisplayTable(dt);
        }


        private static void ListInventoryViaList(InventoryDAL invDAL)
        {
            // Get the list of inventory
            List<NewCar> record = invDAL.GetAllInventoryAsList();

            Console.WriteLine("CarId:\tMake:\tColor:\tPetName:");

            foreach (NewCar c in record)
            {
                Console.WriteLine($"{c.CarId}\t{c.Make}\t{c.Color}\t{c.PetName}");
            }
        }


        private static void DeleteCar(InventoryDAL invDAL)
        {
            // Get ID of the car to delete
            Console.Write("Enter ID of the Car to delete: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            // Just in case you have a referential integration violation!
            try
            {
                invDAL.DeleteCar(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static void UpdateCarPetName(InventoryDAL invDAL)
        {
            // First get the user data
            Console.Write("Enter Car ID: ");
            var carId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter New Pet Name: ");
            var newCarPetName = Console.ReadLine();

            // Now pass to data access library
            invDAL.UpdateCarPetName(carId, newCarPetName);
        }


        private static void InsertNewCar(InventoryDAL invDAL)
        {
            Console.Write("Enter Car ID: ");
            var newCarId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter Car Color: ");
            var newCarColor = Console.ReadLine();
            Console.Write("Enter Car ID: ");
            var newCarMake = Console.ReadLine();
            Console.Write("Enter Pet Name: ");
            var newCarPetName = Console.ReadLine();

            // Now pass to data access library
            var c = new NewCar
            {
                CarId = newCarId,
                Color = newCarColor,
                Make = newCarMake,
                PetName = newCarPetName
            };

            invDAL.InsertAuto(c);
        }


        private static void ShowInstructions()
        {
            Console.WriteLine("I: Inserts a new car.");
            Console.WriteLine("U: Updates an existing car.");
            Console.WriteLine("D: Deletes an existing car.");
            Console.WriteLine("L: Lists current inventory.");
            Console.WriteLine("S: Shows these instructions.");
            Console.WriteLine("P: Looks up pet name.");
            Console.WriteLine("Q: Quits program.");
        }


        private static void DisplayTable(DataTable dt)
        {
            // Print out the column names
            for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
            {
                Console.Write($"{dt.Columns[curCol].ColumnName}\t");
            }
            Console.WriteLine("\n------------------------------------");

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
