using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmChangePassword : Form
    {

        int _UserID;
        clsUser _User; 
       
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;         
        }
        
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("User Not Found !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrUserInfo1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            _User =  clsUser.FindByUserID(_UserID);
            string PasswordHash =  clsUtil.HashPassword(txtCurrentPass.Text.Trim());
            if (_User.Password != PasswordHash)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPass, "Your Password not match the Current Password");
            }
            else{
                errorProvider1.SetError(txtCurrentPass, null);
            }

     }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPass, "Password Can`t be empty");
            }
            else
            {
                errorProvider1.SetError(txtNewPass, null);
            }
        }

        private void txtPassConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassConfirm.Text.Trim() != txtNewPass.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassConfirm, "Password Confirm not match Current Password!");
            }
            else
            {
                errorProvider1.SetError(txtPassConfirm, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {          
            _User.Password = clsUtil.HashPassword(txtNewPass.Text.Trim());

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully","Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Faild Changed Password","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }






    }
}
