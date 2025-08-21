using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }



        DataTable dt;
        private void _ShowAllUsers()
        {
            dt = clsUser.GetAllUsers();
            guna2DataGridView1.DataSource = dt; 
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _ShowAllUsers();
            cmbIsActive.SelectedIndex = 0;
            comboBoxFilter.SelectedIndex = 0;
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();    
            addEditUser.ShowDialog();
            _ShowAllUsers();

        }

        private void PicAddUser_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.ShowDialog();
            _ShowAllUsers();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            addEditUser.ShowDialog();
            _ShowAllUsers();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails userDetails  = new UserDetails((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            userDetails.ShowDialog();   
        }

        private void chnagePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            changePassword.ShowDialog();
            _ShowAllUsers();

        }

        //Filter data
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFilter.Text == "Is Active")
            {
                txtboxFilter.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }

            else
            {
                txtboxFilter.Visible = (comboBoxFilter.Text != "None");
                cmbIsActive.Visible = false;
                if (cmbIsActive.Text == "None")              
                    txtboxFilter.Enabled = false;              
                else
                    txtboxFilter.Enabled = true;
                
                txtboxFilter.Text = "";
                txtboxFilter.Focus();

            }

        }

        private void cmbIsActive_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cmbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                dt.DefaultView.RowFilter = "";
            else
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();

        }

        //Use Sample delegate For Practice
        public delegate string FilterDelegate(string filterColumn, string filterValue);

        public string LikeFilter(string filterColumn, string filterValue) => string.Format("[{0}] LIKE '{1}%'", filterColumn, filterValue);
        


        private void textboxFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";

            switch (comboBoxFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            FilterDelegate filterDelegate;

            if (txtboxFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = guna2DataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "FullName" || FilterColumn == "UserName")
                filterDelegate = LikeFilter;
            else
                filterDelegate = (string filterColumn, string filterValue) => string.Format("[{0}] = {1}", filterColumn, filterValue);

            dt.DefaultView.RowFilter = filterDelegate(FilterColumn, txtboxFilter.Text.Trim());

            lblRecordsCount.Text = dt.Rows.Count.ToString();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to delete this User", "Warnning", MessageBoxButtons.OKCancel); ;

            if (result == DialogResult.OK)
            {
                if (clsUser.Delete((int)guna2DataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully", "Confirm", MessageBoxButtons.OKCancel); ;
                }
                else
                {
                    MessageBox.Show("User Faild to delete", "Error", MessageBoxButtons.OKCancel); ;

                }
            }
           
            _ShowAllUsers();
        }






    }
}
