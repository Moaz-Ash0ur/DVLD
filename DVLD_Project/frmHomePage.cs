using DVLD_Business;
using DVLD_Project.InternationalLiceManage;
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
    public partial class frmHomePage : Form
    {
        frmLogin _frmLogin;
        public frmHomePage(frmLogin login)
        {
            InitializeComponent();
            _frmLogin = login;
           
        }

        private void LoadData()
        {
            var AppStatistics = clsApplication.GetApplicationStatistics();

            txtTotalDrivers.Text = AppStatistics["@TotalDrivers"].ToString();
            txtTotalUsers.Text = AppStatistics["@TotalUsers"].ToString();
            txtInternationalLicenses.Text = AppStatistics["@TotalInternationalLicenses"].ToString();
            txtLocalLicenses.Text = AppStatistics["@TotalLocalLicenses"].ToString();
            txtDetainedLicenses.Text = AppStatistics["@TotalDetainedAndReleaseLicenses"].ToString();


            var Appstatus = clsApplication.GetApplicationStatusCounts();

            txtNewRequests.Text =  Appstatus["New"].ToString();
            txtCancelledRequests.Text = Appstatus["Cancelled"].ToString();
            txtCompletedRequests.Text =  Appstatus["Completed"].ToString();


            var testStats = clsApplication.GetTestResultsCounts();

            txtPassCount.Text = testStats["Pass"].ToString();
            txtFailCount.Text =  testStats["Fail"].ToString();



        }

        private void frmHomePage_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople managePeople = new ManagePeople();
            managePeople.ShowDialog ();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();    
            manageUsers.ShowDialog ();
            LoadData();
        }

        //get data when log in for this transaction
        private void currntUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
                UserDetails userDetails  = new UserDetails(clsGlobal.CurrentUser.UserID);
                 userDetails.ShowDialog ();
            LoadData();
        }

        private void chnagePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frmChangePassword.ShowDialog ();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
             this.Close();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmApplicationType frmApplicationType = new frmApplicationType();
            frmApplicationType.ShowDialog ();
            LoadData();
        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestType frmListTestType = new frmListTestType();
            frmListTestType.ShowDialog();
        }

        //start first application
        private void localLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationLocalLicense applicationLocalLicense = new ApplicationLocalLicense();
            applicationLocalLicense.ShowDialog();

            //here for update status
            LoadData();
        }

        private void localDrivingLicensesApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalLicenseApp applicationLocalLicenseApp = new frmManageLocalLicenseApp();
            applicationLocalLicenseApp.ShowDialog ();
            LoadData();
        }

       

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDriver manageDriver = new ManageDriver();
            manageDriver.ShowDialog();
            LoadData();
        }

        private void internationalLiceneseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalLic frmManageInternationalLic = new frmManageInternationalLic();
            frmManageInternationalLic.ShowDialog ();
            LoadData();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicense frmRenewLocalLicense = new frmRenewLocalLicense();
            frmRenewLocalLicense.ShowDialog ();
            LoadData();
        }

        private void replacmentKicenseForDamagedOrLostToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmReplaceLicenseForLostOrDamaged frmReplaceLicense = new frmReplaceLicenseForLostOrDamaged();
            frmReplaceLicense.ShowDialog();
            LoadData();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmDetainLicense frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
            LoadData();
        }
   
        private void relaseDerainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frmReleaseLicense = new frmReleaseDetainLicense();
           frmReleaseLicense.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmManageDetainLicense frmManageDetainLicense = new frmManageDetainLicense();
            frmManageDetainLicense.ShowDialog();
        }

        private void internationalLiceneseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApp frmInternationalLicenseApp = new frmInternationalLicenseApp();   
            frmInternationalLicenseApp.ShowDialog();
        }

        private void frmHomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frmLogin.Show();
        }
    }
}
