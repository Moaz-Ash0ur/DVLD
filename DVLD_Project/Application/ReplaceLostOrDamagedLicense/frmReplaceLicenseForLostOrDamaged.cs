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
using static DVLD_Business.clsLicense;

namespace DVLD_Project
{
    public partial class frmReplaceLicenseForLostOrDamaged : Form
    {
        public frmReplaceLicenseForLostOrDamaged()
        {
            InitializeComponent();
        }

         int _NewLicenseID = -1;

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)

                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }

        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }

        private void ctrlFilterLicence1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)           
                return;
            
            if (!ctrlFilterLicence1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license","Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplaceLicense.Enabled = false;
                return;
            }

            btnReplaceLicense.Enabled = true;
        }

        private void btnReplaceLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)           
                return;
           

            clsLicense NewLicense = ctrlFilterLicence1.SelectedLicenseInfo.Replace(_GetIssueReason(),clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID: " + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplaceLicense.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlFilterLicence1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lbTitleApplication.Text = "Replacement for Damaged License";
            this.Text = lbTitleApplication.Text;
            lblApplicationFees.Text = clsApplicationType.FindApplicationType(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lbTitleApplication.Text = "Replacement for Lost License";
            this.Text = lbTitleApplication.Text;
            lblApplicationFees.Text = clsApplicationType.FindApplicationType(_GetApplicationTypeID()).ApplicationFees.ToString();
        }
         
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //frmShowLicenseInfo frm =
            //   new frmShowLicenseInfo(_NewLicenseID);
            //frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          //  frmShowPersonLicenseHistory frm =
          //new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
          //  frm.ShowDialog();
        }



    }
}
