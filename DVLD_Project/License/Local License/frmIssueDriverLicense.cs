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
    public partial class frmIssueDriverLicense : Form
    {

        int _LocalDrivingLicenseAppID;
        clsAppLocalLicense _LocalDrivingLicenseApp;

        public frmIssueDriverLicense(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
        }

        private void frmIssueDriverLicense_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApp = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApp == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseAppID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            //check if
            if (!_LocalDrivingLicenseApp.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApp.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrDrivingLicenceApplication1._ShowLocalLiceAppInfo(_LocalDrivingLicenseAppID);

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
           int LicenseID = _LocalDrivingLicenseApp.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID :" + LicenseID.ToString(), "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("License Was not Issued !", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }



    }
}
