using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.UpdateMode;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo;

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonInfo = clsPerson.FindByPersonID(personID);

            Mode = enMode.UpdateMode;

        }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.UserID = DVLD_DataAccess.clsUserData.AddUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            if( clsUserData.FindUserByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }

            return null;
        }

        public static clsUser FindByUserID(int userID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = true;

            if (DVLD_DataAccess.clsUserData.FindUserById(userID, ref personID, ref userName, ref password, ref isActive))
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }

            return null;
        }

        public static DataTable GetAllUsers()
        {
            return DVLD_DataAccess.clsUserData.GetAllUsers();
        }

        public static bool Delete(int userID)
        {
            return DVLD_DataAccess.clsUserData.DeleteUser(userID);
        }

        public static bool IsUserExist(string username)
        {
            return DVLD_DataAccess.clsUserData.IsUserExists(username);
        }

        public static bool IsUserExist(int ID)
        {
            return DVLD_DataAccess.clsUserData.IsUserExist(ID);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return DVLD_DataAccess.clsUserData.IsUserExistForPersonID(PersonID);
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
