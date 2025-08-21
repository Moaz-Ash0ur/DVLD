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
    public partial class frmReleaseDetainLicense : Form
    {
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctrlFilterLicence1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlFilterLicence1.FilterEnabled = false;
        }

        int _SelectedLicenseID = -1;


        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)          
                return;
           
            int ApplicationID = -1;

            bool IsReleased = ctrlFilterLicence1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);
            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlFilterLicence1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void ctrlFilterLicence1_OnLicenseSelected(int obj)
        {
            if (obj == -1)            
                return;
            
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (!ctrlFilterLicence1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            lblDetainID.Text = ctrlFilterLicence1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlFilterLicence1.SelectedLicenseInfo.LicenseID.ToString();
            lblCreatedByUser.Text = ctrlFilterLicence1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;

            lblDetainDate.Text = clsFormat.DateToShort(ctrlFilterLicence1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlFilterLicence1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();           
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            btnRelease.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
            new frmShowPersonLicenseHistory(ctrlFilterLicence1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }
    
    
    
    
    }
}
