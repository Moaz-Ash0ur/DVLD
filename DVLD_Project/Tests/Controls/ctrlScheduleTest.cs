using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlScheduleTest : UserControl
    {

        public enum enMode {AddNew=0,Update=1 };
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0 , RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        private clsAppLocalLicense _AppLocalLicense;

        private int _LocalDrivingAppID = -1;

        clsTestAppoinment _TestAppoinment;

        private int _TestAppoinmentID = -1;

        int _RetakeApplicationID = 0;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        //to handel photo and test type title when use control depand TestType ID Enum

        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

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
                            pbTestTypeImage.Image = Resources.Written_Test_32;
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

        //All test Depand app type local that releated person no need to sent it 
        public void LoadInfo(int LocalDrivingLicenseApp,int AppoinmentID =-1)
        {
            if (AppoinmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingAppID = LocalDrivingLicenseApp;
            _TestAppoinmentID = AppoinmentID;

            _AppLocalLicense = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(_LocalDrivingAppID);


            if(_AppLocalLicense == null)
            {
                MessageBox.Show("No Local Driving License Application with ID =" + _LocalDrivingAppID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //after found it need to check if the schecdule first time or not

            if (_AppLocalLicense.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            //after determine the Retake | need to get the fees
            if(_CreationMode== enCreationMode.RetakeTestSchedule)
            {
             lblRetakeAppFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
             gbRetakeTestInfo.Enabled = true;
             lblTitle.Text = "Schedule Retake Test";
             lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
            }

            //now after now the moed and cycel complet will load the data from ob to controls

            lblLocalDrivingLicenseAppID.Text = _AppLocalLicense.LocalDrivingLicenseApplicationID.ToString();
             lblDrivingClass.Text = _AppLocalLicense.LicenseClassInfo.ClassName;
             lblFullName.Text = _AppLocalLicense.PersonFullName;
            lblTrial.Text = _AppLocalLicense.TotalTrialsPerTest(_TestTypeID).ToString();

            if(_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.FindByID(_TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppoinment = new clsTestAppoinment();
            }
            else
            {
                //lLoad Info Appoinmetn if found
                if(!_LoadTestAppoinmentData())return;
            }

            lblTotalFees.Text =( (Convert.ToSingle(lblFees.Text)) + (Convert.ToSingle(lblRetakeAppFees.Text)) ).ToString();

            if (!_HandleActiveTestAppointment())
                return;

            if (!_HandleAppointmentLocked())
                return;

            if (!_HandlePrviousTest())
                return;

        }

        private bool _LoadTestAppoinmentData()
        {

            _TestAppoinment = clsTestAppoinment.FindByTestAppointmentID(_TestAppoinmentID);

            if (_TestAppoinment == null)
            {
                MessageBox.Show("No Appointment with ID ="+ _TestAppoinmentID.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppoinment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppoinment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppoinment.AppointmentDate;

            dtpTestDate.Value = _TestAppoinment.AppointmentDate;

            if (_TestAppoinment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblRetakeTestAppID.Text = "??";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppoinment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppoinment.RetakeTestApplicationID.ToString();
            }
            return true;
        }

        private bool _HandleActiveTestAppointment()
        {
            if (_Mode == enMode.AddNew && clsAppLocalLicense.IsThereAnActiveScheduledTest(_LocalDrivingAppID, _TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }

            return true;
        }

        private bool _HandleAppointmentLocked()
        {
           
            if (_TestAppoinment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTest()
        {
            
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;

                    return true;

                case clsTestType.enTestType.WrittenTest:
                    
                    if (!_AppLocalLicense.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case clsTestType.enTestType.StreetTest:
               
                    if (!_AppLocalLicense.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }

        private bool _HandleRetakeApplication()
        {
          
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
             
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _AppLocalLicense.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus1 = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppoinment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppoinment.RetakeTestApplicationID = Application.ApplicationID;

            }
            else
            {
                _TestAppoinment.RetakeTestApplicationID = 0;
            }
            return true;
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppoinment.TestTypeID = _TestTypeID;
            _TestAppoinment.LocalDrivingLicenseApplicationID = _AppLocalLicense.LocalDrivingLicenseApplicationID;
            _TestAppoinment.AppointmentDate = dtpTestDate.Value;
            _TestAppoinment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppoinment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                        
            if (_TestAppoinment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       


    }
}
