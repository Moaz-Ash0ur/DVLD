using DVLD_Business;
using DVLD_Project.Properties;
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
    public partial class ctrlScheduledTest : UserControl
    {

        //This control for all test type so will change photo and title depand it

         clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
         int _TestID = -1;

         clsAppLocalLicense _LocalDrivingLicenseApp;

        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }


         int _TestAppointmentID = -1;
         int _LocalDrivingLicenseAppID = -1;
         clsTestAppoinment _TestAppointment;

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }


        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppoinment.FindByTestAppointmentID(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error No Appointment ID Found","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseAppID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApp = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApp == null)
            {
                MessageBox.Show("Error No Local Driving License Application ","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApp.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApp.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseApp.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }











        private void ctrlScheduledTest_Load(object sender, EventArgs e)
        {

        }


    }
}
