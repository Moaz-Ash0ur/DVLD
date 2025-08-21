using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDetainedLicense
    {

        private enum enMode { AddNew = 0 , UpdateMode = 1  };
        private enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { set; get; }

        //for release license

        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { get; set; }


        private clsDetainedLicense(int detainID, int licenseID, DateTime detainDate, float fineFees, int createdByUserID, bool isReleased,
                          DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
            this.ReleasedByUserInfo = clsUser.FindByUserID(this.ReleasedByUserID);
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);


            Mode = enMode.UpdateMode;
        }

        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = 0;
            this.DetainDate = DateTime.MinValue;
            this.FineFees = 0;
            this.CreatedByUserID = 0;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        public bool AddNew()
        {
            this.DetainID = DVLD_DataAccess.clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return this.DetainID != -1;
        }

        public bool Update()
        {
            return DVLD_DataAccess.clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID,ref LicenseID, ref DetainDate,ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,ref ReleasedByUserID, ref ReleaseApplicationID))

       return new clsDetainedLicense(DetainID,LicenseID, DetainDate,FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID,ref DetainID, ref DetainDate,ref FineFees, ref CreatedByUserID,         
                ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))

              return new clsDetainedLicense(DetainID,LicenseID, DetainDate,FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static DataTable GetAllDetainedLicenses()
        {
            return DVLD_DataAccess.clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return Update();

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

                default:
                    break;
            }
            return false;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }




    }
}
