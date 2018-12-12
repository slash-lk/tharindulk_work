﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with DataSets ****\n");

            // Create the DataSet object and add a few properties
            var carsInventoryDS = new DataSet("Car Inventory");

            carsInventoryDS.ExtendedProperties["TimeStamp"] = DateTime.Now;
            carsInventoryDS.ExtendedProperties["DataSetID"] = Guid.NewGuid();
            carsInventoryDS.ExtendedProperties["Company"] = "Slash & Co.";

            FillDataSet(carsInventoryDS);
            PrintDataSet(carsInventoryDS);

            SaveAndLoadAsXml(carsInventoryDS);

            SaveAndLoadAsBinary(carsInventoryDS);

            Console.WriteLine();
            Console.WriteLine("** Using Data Reader **\n");
            PrintDataSetUsingReader(carsInventoryDS);
            Console.ReadLine();            
        }


        private static void FillDataSet(DataSet ds)
        {
            // Create data columns that map to the "real" columns in the Inventory table
            // of the AutoLot database.
            var carIdColumn = new DataColumn("CarID", typeof(int))
            {
                Caption = "Car ID",
                ReadOnly = true,
                AllowDBNull = false,
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };

            var carMakeColumn = new DataColumn("Make", typeof(string));

            var carColorColumn = new DataColumn("Color", typeof(string));

            var carPetNameColumn = new DataColumn("PetName", typeof(string))
            {
                Caption = "Pet Name"
            };

            // Now add DataColumns to a DataTable.
            var inventoryTable = new DataTable("Inventory");
            inventoryTable.Columns.AddRange(new[] { carIdColumn, carMakeColumn, carColorColumn, carPetNameColumn });

            // Now add some rows to the Inventory Table.

            // Row #1
            DataRow carRow = inventoryTable.NewRow();
            carRow["Make"] = "BMW";
            carRow["Color"] = "Black";
            carRow["PetName"] = "Hamlet";
            inventoryTable.Rows.Add(carRow);

            // Row #2
            carRow = inventoryTable.NewRow();
            // Column 0 is the autoincremented ID field, so start at 1.
            carRow[1] = "Saab";
            carRow[2] = "Red";
            carRow[3] = "Sea Breeze";
            inventoryTable.Rows.Add(carRow);

            // Mark the primary key of this table.
            inventoryTable.PrimaryKey = new[] { inventoryTable.Columns[0] };

            // Finally, add our table to the DataSet.
            ds.Tables.Add(inventoryTable);
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


        private static void PrintTable(DataTable dt)
        {
            // Get the DataTableReader type.
            DataTableReader dtReader = dt.CreateDataReader();

            // The DataTableReader works just like the DataReader.
            while (dtReader.Read())
            {
                for (int i = 0; i < dtReader.FieldCount; i++)
                {
                    Console.Write($"{dtReader.GetValue(i).ToString().Trim()}\t");
                }
                Console.WriteLine();
            }
            dtReader.Close();
        }


        private static void PrintDataSetUsingReader(DataSet ds)
        {
            // Print out the DataSet name and any extended properties.
            Console.WriteLine($"DataSet is named: {ds.DataSetName}");

            foreach (DictionaryEntry de in ds.ExtendedProperties)
            {
                Console.WriteLine($"Key = {de.Key}, Value = {de.Value}");
            }

            Console.WriteLine();

            // Print out each table using data reader.
            foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine($"=> {dt.TableName} Table:");

                // Print out the column names.
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Console.Write($"{dt.Columns[curCol].ColumnName}\t");
                }
                Console.WriteLine("\n----------------------------------------");

                // Call our new helper method
                PrintTable(dt);
            }
        }


        private static void SaveAndLoadAsXml(DataSet carsInventoryDS)
        {
            // Save this DataSet as XML.
            carsInventoryDS.WriteXml("carsDataSet.xml");
            carsInventoryDS.WriteXmlSchema("carsDataSet.xsd");

            // Clear out DataSet
            carsInventoryDS.Clear();

            // Load DataSet from XML file.
            carsInventoryDS.ReadXml("carsDataSet.xml");
        }


        private static void SaveAndLoadAsBinary(DataSet carsInventoryDS)
        {
            // Set binary serialization flag.
            carsInventoryDS.RemotingFormat = SerializationFormat.Binary;

            // Save this DataSet as binary.
            var fs = new FileStream("BinaryCars.bin", FileMode.Create);

            var bFormat = new BinaryFormatter();
            bFormat.Serialize(fs, carsInventoryDS);
            fs.Close();

            // Clear out DataSet.
            carsInventoryDS.Clear();

            // Load DataSet from binary file.
            fs = new FileStream("BinaryCars.bin", FileMode.Open);
            DataSet data = (DataSet)bFormat.Deserialize(fs);
            fs.Close();
            carsInventoryDS = data;
        }

    }
}