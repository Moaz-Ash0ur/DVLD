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
    public partial class frmShowPersonLicenseHistory : Form
    {
        int _PersonID;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }


        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrPersonCardWithFilter1.LoadPersonInfo1(_PersonID);
                ctrPersonCardWithFilter1.FilterEnabled = false; 
                ctrDriverLicence1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrPersonCardWithFilter1.Enabled = true;
                ctrPersonCardWithFilter1.FilterFocus();
            }


        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            //if call this form from another way to check if person not sent

            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrDriverLicence1.Clear();
            }
            else
                ctrDriverLicence1.LoadInfoByPersonID(_PersonID);
        }



    }
}
