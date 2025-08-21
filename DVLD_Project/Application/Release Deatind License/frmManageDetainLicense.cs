using DVLD_Business;
using Guna.UI2.WinForms;
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
    public partial class frmManageDetainLicense : Form
    {
        public frmManageDetainLicense()
        {
            InitializeComponent();
        }

        DataTable _dtDetainedLicenses;
        private void frmManageDetainLicense_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            guna2DataGridView1.DataSource = _dtDetainedLicenses;
        }

        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "Is Released":              
                        FilterColumn = "IsReleased";
                        break;   
                    
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtDetainedLicenses.Rows.Count.ToString();

        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        //Context Menu
        private void shioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.FindByLicenseID((int)guna2DataGridView1.CurrentRow.Cells[1].Value).DriverInfo.PersonID;

            PersonDetails frm = new PersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            frmLicenseInfo frm = new frmLicenseInfo((int)guna2DataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLiecnseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.FindByLicenseID((int)guna2DataGridView1.CurrentRow.Cells[1].Value).DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetaindLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense((int)guna2DataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmManageDetainLicense_Load(null, null);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetaindLicenseHistoryToolStripMenuItem.Enabled = !(bool)guna2DataGridView1.CurrentRow.Cells[3].Value;
        }

        private void btnDetainedLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
        }

        private void btnReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frmReleaseDetainLicense = new frmReleaseDetainLicense((int)guna2DataGridView1.CurrentRow.Cells[1].Value);
            frmReleaseDetainLicense.ShowDialog();
        }




    }
}
