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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD_Project
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

       
        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
          
            dgvPeople.DataSource = _dtAllPeople;
            lbCountecord.Text = dgvPeople.Rows.Count.ToString();
            _FormatPeopleGrid();
        }

        private void _LoadAllPeople()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            dgvPeople.DataSource = _dtAllPeople;
            comboBoxFilter.SelectedIndex = 0;
            lbCountecord.Text = dgvPeople.Rows.Count.ToString();
            _FormatPeopleGrid();
        }

        private void _FormatPeopleGrid()
        {
            dgvPeople.Columns["PersonID"].HeaderText = "Person ID"; 
            dgvPeople.Columns["NationalNo"].HeaderText = "National No.";
            dgvPeople.Columns["FirstName"].HeaderText = "First Name";
            dgvPeople.Columns["LastName"].HeaderText = "Last Name";
            dgvPeople.Columns["GendorCaption"].HeaderText = "Gender";
            dgvPeople.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
            dgvPeople.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dgvPeople.Columns["CountryName"].HeaderText = "Nationality";
            dgvPeople.Columns["Phone"].HeaderText = "Phone";
            dgvPeople.Columns["Email"].HeaderText = "Email";

            //Width
            dgvPeople.Columns["PersonID"].Width = 70;
            dgvPeople.Columns["NationalNo"].Width = 70;
            dgvPeople.Columns["FirstName"].Width = 100;
            dgvPeople.Columns["LastName"].Width = 100;
            dgvPeople.Columns["GendorCaption"].Width = 80;
            dgvPeople.Columns["DateOfBirth"].Width = 90;
            dgvPeople.Columns["CountryName"].Width = 70;
            dgvPeople.Columns["Phone"].Width = 80;
            dgvPeople.Columns["Email"].Width = 110;

            //non show
            dgvPeople.Columns["ImagePath"].Visible = false;
            dgvPeople.Columns["ThirdName"].Visible = false;
            dgvPeople.Columns["Address"].Visible = false;
            dgvPeople.Columns["NationalityCountryID"].Visible = false;
            dgvPeople.Columns["SecondName"].Visible = false;


        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            _LoadAllPeople();
        }

        //start own fun

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails showPerson = new PersonDetails((int)dgvPeople.CurrentRow.Cells[0].Value);
            showPerson.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();  
            addEditPerson.ShowDialog();

            _RefreshPeoplList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            addEditPerson.ShowDialog();
            _RefreshPeoplList();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {
                if (clsPerson.Delete((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeoplList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void PicAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson();
            addEditPerson.ShowDialog();

            _RefreshPeoplList();

        }
   
        //edit wiht instrcutor
        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (comboBoxFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (textboxFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                lbCountecord.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textboxFilter.Text.Trim());
            else
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textboxFilter.Text.Trim());

                lbCountecord.Text = dgvPeople.Rows.Count.ToString();
        }

        private void textboxFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBoxFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            textboxFilter.Visible = (comboBoxFilter.Text != "None");

            if (textboxFilter.Visible)
            {
                textboxFilter.Text = "";
                textboxFilter.Focus();
            }

        }



    }
}
