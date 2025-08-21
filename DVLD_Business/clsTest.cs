using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTest
    {

        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppoinment TestAppointmentInfo { set; get; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
            this.TestAppointmentInfo = clsTestAppoinment.FindByTestAppointmentID(testAppointmentID);
            Mode = enMode.UpdateMode;
        }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.TestID = DVLD_DataAccess.clsTestData.AddTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static clsTest FindByTestID(int testID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            if (DVLD_DataAccess.clsTestData.FindTest(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);
            }

            return null;
        }

        public static DataTable GetAllTests()
        {
            return DVLD_DataAccess.clsTestData.GetAllTests();
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.FindTest(TestID,ref TestAppointmentID, ref TestResult,ref Notes, ref CreatedByUserID))
                return new clsTest(TestID,TestAppointmentID, TestResult,Notes, CreatedByUserID);
            else
                return null;

        }

        public static clsTest FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;
           
            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, (int)TestTypeID, ref TestID,ref TestAppointmentID, ref TestResult,ref Notes, ref CreatedByUserID))             
                
                return new clsTest(TestID,TestAppointmentID, TestResult,Notes, CreatedByUserID);
            
            else
                return null;

        }


        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return _Update();

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

                default:
                    break;
            }
            return true;
        }



    }
}
