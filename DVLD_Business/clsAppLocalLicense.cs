using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;
using static DVLD_Business.clsLicense;

namespace DVLD_Business
{
    public class clsAppLocalLicense : clsApplication
    {
        private enum enMode { AddNew  = 0, UpdateMode = 1 };

        private enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get
            {
                return clsPerson.FindByPersonID(ApplicantPersonID).FullName;
            }

        }


        private clsAppLocalLicense(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
                  DateTime ApplicationDate, int ApplicationTypeID,
                   enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
                   float PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus1 = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
             this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.UpdateMode;
        }

        public clsAppLocalLicense()
        {
            this.LicenseClassID = -1;
            this.LocalDrivingLicenseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.LocalDrivingLicenseApplicationID = DVLD_DataAccess.clsAppLocalLicenseData.AddLocalDrivingLicenseApplication(this.ApplicationID,                      
                this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _Update()
        {

            return clsAppLocalLicenseData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return DVLD_DataAccess.clsAppLocalLicenseData.GetAllLocalDrivingLicenseApplications();

        }

        public static clsAppLocalLicense FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsAppLocalLicenseData.FindLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsAppLocalLicense(LocalDrivingLicenseApplicationID, Application.ApplicationID,Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID,(enApplicationStatus)Application.ApplicationStatus1, 
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;

        }

        public static clsAppLocalLicense FindByApplicationID(int ApplicationID)
        {
            
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsAppLocalLicenseData.FindLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsAppLocalLicense(LocalDrivingLicenseApplicationID, Application.ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus1,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public bool Delete()
        {
            bool IsLocalDrivingApplDeleted = false;
            bool IsBaseApplicationDeleted = false;

            //first we delete the Local Driving LicenseApp
            IsLocalDrivingApplDeleted = clsAppLocalLicenseData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplDeleted)
                return false;
            //then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }

        public bool Save()
        {

            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateMode:

                    return _Update();
            }

            return false;
        }

        //======================================================

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApp, clsTestType.enTestType TestTypeID)

        {
            return clsAppLocalLicenseData.DoesPassTestType(LocalDrivingLicenseApp,(int)TestTypeID);
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsAppLocalLicenseData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsAppLocalLicenseData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsAppLocalLicenseData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {

            return clsAppLocalLicenseData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return clsAppLocalLicenseData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver _Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);  
             
            if(_Driver == null)
            {
                _Driver = new clsDriver();
                _Driver.PersonID = this.ApplicantPersonID;
                _Driver.CreatedByUserID = CreatedByUserID;

                if (_Driver.Save())
                {
                    DriverID = _Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = _Driver.DriverID;
            }

            clsLicense _License = new clsLicense();

            _License.ApplicationID = this.ApplicationID;
            _License.DriverID = DriverID;
            _License.LicenseClass = this.LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            _License.Notes = Notes;
            _License.PaidFees = this.LicenseClassInfo.ClassFees;
            _License.IsActive = true;
            _License.IssueReason = clsLicense.enIssueReason.FirstTime;
            _License.CreatedByUserID = this.CreatedByUserID;

            if (_License.Save())
            {
                this.SetComplete();

                return _License.LicenseID;
            }

            else
                return -1;

        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

    
        




      



    }
}
