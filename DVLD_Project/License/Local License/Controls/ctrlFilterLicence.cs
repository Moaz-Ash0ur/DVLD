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
    public partial class ctrlFilterLicence : UserControl
    {

        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }


        public ctrlFilterLicence()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

         int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrDriverLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return ctrDriverLicenseInfo1.SelectedLicenseInfo; } }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenceID.Text = LicenseID.ToString();
            ctrDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(_LicenseID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenceID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenceID.Text);
            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenceID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenceID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenceID, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtLicenceID, null);
            }
        }

        private void txtLicenceID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }
        }

        private void ctrlFilterLicence_Load(object sender, EventArgs e)
        {

        }

        public void txtLicenseIDFocus()
        {
            txtLicenceID.Focus();
        }

    }
}
