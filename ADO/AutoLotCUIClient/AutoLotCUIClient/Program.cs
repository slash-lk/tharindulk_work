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
            throw new NotImplementedException();
        }

        private static void DeleteCar(InventoryDAL invDAL)
        {
            throw new NotImplementedException();
        }

        private static void UpdateCarPetName(InventoryDAL invDAL)
        {
            throw new NotImplementedException();
        }

        private static void InsertNewCar(InventoryDAL invDAL)
        {
            throw new NotImplementedException();
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
    }
}
