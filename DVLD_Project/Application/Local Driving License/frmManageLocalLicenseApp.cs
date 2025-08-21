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
    public partial class frmManageLocalLicenseApp : Form
    {
        public frmManageLocalLicenseApp()
        {
            InitializeComponent();
        }

        DataTable dt;

        private void _ShowAllApplications()
        {
            dt =  clsAppLocalLicense.GetAllLocalDrivingLicenseApplications();
            guna2DataGridView1.DataSource = dt;

            guna2DataGridView1.Columns[0].HeaderText = "L.D.L.AppID";
            guna2DataGridView1.Columns[0].Width = 120;
            lblRecordsCount.Text = dt.Rows.Count.ToString();    
        }

        private void frmManageLocalLicenseApp_Load(object sender, EventArgs e)
        {
            _ShowAllApplications();
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            ApplicationLocalLicense applicationLocalLicense = new ApplicationLocalLicense();
            applicationLocalLicense.ShowDialog();
        }

        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (comboBoxFilter.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (textboxFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")

                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textboxFilter.Text.Trim());
            else
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textboxFilter.Text.Trim());

            lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();



        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            textboxFilter.Visible = (comboBoxFilter.Text != "None");

            if (textboxFilter.Visible)
            {
                textboxFilter.Text = "";
                textboxFilter.Focus();
            }

            dt.DefaultView.RowFilter = "";
            lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();
        }

        //========================start test for issue leicnse========================

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
            frmListAppointments frm = new frmListAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();        
            frmManageLocalLicenseApp_Load(null, null);

        }

        private void scheduleVisionTetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;

            int LicenseID = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }     

        private void issueDrivingLeicnceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicense frm = new frmIssueDriverLicense((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _ShowAllApplications();
        }

        private void shioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlocalDrivinfLicenseApp frmlocalDrivinfLicenseApp = new frmlocalDrivinfLicenseApp((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            frmlocalDrivinfLicenseApp.ShowDialog();

            frmManageLocalLicenseApp_Load(null, null);


        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            ApplicationLocalLicense frm = new ApplicationLocalLicense((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmManageLocalLicenseApp_Load(null, null);
        }

        private void deleeApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

 
            clsAppLocalLicense LocalDrivingLicenseApplication = clsAppLocalLicense.FindByLocalDrivingAppLicenseID((int)guna2DataGridView1.CurrentRow.Cells[0].Value);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    frmManageLocalLicenseApp_Load(null, null);
                }
                else               
                    MessageBox.Show("Can not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);               
            }

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsAppLocalLicense LocalDrivingLicenseApplication = clsAppLocalLicense.FindByLocalDrivingAppLicenseID((int)guna2DataGridView1.CurrentRow.Cells[0].Value);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelld Successfully !","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _ShowAllApplications();
                }
                else
                    MessageBox.Show("Can not Cancel applicatoin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

         int LocalDrivingLicenseApplicationID = (int)guna2DataGridView1.CurrentRow.Cells[0].Value;
         clsAppLocalLicense LocalDrivingLicenseApplication = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)guna2DataGridView1.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            issueDrivingLeicnceToolStripMenuItem.Enabled = (TotalPassedTests >=3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus1 == clsApplication.enApplicationStatus.New);
            scheduleTestToolStripMenuItem.Enabled = !LicenseExists;

            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus1 == clsApplication.enApplicationStatus.New);

            deleeApplicationToolStripMenuItem.Enabled =(LocalDrivingLicenseApplication.ApplicationStatus1 == clsApplication.enApplicationStatus.New);

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            scheduleTestToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus1 == clsApplication.enApplicationStatus.New);

            if (scheduleTestToolStripMenuItem.Enabled)
            {
                scheduleVisionTetsToolStripMenuItem.Enabled = !PassedVisionTest;

                scheduleWrittenToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }

        }






    }
}
