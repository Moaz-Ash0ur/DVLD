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
    public partial class frmListAppointments : Form
    {

         DataTable _dtLicenseTestAppointments;
         int _LocalDrivingAppID;
         clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;


        public frmListAppointments(int LocalDrivingAppID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingAppID = LocalDrivingAppID;
            _TestType = TestType;

        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmListAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrDrivingLicenceApplication1._ShowLocalLiceAppInfo(_LocalDrivingAppID);
            _dtLicenseTestAppointments = clsTestAppoinment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingAppID, _TestType);

            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsAppLocalLicense localDrivingLicenseApp = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(_LocalDrivingAppID);

            if (localDrivingLicenseApp.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApp.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmScheduleTes frm1 = new frmScheduleTes(_LocalDrivingAppID, _TestType);
                frm1.ShowDialog();
                frmListAppointments_Load(null, null) ;
                return;
            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTes frm2 = new frmScheduleTes(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID,_TestType);
            frm2.ShowDialog();
            frmListAppointments_Load(null, null);

           
        }
                

        //Take Test and Edit Schedule
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
    
            frmScheduleTes frm = new frmScheduleTes(_LocalDrivingAppID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmListAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmListAppointments_Load(null, null);
        }



    }
}
