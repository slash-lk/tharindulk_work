using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AutoLotDAL.Models;

namespace AutoLotDAL.ConnectedLayer
{
    public class InventoryDAL
    {
        private SqlConnection _sqlConnection = null;

        public void OpenConnection(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }


        public void CloseConnection()
        {
            _sqlConnection.Close();
        }


        public void InsertAuto(int id, string color, string make, string petName)
        {
            // Format and execute SQL statement
            string sql = $"INSERT INTO Inventory(Make, Color, PetName) VALUES('{make}', '{color}', '{petName}')";

            // Execute using our connection
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }


        public void InsertAuto(NewCar car)
        {
            // Format and execute SQL statement
            string sql = $"INSERT INTO Inventory(Make, Color, PetName) VALUES('{car.Make}', '{car.Color}', '{car.PetName}')";

            // Execute using our connection
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }


        public void DeleteCar(int id)
        {
            // Delete the car with the specified CarId
            string sql = $"DELETE FROM Inventory WHERE CarId = '{id}'";

            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is on order!", ex);
                    throw error;
                }
            }
        }


        public void UpdateCarPetName(int id, string newPetName)
        {
            // Update the PetName of the car with the specified CarId
            string sql = $"UPDATE Inventory SET PetName = '{newPetName}' WHERE CarId = '{id}'";

            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }


        public List<NewCar> GetAllInventoryAsList()
        {
            // This will hold the records
            List<NewCar> inv = new List<NewCar>();

            // Prep command object
            string sql = "SELECT * FROM Inventory";

            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    inv.Add(new NewCar
                    {
                        CarId = (int)dataReader["CarId"],
                        Color = (string)dataReader["Color"],
                        Make = (string)dataReader["Make"],
                        PetName = (string)dataReader["PetName"]
                    });
                }
                dataReader.Close();
            }
            return inv;
        }


        public DataTable GetAllInventoryAsDataTable()
        {
            // This will hold the records.
            DataTable dataTable = new DataTable();

            // Prep command object
            string sql = "SELECT * FROM Inventory";

            using (SqlCommand cmd = new SqlCommand(sql, _sqlConnection))
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                // Fill the DataTable with data from the reader and clean up
                dataTable.Load(dataReader);
                dataReader.Close();
            }

            return dataTable;
        }


        public string LookUpPetName(int carId)
        {
            string carPetName;

            // Establish name of stored proc.
            using (SqlCommand command = new SqlCommand("GetPetName", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Input param
                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@carId",
                    SqlDbType = SqlDbType.Int,
                    Value = carId,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(param);

                // Output param
                param = new SqlParameter
                {
                    ParameterName = "@petName",
                    SqlDbType = SqlDbType.Char,
                    Size = 10,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(param);

                // Execute the stored proc.
                command.ExecuteNonQuery();

                // Return the output param
                carPetName = (string)command.Parameters["@petName"].Value;
            }
            return carPetName;
        }


        public void ProcessCreditRisk(bool throwEx, int custId)
        {
            // First, look up current name based on customer ID
            string fName;
            string lName;

            var cmdSelect = new SqlCommand($"SELECT * FROM Customers WHERE CustId = {custId}", _sqlConnection);

            using (var dataReader = cmdSelect.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    fName = (string)dataReader["FirstName"];
                    lName = (string)dataReader["LastName"];
                }
                else
                {
                    return;
                }
            }

            // Create command objects that represent each step of the operation.
            var cmdRemove = new SqlCommand($"DELETE FROM Customers WHERE CustId = {custId}", _sqlConnection);

            var cmdInsert = new SqlCommand($"INSERT INTO CreditRisks(FirstName, LastName) VALUES ('{fName}', '{lName}')", _sqlConnection);

            // We will get this from the connection object
            SqlTransaction tx = null;

            try
            {
                tx = _sqlConnection.BeginTransaction();

                // Enlist the commands in to this transaction
                cmdInsert.Transaction = tx;
                cmdRemove.Transaction = tx;

                // Execute the commands
                cmdInsert.ExecuteNonQuery();
                cmdRemove.ExecuteNonQuery();

                // Simulate error
                if (throwEx)
                {
                    throw new Exception("Sorry! Database error! Tx failed...");
                }

                // Commit it!
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Any error will roll back transaction.
                // Using the new conditional access operator to check for null.
                tx?.Rollback();
            }
        }
    }
}
