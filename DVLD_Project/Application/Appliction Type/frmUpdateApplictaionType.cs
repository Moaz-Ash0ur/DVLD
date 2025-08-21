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
    public partial class frmUpdateApplictaionType : Form
    {
        private int _ApplicationTypeID;
        clsApplicationType _clsAppType;


        public frmUpdateApplictaionType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;  
        }

        private void _FillApplictionInfo()
        {
             _clsAppType = clsApplicationType.FindApplicationType(_ApplicationTypeID);

            if (_clsAppType != null)
            {
              lbIDApp.Text = _clsAppType.ApplicationTypeID.ToString();
              txtAppTtile.Text = _clsAppType.ApplicationTypeTitle;
              txtFessApp.Text = _clsAppType.ApplicationFees.ToString();
            }
        }
  
        private void frmUpdateApplictaionType_Load(object sender, EventArgs e)
        {
            _FillApplictionInfo();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            _clsAppType.ApplicationTypeTitle = txtAppTtile.Text.Trim();
            _clsAppType.ApplicationFees =  Convert.ToInt64(txtFessApp.Text);

            if (_clsAppType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Information", MessageBoxButtons.OK);

            }
            else
                MessageBox.Show("Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);
        }

        private void txtAppTtile_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppTtile.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAppTtile, "Title cannot be empty!");
            }
            else          
                errorProvider1.SetError(txtAppTtile, null);
           
        }

        private void txtFessApp_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFessApp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFessApp, "Fees cannot be empty!");
                return;
            }
            else           
                errorProvider1.SetError(txtFessApp, null);
            

            if (!clsValidation.IsNumber(txtFessApp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFessApp, "Invalid Number.");
            }
            else
            
                errorProvider1.SetError(txtFessApp, null);          
        }



    }
}
