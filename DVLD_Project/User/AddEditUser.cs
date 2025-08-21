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

namespace DVLD_Project
{
    public partial class AddEditUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public clsUser _User;
        public int _UserID;

        public AddEditUser()
        {
            InitializeComponent();

             _Mode = enMode.AddNew;          
        }

        public AddEditUser(int id)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = id;
          
        }

        //In Load Set Values In Form
        private void _ResetDefualtValuesInLoad()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTitleUserScreen.Text = "Add New User";
                _User = new clsUser();

                tabPageLogin.Enabled = false;

                ctrPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lbTitleUserScreen.Text = "Update User";

                tabPageLogin.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUsername.Text = "";
            txtPass.Text = "";
            txtPassConfirm.Text = "";
            chIsActive.Checked = true;

        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lbUserID.Text = _User.UserID.ToString();
            txtUsername.Text = _User.UserName;
            txtPass.Text = _User.Password;
            txtPassConfirm.Text = _User.Password;
            chIsActive.Checked = _User.IsActive;
            ctrPersonCardWithFilter1.LoadPersonInfo1(_User.PersonID);
        }

        private void AddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValuesInLoad();

            if (_Mode == enMode.Update)
                _LoadData();

        }
      

         //Edit wiht Instructor ***
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tabPageLogin.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["tabPageLogin"];
                return;
            }

            //incase of add new mode.
            if (ctrPersonCardWithFilter1.PersonID != -1)
            {

                if (clsUser.IsUserExistForPersonID(ctrPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tabPageLogin.Enabled = true;
                    tabControl1.SelectedTab = tabControl1.TabPages["tabPageLogin"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrPersonCardWithFilter1.FilterFocus();

            }

        }
        //                      ***


        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.UserName = txtUsername.Text;
            _User.Password = txtPass.Text;
            _User.PersonID = ctrPersonCardWithFilter1.PersonID;
            _User.IsActive = chIsActive.Checked;

            if (_User.Save())
            {
                _UserID = _User.UserID;
                lbUserID.Text = _User.UserID.ToString();
                lbTitleUserScreen.Text = "Update User";
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Information", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);      
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //get PersonID use event
        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            //_PersonID = obj;
            //ctrPersonCardWithFilter1.LoadPersonInfo1(_PersonID);
        }


        //Validating Input here
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }


            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUsername, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUsername, null);
                }
            }
            else
            {

                if (_User.UserName != txtUsername.Text.Trim())//mean it changed and check if have use like same name
                {
                    if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUsername, "Username is Used !");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, null);
                    }
                }

            }

        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPass, "Password Can`t be empty");
            }
            else
            {
                errorProvider1.SetError(txtPass, null);
            }
        }

        private void txtPassConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassConfirm.Text.Trim() != txtPass.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassConfirm, "Password Confirm not match Current Password!");
            }
            else
            {
                errorProvider1.SetError(txtPassConfirm, null);
            }
        }




    }
}
