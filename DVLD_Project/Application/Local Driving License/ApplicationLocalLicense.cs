using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project
{
    public partial class ApplicationLocalLicense : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _LocalAppLicenseID = -1;
        private int _PersonIDSelected = -1;
        clsAppLocalLicense _LocalDrivingAppLicense;

    
        public ApplicationLocalLicense()
        {
            InitializeComponent();     
            _Mode = enMode.AddNew;        
        }

        public ApplicationLocalLicense(int id)
        {
            InitializeComponent();

            _LocalAppLicenseID = id;
            _Mode = enMode.Update;
        }

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                CbClassName.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();

            if (_Mode == enMode.AddNew)
            {

                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingAppLicense = new clsAppLocalLicense();
                ctrPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                CbClassName.SelectedIndex = 2;
                lbAppFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lbApplDate.Text = DateTime.Now.ToShortDateString();
                lbCreatBy.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }

        }

        private void _LoadData()
        {
            //in update mode
            ctrPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingAppLicense = clsAppLocalLicense.FindByLocalDrivingAppLicenseID(_LocalAppLicenseID);

            if (_LocalDrivingAppLicense == null)
            {
                MessageBox.Show("No Application Found", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrPersonCardWithFilter1.LoadPersonInfo1(_LocalDrivingAppLicense.ApplicantPersonID);
            lbAppID.Text = _LocalDrivingAppLicense.LocalDrivingLicenseApplicationID.ToString();
            lbApplDate.Text = clsFormat.DateToShort(_LocalDrivingAppLicense.ApplicationDate);
            CbClassName.SelectedIndex = CbClassName.FindString(clsLicenseClass.Find(_LocalDrivingAppLicense.LicenseClassID).ClassName);
            lbAppFees.Text = _LocalDrivingAppLicense.PaidFees.ToString();
            lbCreatBy.Text = clsUser.FindByUserID(_LocalDrivingAppLicense.CreatedByUserID).UserName;

        }

        private void DataBackEvent(int PersonID)
        {
            _PersonIDSelected = PersonID;
            ctrPersonCardWithFilter1.LoadPersonInfo1(PersonID);
        }

        private void ApplicationLocalLicense_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicenseClassID = clsLicenseClass.Find(CbClassName.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_PersonIDSelected, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ctrPersonCardWithFilter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have  license with the same driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingAppLicense.ApplicantPersonID = ctrPersonCardWithFilter1.PersonID;
            _LocalDrivingAppLicense.ApplicationDate = DateTime.Now;
            _LocalDrivingAppLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingAppLicense.ApplicationStatus1 = clsApplication.enApplicationStatus.New;
            _LocalDrivingAppLicense.LastStatusDate = DateTime.Now;
            _LocalDrivingAppLicense.PaidFees = Convert.ToSingle(lbAppFees.Text);
            _LocalDrivingAppLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingAppLicense.LicenseClassID = LicenseClassID;


            if (_LocalDrivingAppLicense.Save())
            {
                lbAppID.Text = _LocalDrivingAppLicense.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonIDSelected = obj;
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }

            if (ctrPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }

            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrPersonCardWithFilter1.FilterFocus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
