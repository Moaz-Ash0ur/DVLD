using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        private enum enMode { EmptyMode = 0, UpdateMode = 1, AddNew = 2 };

        private enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }



        //make Compestion Obj
        //can get info here and use Person.Cls.NameOfCountry power of use
        public clsCountry CountryInfo;


        private clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName,
                            string lastName, DateTime dateOfBirth, int gender, string address, string phone,
                            string email, int nationalityCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            this.CountryInfo = clsCountry.Find(nationalityCountryID);

            Mode = enMode.UpdateMode;
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.MinValue;
            this.Gender = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            this.PersonID = DVLD_DataAccess.clsPersonData.AddPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _Update()
        {
            return DVLD_DataAccess.clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);
        }

        public static clsPerson FindByPersonID(int personID)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "",  address = "", phone = "", email = "";
            DateTime dateOfBirth = DateTime.MinValue;
            int nationalityCountryID = -1 , gender = 0;
            string imagePath = "";

            if (DVLD_DataAccess.clsPersonData.FindPerson(personID, ref nationalNo, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath))
            {
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender,
                                      address, phone, email, nationalityCountryID, imagePath);
            }

            return null;
        }

        public static clsPerson FindByNationalNo(string nationalNo)
        {
            int personID = -1;
            string firstName = "", secondName = "", thirdName = "", lastName = "", address = "", phone = "", email = "";
            DateTime dateOfBirth = DateTime.MinValue;
            int nationalityCountryID = -1, gender = 0;
            string imagePath = "";

            if (DVLD_DataAccess.clsPersonData.FindPersonByNationalNo(nationalNo, ref personID, ref firstName, ref secondName,
                ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID,
                ref imagePath))
            {
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender,
                                      address, phone, email, nationalityCountryID, imagePath);
            }

            return null;
        }

        public static DataTable GetAllPeople()
        {
            return DVLD_DataAccess.clsPersonData.GetAllPeople();
        }

        public static bool Delete(int personID)
        {
            return DVLD_DataAccess.clsPersonData.DeletePerson(personID);
        }

        public static bool IsPersonIDExists(int personId)
        {
            return DVLD_DataAccess.clsPersonData.IsPersonExist(personId);
        }

        public static bool IsPersonIDRelatedToUser(int personId)
        {
            return DVLD_DataAccess.clsPersonData.IsPersonIDRelatedToUser(personId);
        }

        public static bool isPersonExist(string NationlNo)
        {
            return clsPersonData.IsPersonExist(NationlNo);
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
