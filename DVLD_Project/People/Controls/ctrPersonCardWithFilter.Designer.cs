namespace DVLD_Project
{
    partial class ctrPersonCardWithFilter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnSerachPerson = new System.Windows.Forms.Button();
            this.btnAddNewPerson = new Guna.UI2.WinForms.Guna2PictureBox();
            this.txtboxFilter = new Guna.UI2.WinForms.Guna2TextBox();
            this.CombFilterBy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrPersonCard1 = new DVLD_Project.ctrPersonCard();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnSerachPerson);
            this.gbFilter.Controls.Add(this.btnAddNewPerson);
            this.gbFilter.Controls.Add(this.txtboxFilter);
            this.gbFilter.Controls.Add(this.CombFilterBy);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Location = new System.Drawing.Point(3, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(783, 70);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnSerachPerson
            // 
            this.btnSerachPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerachPerson.Image = global::DVLD_Project.Properties.Resources.SearchPerson;
            this.btnSerachPerson.Location = new System.Drawing.Point(509, 19);
            this.btnSerachPerson.Name = "btnSerachPerson";
            this.btnSerachPerson.Size = new System.Drawing.Size(44, 36);
            this.btnSerachPerson.TabIndex = 19;
            this.btnSerachPerson.UseVisualStyleBackColor = true;
            this.btnSerachPerson.Click += new System.EventHandler(this.btnSerachPerson_Click);
            this.btnSerachPerson.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnSerachPerson_KeyPress);
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnAddNewPerson.Image = global::DVLD_Project.Properties.Resources.Add_Person_72;
            this.btnAddNewPerson.ImageRotate = 0F;
            this.btnAddNewPerson.Location = new System.Drawing.Point(559, 19);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(44, 36);
            this.btnAddNewPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddNewPerson.TabIndex = 3;
            this.btnAddNewPerson.TabStop = false;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // txtboxFilter
            // 
            this.txtboxFilter.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtboxFilter.DefaultText = "";
            this.txtboxFilter.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtboxFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtboxFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtboxFilter.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtboxFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtboxFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtboxFilter.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtboxFilter.Location = new System.Drawing.Point(318, 19);
            this.txtboxFilter.Name = "txtboxFilter";
            this.txtboxFilter.PasswordChar = '\0';
            this.txtboxFilter.PlaceholderText = "";
            this.txtboxFilter.SelectedText = "";
            this.txtboxFilter.Size = new System.Drawing.Size(185, 36);
            this.txtboxFilter.TabIndex = 2;
            this.txtboxFilter.Validating += new System.ComponentModel.CancelEventHandler(this.txtboxFilter_Validating);
            // 
            // CombFilterBy
            // 
            this.CombFilterBy.BackColor = System.Drawing.Color.Transparent;
            this.CombFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CombFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombFilterBy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CombFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CombFilterBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CombFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CombFilterBy.ItemHeight = 30;
            this.CombFilterBy.Items.AddRange(new object[] {
            "National No",
            "Person ID"});
            this.CombFilterBy.Location = new System.Drawing.Point(124, 19);
            this.CombFilterBy.Name = "CombFilterBy";
            this.CombFilterBy.Size = new System.Drawing.Size(188, 36);
            this.CombFilterBy.TabIndex = 1;
            this.CombFilterBy.SelectedIndexChanged += new System.EventHandler(this.CombFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find By :";
            // 
            // ctrPersonCard1
            // 
            this.ctrPersonCard1.Location = new System.Drawing.Point(3, 79);
            this.ctrPersonCard1.Name = "ctrPersonCard1";
            this.ctrPersonCard1.Size = new System.Drawing.Size(792, 313);
            this.ctrPersonCard1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrPersonCard1);
            this.Name = "ctrPersonCardWithFilter";
            this.Size = new System.Drawing.Size(798, 407);
            this.Load += new System.EventHandler(this.ctrPersonCardWithFilter_Load);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrPersonCard ctrPersonCard1;
        private System.Windows.Forms.GroupBox gbFilter;
        private Guna.UI2.WinForms.Guna2PictureBox btnAddNewPerson;
        private Guna.UI2.WinForms.Guna2TextBox txtboxFilter;
        private Guna.UI2.WinForms.Guna2ComboBox CombFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSerachPerson;
    }
}
