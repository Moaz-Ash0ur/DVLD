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

namespace DVLD_Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void _Login()
        {
          string Password = clsUtil.HashPassword(txtPassword.Text.Trim());
            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), Password);

            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                
                if (!user.IsActive)
                {
                    txtUsername.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmHomePage frm = new frmHomePage(this);
                frm.ShowDialog();


            }
            else
            {
                txtUsername.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _Login();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DateTime today = DateTime.Now.Date;
            lblDate.Text = today.ToString("yyyy-MM-dd");

            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUsername.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;  
        }

            
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            lblTime.Text = currentTime.ToString("hh:mm:ss tt");
        }

    }
}
