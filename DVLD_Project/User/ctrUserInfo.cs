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
    public partial class ctrUserInfo : UserControl
    {
        public ctrUserInfo()
        {
            InitializeComponent();
        }
        clsUser _User;

        private void ctrUserInfo_Load(object sender, EventArgs e)
        {

        }

        public void LoadUserInfo(int UserID)
        {

            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("User Not Found!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrPersonCard1.LoadPersonInfo(_User.PersonID);
            lbUserID.Text = _User.UserID.ToString();
            lbUsername.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lbUserIsActive.Text = "Yes";
            else
                lbUserIsActive.Text = "No";
        }

        private void _ResetPersonInfo()
        {
            ctrPersonCard1.ResetPersonInfo();
            lbUserID.Text = "[???]";
            lbUserID.Text = "[???]";
            lbUserIsActive.Text = "[???]";
        }

            


    }
}
