using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.ConnectedLayer;

namespace AdoNetTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Simple Transaction Example ****\n");

            // A simple way to allow the tx to succeed or not.
            bool throwEx = true;

            Console.Write("Do you want to throw an exception (Y or N): ");
            var userAnswer = Console.ReadLine();

            if (userAnswer?.ToLower() == "n")
            {
                throwEx = false;
            }

            var dal = new InventoryDAL();
            dal.OpenConnection(ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString);

            // Process customer 5 - enter the id of Homer Simpson in the next line
            dal.ProcessCreditRisk(throwEx, 5);
            Console.WriteLine("Check CreditRisks table for results");
            Console.ReadLine();
        }
    }
}
