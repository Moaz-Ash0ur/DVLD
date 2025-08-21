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

namespace DVLD_Project.InternationalLiceManage
{
    public partial class frmManageInternationalLic : Form
    {
        public frmManageInternationalLic()
        {
            InitializeComponent();
        }

        private DataTable _dtInternationalLicenseApplications;

        private void frmManageInternationalLic_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApp frm = new frmInternationalLicenseApp();
            frm.ShowDialog();
            frmManageInternationalLic_Load(null, null);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;

            PersonDetails frm = new PersonDetails(PersonID);
            frm.ShowDialog();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
           frmShowInternationalInfo frm = new frmShowInternationalInfo(InternationalLicenseID);
           frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
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
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
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
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    
                        FilterColumn = "ApplicationID";
                        break;
                  
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }
            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }


    }
}
