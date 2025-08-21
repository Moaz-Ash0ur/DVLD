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
    public partial class frmScheduleTes : Form
    {

        int _LocalDrivingLicenseAppID = -1;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int _TestAppoitmentID = -1;


        public frmScheduleTes(int LocalDrivingLicenseAppID, clsTestType.enTestType TestTypeID, int TestAppoitmentID1=-1)
        {
            InitializeComponent();

            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;   
            _TestTypeID = TestTypeID;
            _TestAppoitmentID = TestAppoitmentID1;

        }

        private void frmScheduleTes_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseAppID, _TestAppoitmentID);
        }














    }
}
