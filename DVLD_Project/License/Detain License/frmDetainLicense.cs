using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        int _SelectedLicenseID;
        int _DetainLicenseID;
        clsDetainedLicense _DetainedLicense;

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now).ToString();  
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlFilterLicence1_OnLicenseSelected(int obj)
        {
            //all things you get should make check 
            if (obj == -1)
                return;       

            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;


            if (ctrlFilterLicence1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is already detained, choose another one", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            txtFineFees.Focus();
            btnDetain.Enabled = true;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)         
                return;
           
            _DetainLicenseID  = ctrlFilterLicence1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text),clsGlobal.CurrentUser.UserID);
          
            if (_DetainLicenseID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblDetainID.Text = _DetainLicenseID.ToString();
            MessageBox.Show("License Detained Successfully with ID :" + _DetainLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
            ctrlFilterLicence1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlFilterLicence1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm =new frmLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
        }
    }
}
