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
    public partial class ctrDriverLicence : UserControl
    {
        public ctrDriverLicence()
        {
            InitializeComponent();          
        }

        int _DriverID;
        clsDriver _Driver;
        DataTable _dtDriverLocalLicenseHistory;
        DataTable _dtDriverInternationalLicenseHistory;

        private void _LoadLocalLicenseInfo()
        {
            _dtDriverLocalLicenseHistory = clsDriver.GetLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicenseHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }

         }

       private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicenseHistory;
            lblLocalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }

        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("No Driver Linked Wiht This Person","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _DriverID =  _Driver.DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();


        }

        public void Clear()
        {
            _dtDriverLocalLicenseHistory.Clear();
            _dtDriverInternationalLicenseHistory.Clear();

        }


        private void ctrDriverLicence_Load(object sender, EventArgs e)
        {

        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmShowLicenseInfo = new frmLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frmShowLicenseInfo.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // frmShowInternationalInfo frm = new frmShowInternationalInfo((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            // frm.ShowDialog();
        }




    }
}
