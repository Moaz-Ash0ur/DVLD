using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DVLD_Business
{
    public class clsTestAppoinment
    {

        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.AddNew;

     
        public int TestAppoinmentID { get; set; }
        public clsTestType.enTestType TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }

        private clsTestAppoinment(int TestAppointmentID, clsTestType.enTestType TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  float PaidFees,
           int CreatedByUserID,  bool IsLocked,  int RetakeTestApplicationID)
        {
           
            this.TestAppoinmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.PaidFees = PaidFees;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(this.RetakeTestApplicationID);

            Mode = enMode.UpdateMode;
        }


        public clsTestAppoinment()
        {
            this.TestAppoinmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;


            Mode = enMode.AddNew;
        }

        private bool AddNew()
        {
            this.TestAppoinmentID = clsTestAppoinmentData.AddTestAppoinment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees,
          this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return this.TestAppoinmentID != -1;
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsTestAppoinmentData.UpdateTestAppointment(
                this.TestAppoinmentID,
                (int)this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                (float)this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked,
                this.RetakeTestApplicationID
            );

        }

        public static clsTestAppoinment FindByTestAppointmentID(int testAppointmentID)
        {
            int testTypeID = 0, localDrivingLicenseApplicationID = 0, createdByUserID = 0, retakeTestApplicationID = 0;
            DateTime appointmentDate = DateTime.MinValue;
            float paidFees = 0;
            bool isLocked = false;

            if (clsTestAppoinmentData.FindTestAppointment(testAppointmentID, ref testTypeID, ref localDrivingLicenseApplicationID, ref appointmentDate,
                                     ref paidFees, ref createdByUserID, ref isLocked, ref retakeTestApplicationID))
            {
                return new clsTestAppoinment(testAppointmentID, (clsTestType.enTestType)testTypeID, localDrivingLicenseApplicationID, appointmentDate,
                                            paidFees, createdByUserID, isLocked, retakeTestApplicationID);
            }

            return null;
        }

        public static clsTestAppoinment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppoinmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppoinment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppoinmentData.GetAllTestAppointments();

        }

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppoinmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppoinmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        private int _GetTestID()
        {
            return clsTestAppoinmentData.GetTestID(this.TestAppoinmentID);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNew())
                    {
                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                case enMode.UpdateMode: 
                return (_Update());

                default:
                    break;
            }
            return true;
        }




    }
}
