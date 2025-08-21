using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project
{
    public partial class frmlocalDrivinfLicenseApp : Form
    {
        private int _AppLocalLicense = -1;

        public frmlocalDrivinfLicenseApp(int AppLocal)
        {
            InitializeComponent();
            _AppLocalLicense = AppLocal;

        }

        private void frmlocalDrivinfLicenseApp_Load(object sender, EventArgs e)
        {
            ctrDrivingLicenceApplication1._ShowLocalLiceAppInfo(_AppLocalLicense);

        }



    }
}
