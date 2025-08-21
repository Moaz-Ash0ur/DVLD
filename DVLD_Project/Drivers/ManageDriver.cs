using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageDriver : Form
    {
        public ManageDriver()
        {
            InitializeComponent();
        }
        DataTable _dtListDrivers;

        private void ManageDriver_Load(object sender, EventArgs e)
        {
            _dtListDrivers =clsDriver.GetAllDrivers();
            dgvListDrivers.DataSource = _dtListDrivers;
            lbRecordCount.Text = dgvListDrivers.Rows.Count.ToString();
        }

        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "National No.";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(textboxFilter.Text.Trim()==""|| FilterColumn == "")
            {
                _dtListDrivers.DefaultView.RowFilter = "";
                lbRecordCount.Text = dgvListDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                _dtListDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textboxFilter.Text.Trim());
            else
                _dtListDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textboxFilter.Text.Trim());

            lbRecordCount.Text = _dtListDrivers.Rows.Count.ToString();



        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails personDetails  = new PersonDetails((int)dgvListDrivers.CurrentRow.Cells[1].Value);
            personDetails.ShowDialog();
        }

        private void showPersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frmShowPersonLicenseHistory = new frmShowPersonLicenseHistory((int)dgvListDrivers.CurrentRow.Cells[1].Value);    
            frmShowPersonLicenseHistory.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            textboxFilter.Visible = cbFilterBy.Text != "None";
            textboxFilter.Text = "";
            textboxFilter.Focus();

        }

        private void textboxFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



    }
}
