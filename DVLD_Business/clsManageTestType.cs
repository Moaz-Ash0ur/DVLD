using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };
        private enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }
 
        private clsTestType(clsTestType.enTestType ID, string testTypeTitle, string testTypeDescription, float testTypeFees)
        {
            this.ID = ID;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;

            Mode = enMode.UpdateMode;
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsTestTypeData.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public static clsTestType FindByID(int testTypeID)
        {
            string title = string.Empty;
            string description = string.Empty;
            float fees = 0;

            if (DVLD_DataAccess.clsTestTypeData.FindTestType(testTypeID, ref title, ref description, ref fees))
            {
                return new clsTestType((enTestType)testTypeID, title, description, fees);
            }

            return null;
        }

        public static clsTestType FindByID(clsTestType.enTestType TestTypeID)
        {
            string title = string.Empty;
            string description = string.Empty;
            float fees = 0;

            if (DVLD_DataAccess.clsTestTypeData.FindTestType((int)TestTypeID, ref title, ref description, ref fees))
            {
                return new clsTestType(TestTypeID, title, description, fees);
            }

            return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return DVLD_DataAccess.clsTestTypeData.GetAllTestTypes();
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
            return false;
        }

       



    }
}
