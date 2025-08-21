using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, UpdateMode = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };


        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo { set; get; }

        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;//compostion for ApplicationType tocan acces from this class to any info use Comp
        
        public int ApplicationStatus { get; set; }
        public enApplicationStatus ApplicationStatus1 { set; get; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus1)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;//same thing wiht User to get name,age,pass

    
        private clsApplication(int ApplicationID, int ApplicantPersonID,
          DateTime ApplicationDate, int ApplicationTypeID,
           enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
           float PaidFees, int CreatedByUserID)

        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo  = clsPerson.FindByPersonID(ApplicantPersonID); 
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.FindApplicationType(ApplicationTypeID);
            this.ApplicationStatus1 = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.UpdateMode;
        }


        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.ApplicationStatus1 = enApplicationStatus.New;

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.ApplicationID = DVLD_DataAccess.clsApplicationData.AddApplication(this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus1, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _Update()
        {
            //call DataAccess Layer 

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus1,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

          if(clsApplicationData.FindApplicationInfoByID(ApplicationID, ref ApplicantPersonID,ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate,ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID,ApplicationDate, 
                    ApplicationTypeID,(enApplicationStatus)ApplicationStatus, LastStatusDate,PaidFees, CreatedByUserID);
            }                 
            else
                return null;
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static DataTable GetAllApplications()
        {
            return DVLD_DataAccess.clsApplicationData.GetAllApplications();
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID,2);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public bool Save()
        {
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


        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.IsPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public static Dictionary<string, int> GetApplicationStatistics()
        {
            return clsApplicationData.GetApplicationStatistics();
        }

        public static Dictionary<string, int> GetApplicationStatusCounts()
        {
            return clsApplicationData.GetApplicationStatusCounts();
        }

        public static Dictionary<string, int> GetTestResultsCounts()
        {
            return clsApplicationData.GetTestResultsCounts();
        }
        





















        //Will delete after end
        public static int GetApplicationIDByLocalApplicationID(int LocaAppID)
        {
            return DVLD_DataAccess.clsApplicationData.GetApplicationIDByLocalApplicationID(LocaAppID);
        }

        public static DataTable GetLicenseDetailsByApplicationID(int AppID)
        {
            return DVLD_DataAccess.clsApplicationData.GetLicenseDetailsByApplicationID(AppID);
        }

        public static DataTable GetInternationalLicenseDetailsByApplicationID(int AppID)
        {
            return DVLD_DataAccess.clsApplicationData.GetInternationalLicenseDetailsByApplicationID(AppID);
        }
        //





    }
}
