using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Project.Properties;
using static DVLD_Project.AddEditPerson;

namespace DVLD_Project
{
    public partial class ctrPersonCard : UserControl
    {

        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrPersonCard()
        {
            InitializeComponent();
        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.FindByPersonID(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.FindByNationalNo(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                PersonImage.Image = Resources.Male_512;
            else
                PersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    PersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            lbEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lbPersonID.Text = _Person.PersonID.ToString();
            lbNationalNo.Text = _Person.NationalNo;
            lbPersonName.Text = _Person.FullName;
            lbGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lbEmail.Text = _Person.Email;
            lbPhone.Text = _Person.Phone;
            lbDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lbCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lbAddress.Text = _Person.Address;
            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lbPersonID.Text = "[????]";
            lbNationalNo.Text = "[????]";
            lbPersonName.Text = "[????]";
            lbGender.Text = "[????]";
            lbEmail.Text = "[????]";
            lbPhone.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbCountry.Text = "[????]";
            lbAddress.Text = "[????]";
            PersonImage.Image = Resources.Male_512;

        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddEditPerson frm = new AddEditPerson(_PersonID);
            frm.ShowDialog();
 
            LoadPersonInfo(_PersonID);

        }






    }
}
