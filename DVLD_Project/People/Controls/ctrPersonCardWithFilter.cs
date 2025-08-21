using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrPersonCardWithFilter : UserControl
    {
        //event when select ID
        public event Action<int> OnPersonSelected;
        //raise the event and sent it | who subscribe on it
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }

        string _NationalNo;


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
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

        public ctrPersonCardWithFilter()
        {
            InitializeComponent();
 
        }


        private int _PersonID = -1;

        public int PersonID
        {
            get { return ctrPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrPersonCard1.SelectedPersonInfo; }
        }


        //insted put all logic from other ctrl here sepreatd it
        public void LoadPersonInfo1(int PersonID)
        {
            CombFilterBy.SelectedIndex = 1;
            txtboxFilter.Text = PersonID.ToString();
            FindPerson();
        }

        public void LoadPersonInfo1(string NationalNo)
        {
            CombFilterBy.SelectedIndex = 0;
            txtboxFilter.Text = NationalNo.ToString();
            FindPerson();
        }

        private void FindPerson()
        {
            switch (CombFilterBy.Text)
            {
                case "Person ID":
                    ctrPersonCard1.LoadPersonInfo(int.Parse(txtboxFilter.Text));

                    break;

                case "National No":
                    ctrPersonCard1.LoadPersonInfo(txtboxFilter.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrPersonCard1.PersonID);
        }

        private void CombFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtboxFilter.Text = "";
            txtboxFilter.Focus();
        }

        private void ctrPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            CombFilterBy.SelectedIndex = 0;
            txtboxFilter.Focus();
        }

        private void txtboxFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtboxFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtboxFilter, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtboxFilter, null);
            }
        }

        public void LoadPersonInfoByNationalNo(string NationalNo)
         {
            ctrPersonCard1.LoadPersonInfo(NationalNo);
            txtboxFilter.Text = NationalNo.ToString();
            CombFilterBy.SelectedIndex = 0;
            txtboxFilter.Enabled = false;
            CombFilterBy.Enabled = false;
            btnSerachPerson.Enabled = false;
            btnAddNewPerson.Enabled = false;
         }
  
        private void DataBackEvent(int PersonID)
        {
            // Handle the data received

            CombFilterBy.SelectedIndex = 1;
            txtboxFilter.Text = PersonID.ToString();
            ctrPersonCard1.LoadPersonInfo(PersonID);
        }
            
        public void FilterFocus()
        {
            txtboxFilter.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {

            AddEditPerson frm1 = new AddEditPerson();
            frm1.dataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();

        }
       
        private void btnSerachPerson_Click(object sender, EventArgs e)
        {

            //if (!this.ValidateChildren())
            //{
            //    //Here we dont continue becuase the form is not valid
            //    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;

            //}

            FindPerson();

        }

        private void btnSerachPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnSerachPerson.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (CombFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }





    }
}
