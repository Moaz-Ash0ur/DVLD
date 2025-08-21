using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationType
    {

        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        private clsApplicationType(int applicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationFees = applicationFees;

            Mode = enMode.UpdateMode;
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsApplicationTypeData.UpdateApplicationFees(
                this.ApplicationTypeID,             
                this.ApplicationFees
            );
        }
       
        public static clsApplicationType FindApplicationType(int applicationTypeID)
        {
            string applicationTypeTitle = "";
            float applicationFees = 0;

            if (DVLD_DataAccess.clsApplicationTypeData.FindApplicationType( applicationTypeID, ref applicationTypeTitle, ref applicationFees))
            {
                return new clsApplicationType(applicationTypeID, applicationTypeTitle, applicationFees);
            }

            return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return DVLD_DataAccess.clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return _Update();

                default:
                    break;
            }
            return true;
        }








    }
}
