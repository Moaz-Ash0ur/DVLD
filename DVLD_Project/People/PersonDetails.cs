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
    public partial class PersonDetails : Form
    {

        public PersonDetails(int personID)
        {
            InitializeComponent();
            ctrPersonCard1.LoadPersonInfo(personID);
        }

        public PersonDetails(string NationalNo)
        {
            InitializeComponent();
            ctrPersonCard1.LoadPersonInfo(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      



    }
}
