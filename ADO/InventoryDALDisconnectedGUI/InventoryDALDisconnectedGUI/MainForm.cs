using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoLotDAL.DisconnectedLayer;

namespace InventoryDALDisconnectedGUI
{
    public partial class MainForm : Form
    {
        InventoryDALDC _dal = null;

        public MainForm()
        {
            InitializeComponent();

            string cnStr = @"Data Source = localhost\SQLEXPRESS; Initial Catalog = AutoLot; Integrated Security = SSPI";

            // Create our data access object
            _dal = new InventoryDALDC(cnStr);

            // Fill up our grid
            inventoryGrid.DataSource = _dal.GetAllInventory();
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            // Get modified data from the grid
            DataTable changedDT = (DataTable)inventoryGrid.DataSource;

            try
            {
                // Commit our changes
                _dal.UpdateInventory(changedDT);
                inventoryGrid.DataSource = _dal.GetAllInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
