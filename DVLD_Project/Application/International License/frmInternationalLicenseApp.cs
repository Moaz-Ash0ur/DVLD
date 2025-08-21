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
    public partial class frmInternationalLicenseApp : Form
    {

        public frmInternationalLicenseApp()
        {
            InitializeComponent();
        }

        int _InternationalLicenseID = -1;
        clsInternationalLicense _InternationalLicense;

        private void _CreateApplication()
        {
            _InternationalLicense.ApplicantPersonID = ctrlFilterLicence1.SelectedLicenseInfo.DriverInfo.PersonID;
            _InternationalLicense.ApplicationDate = DateTime.Now;
            _InternationalLicense.ApplicationStatus1 = clsApplication.enApplicationStatus.Completed;
            _InternationalLicense.LastStatusDate = DateTime.Now;
            _InternationalLicense.PaidFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

        }

        private void frmInternationalLicenseApp_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlFilterLicence1_OnLicenseSelected(int obj)
        {
           if(obj == -1) { return; }

            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = true;

            if (ctrlFilterLicence1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlFilterLicence1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID :" + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;
        }
      
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
              new frmShowPersonLicenseHistory(ctrlFilterLicence1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalInfo frm = new frmShowInternationalInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            _InternationalLicense = new clsInternationalLicense();

            //first create appliaction will allow us issue InternationalLicense
            _CreateApplication();

            _InternationalLicense.DriverID = ctrlFilterLicence1.SelectedLicenseInfo.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = ctrlFilterLicence1.SelectedLicenseInfo.LicenseID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!_InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = _InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID:" + _InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlFilterLicence1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }






    }
}
