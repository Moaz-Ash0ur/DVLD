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
    public partial class ctrDrivingLicenceApplication : UserControl
    {

        clsAppLocalLicense _LocalDrivingAppLicense;

        private int _LocalDrivingLicenseAppID = -1;

        private int _LicenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseAppID; }
        }

        public ctrDrivingLicenceApplication()
        {
            InitializeComponent();
        }
    


        private void _FillLocalDrivingLicenseApp()
        {
             _LicenseID = _LocalDrivingAppLicense.GetActiveLicenseID();

            // incase there is license enable the show link.
              lnShowLicenseInfo.Enabled = (_LicenseID != -1);

            lbLocalAppID.Text = _LocalDrivingAppLicense.LocalDrivingLicenseApplicationID.ToString();
             lbPasstedTestCount.Text = _LocalDrivingAppLicense.GetPassedTestCount().ToString();
           lbClassType.Text = clsLicenseClass.Find(_LocalDrivingAppLicense.LicenseClassID).ClassName;
           ctrApplicationInfo1._ShowApplicationInfo(_LocalDrivingAppLicense.ApplicationID);
                   
        }

        public void _ShowLocalLiceAppInfo(int LocalLiecApp)
        {
            _LocalDrivingAppLicense = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(LocalLiecApp);

            if (_LocalDrivingAppLicense == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApp();

        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseAppID = -1;
            ctrApplicationInfo1.ResetApplicationInfo();
            lbLocalAppID.Text = "[????]";
            lbClassType.Text = "[????]";

        }



        private void ctrDrivingLicenceApplication_Load(object sender, EventArgs e)
        {

        }



    }
}
