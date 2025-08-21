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
    public partial class ctrApplicationInfo : UserControl
    {
        public ctrApplicationInfo()
        {
            InitializeComponent();
        }

        private clsApplication _Application;

        private int _ApplicationID = -1;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }


        int _PersonID = 0;

        public int GetPersonID
        {
            get { return _PersonID; }
        }

        private void _FillApplicationInfo()
        {

            _ApplicationID = _Application.ApplicationID;
            lbAppID.Text = _Application.ApplicationID.ToString();
            lbStatus.Text = _Application.StatusText;
            lbType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
            lbFees.Text = _Application.PaidFees.ToString();
            lbApplicantPerson.Text = _Application.PersonInfo.FullName;
            lbDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lbDateStaus.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lbCreatedby.Text = _Application.CreatedByUserInfo.UserName;
        }

        public void _ShowApplicationInfo(int AppID)
        {
          
            _Application = clsApplication.FindBaseApplication(AppID);
            _ApplicationID = AppID;

            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillApplicationInfo();

        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lbAppID.Text = "[????]";
            lbStatus.Text = "[????]";
            lbType.Text = "[????]";
            lbFees.Text = "[????]";
            lbApplicantPerson.Text = "[????]";
            lbDate.Text = "[????]";
            lbDateStaus.Text = "[????]";
            lbCreatedby.Text = "[????]";

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails frm = new PersonDetails(_Application.ApplicantPersonID);
            frm.ShowDialog();

            _ShowApplicationInfo(_ApplicationID);
        }






    }
}
