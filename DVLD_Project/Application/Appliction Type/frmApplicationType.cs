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
    public partial class frmApplicationType : Form
    {
        public frmApplicationType()
        {
            InitializeComponent();
        }

        DataTable _dtApplicationType;
        private void frmApplicationType_Load(object sender, EventArgs e)
        {
            _dtApplicationType = clsApplicationType.GetAllApplicationTypes();
            guna2DataGridView1.DataSource = _dtApplicationType;
            lbNumRecord.Text = guna2DataGridView1.RowCount.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplictaionType frmUpdateApplictaionType = new frmUpdateApplictaionType((int)guna2DataGridView1.CurrentRow.Cells[0].Value);
            frmUpdateApplictaionType.ShowDialog();
            frmApplicationType_Load(null,null);

        }




    }
}
