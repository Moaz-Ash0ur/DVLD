using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Project.Properties;
using TheArtOfDevHtmlRenderer.Adapters;
using System.Xml;

namespace DVLD_Project
{
    public partial class AddEditPerson : Form
    {
        //Declare delegate
        public delegate void DataBackEvent(int PersonID);

        public event DataBackEvent dataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;

        //if person not sent is add mode
        public AddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public AddEditPerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _ResetDefualtValues()//in load
        {
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rdMale.Checked)
                PicPersonImage.Image = Resources.Male_512;
            else
                PicPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = (PicPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            CombCountryName.SelectedIndex = CombCountryName.FindString("Jordan");

            txtFirstName.Text = "";
            txtSecoundName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNatinalNo.Text = "";
            rdMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";


        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                CombCountryName.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            //just call in Update Mode 
            _Person = clsPerson.FindByPersonID(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID:" + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lbPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecoundName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNatinalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gender == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            CombCountryName.SelectedIndex = CombCountryName.FindString(_Person.CountryInfo.CountryName);


            if (_Person.ImagePath != "")
            {
                PicPersonImage.ImageLocation = _Person.ImagePath;
            }

            llRemoveImage.Visible = (_Person.ImagePath != "");


        }

        //here=======================
        private bool _HandlePersonImage()
        {      
            if (_Person.ImagePath != PicPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                      
                    }
                }

                if (PicPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = PicPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        PicPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void AddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            


            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = clsCountry.Find(CombCountryName.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecoundName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNatinalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rdMale.Checked)
                _Person.Gender = (short)enGendor.Male;
            else
                _Person.Gender = (short)enGendor.Female;

            _Person.NationalityCountryID = NationalityCountryID;

            if (PicPersonImage.ImageLocation != null)
                _Person.ImagePath = PicPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lbPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dataBack?.Invoke(_Person.PersonID);//send person id to from call this form
            }
            else             
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                PicPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PicPersonImage.ImageLocation = null;

            if (rdMale.Checked)
                PicPersonImage.Image = Resources.Male_512;
            else
                PicPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            if (PicPersonImage.ImageLocation == null)
                PicPersonImage.Image = Resources.Male_512;
        }

        private void rdFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (PicPersonImage.ImageLocation == null)
                PicPersonImage.Image = Resources.Female_512;
        }

        //Validate the Enter data from user     
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };




        }

        private void txtNatinalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNatinalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNatinalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNatinalNo, null);
            }

            if (txtNatinalNo.Text.Trim() != _Person.NationalNo && clsPerson.isPersonExist(txtNatinalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNatinalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNatinalNo, null);
            }
        }



    }
}
