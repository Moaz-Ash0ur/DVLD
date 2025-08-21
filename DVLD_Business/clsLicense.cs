using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicense
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };


        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }

        public clsDriver DriverInfo;

        public int LicenseClass { get; set; }

        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public enIssueReason IssueReason { set; get; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public clsDetainedLicense DetainedInfo { set; get; }

       public bool IsDetained
           {
           get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
           }




        public clsLicense()

        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }


        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate,
                          string notes, float paidFees, bool isActive,enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            this.DriverInfo = clsDriver.FindByDriverID(driverID);
            this.LicenseClassInfo = clsLicenseClass.Find(licenseClass);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(licenseID);

            Mode =  enMode.UpdateMode;

        }


        public bool _AddNewLicense()
        {
            this.LicenseID = DVLD_DataAccess.clsLicenceData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                                                                       this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,
                                                                      (byte)this.IssueReason, this.CreatedByUserID);
            return this.LicenseID != -1;
        }

        public bool _UpdateLicense()
        {
            return DVLD_DataAccess.clsLicenceData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                                                                 this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                                                                 this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense FindByLicenseID(int licenseID)
        {
            int applicationID = 0, driverID = 0, createdByUserID = 0;
            int licenseClass = 0; string notes = ""; int issueReason = 0;
            DateTime issueDate = DateTime.MinValue, expirationDate = DateTime.MinValue;
            float paidFees = 0;
            bool isActive = false;

            if (DVLD_DataAccess.clsLicenceData.GetLicenseInfoByID(licenseID, ref applicationID, ref driverID, ref licenseClass,
                                                            ref issueDate, ref expirationDate, ref notes, ref paidFees,
                                                            ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID, licenseClass, issueDate, expirationDate, notes,
                                      paidFees, isActive, (enIssueReason)issueReason, createdByUserID);
            }

            return null;
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenceData.GetAllLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return _UpdateLicense();

                case enMode.AddNew:
                    if (_AddNewLicense())
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

      

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenceData.GetDriverLicenses(DriverID);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicenceData.DeactivateLicense(this.LicenseID));
        }

        public bool ActivateLicense()
        {
            return (clsLicenceData.ActivateLicense(this.LicenseID));
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenceData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus1 = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = this.CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus1 = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplicationType(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())       
                return null;
            


            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = this.CreatedByUserID;

            if (!NewLicense.Save())           
                return null;
            

            DeactivateCurrentLicense();
            return NewLicense;

        }

        public static int GetPersonIDByDriverID(int DriverID)
        {
            return DVLD_DataAccess.clsLicenceData.GetPersonIDByDriverID(DriverID);
        }

        public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();

            detainedLicense.LicenseID = this.LicenseID;//you are in license what to detain it
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {
                return -1;

            }

            return detainedLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus1 = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }

    }
}
