namespace DVLD_Project
{
    partial class ApplicationLocalLicense
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcApplicationInfo = new System.Windows.Forms.TabControl();
            this.PersonInfo = new System.Windows.Forms.TabPage();
            this.btnNextPage = new Guna.UI2.WinForms.Guna2Button();
            this.ctrPersonCardWithFilter1 = new DVLD_Project.ctrPersonCardWithFilter();
            this.tpApplicationInfo = new System.Windows.Forms.TabPage();
            this.CbClassName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbApplDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbAppFees = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbCreatBy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox6 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbAppID = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox7 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.tcApplicationInfo.SuspendLayout();
            this.PersonInfo.SuspendLayout();
            this.tpApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // tcApplicationInfo
            // 
            this.tcApplicationInfo.Controls.Add(this.PersonInfo);
            this.tcApplicationInfo.Controls.Add(this.tpApplicationInfo);
            this.tcApplicationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcApplicationInfo.Location = new System.Drawing.Point(13, 66);
            this.tcApplicationInfo.Name = "tcApplicationInfo";
            this.tcApplicationInfo.SelectedIndex = 0;
            this.tcApplicationInfo.Size = new System.Drawing.Size(692, 471);
            this.tcApplicationInfo.TabIndex = 1;
            // 
            // PersonInfo
            // 
            this.PersonInfo.Controls.Add(this.btnNextPage);
            this.PersonInfo.Controls.Add(this.ctrPersonCardWithFilter1);
            this.PersonInfo.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PersonInfo.Location = new System.Drawing.Point(4, 29);
            this.PersonInfo.Name = "PersonInfo";
            this.PersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.PersonInfo.Size = new System.Drawing.Size(684, 438);
            this.PersonInfo.TabIndex = 0;
            this.PersonInfo.Text = "Personal  Info";
            this.PersonInfo.UseVisualStyleBackColor = true;
            // 
            // btnNextPage
            // 
            this.btnNextPage.BorderRadius = 1;
            this.btnNextPage.BorderThickness = 1;
            this.btnNextPage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextPage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextPage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextPage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextPage.FillColor = System.Drawing.Color.White;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.Black;
            this.btnNextPage.Location = new System.Drawing.Point(539, 397);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(121, 32);
            this.btnNextPage.TabIndex = 1;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.FilterEnabled = true;
            this.ctrPersonCardWithFilter1.Font = new System.Drawing.Font("Microsoft Tai Le", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(6, 6);
            this.ctrPersonCardWithFilter1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(668, 446);
            this.ctrPersonCardWithFilter1.TabIndex = 0;
            this.ctrPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrPersonCardWithFilter1_OnPersonSelected);
            // 
            // tpApplicationInfo
            // 
            this.tpApplicationInfo.Controls.Add(this.CbClassName);
            this.tpApplicationInfo.Controls.Add(this.lbApplDate);
            this.tpApplicationInfo.Controls.Add(this.lbAppFees);
            this.tpApplicationInfo.Controls.Add(this.lbCreatBy);
            this.tpApplicationInfo.Controls.Add(this.guna2PictureBox3);
            this.tpApplicationInfo.Controls.Add(this.guna2PictureBox6);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel4);
            this.tpApplicationInfo.Controls.Add(this.lbAppID);
            this.tpApplicationInfo.Controls.Add(this.guna2PictureBox2);
            this.tpApplicationInfo.Controls.Add(this.guna2PictureBox1);
            this.tpApplicationInfo.Controls.Add(this.guna2PictureBox7);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel3);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel2);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel5);
            this.tpApplicationInfo.Controls.Add(this.guna2HtmlLabel1);
            this.tpApplicationInfo.Location = new System.Drawing.Point(4, 29);
            this.tpApplicationInfo.Name = "tpApplicationInfo";
            this.tpApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpApplicationInfo.Size = new System.Drawing.Size(684, 438);
            this.tpApplicationInfo.TabIndex = 1;
            this.tpApplicationInfo.Text = "Application Info";
            this.tpApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // CbClassName
            // 
            this.CbClassName.BackColor = System.Drawing.Color.Transparent;
            this.CbClassName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbClassName.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CbClassName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CbClassName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CbClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CbClassName.ItemHeight = 30;
            this.CbClassName.Location = new System.Drawing.Point(260, 185);
            this.CbClassName.Name = "CbClassName";
            this.CbClassName.Size = new System.Drawing.Size(235, 36);
            this.CbClassName.TabIndex = 111;
            // 
            // lbApplDate
            // 
            this.lbApplDate.BackColor = System.Drawing.Color.Transparent;
            this.lbApplDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbApplDate.Location = new System.Drawing.Point(260, 136);
            this.lbApplDate.Name = "lbApplDate";
            this.lbApplDate.Size = new System.Drawing.Size(36, 26);
            this.lbApplDate.TabIndex = 110;
            this.lbApplDate.Text = "???";
            // 
            // lbAppFees
            // 
            this.lbAppFees.BackColor = System.Drawing.Color.Transparent;
            this.lbAppFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppFees.Location = new System.Drawing.Point(260, 255);
            this.lbAppFees.Name = "lbAppFees";
            this.lbAppFees.Size = new System.Drawing.Size(36, 26);
            this.lbAppFees.TabIndex = 108;
            this.lbAppFees.Text = "???";
            // 
            // lbCreatBy
            // 
            this.lbCreatBy.BackColor = System.Drawing.Color.Transparent;
            this.lbCreatBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreatBy.Location = new System.Drawing.Point(260, 312);
            this.lbCreatBy.Name = "lbCreatBy";
            this.lbCreatBy.Size = new System.Drawing.Size(36, 26);
            this.lbCreatBy.TabIndex = 107;
            this.lbCreatBy.Text = "???";
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Image = global::DVLD_Project.Properties.Resources.User_32__2;
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(212, 307);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(34, 31);
            this.guna2PictureBox3.TabIndex = 105;
            this.guna2PictureBox3.TabStop = false;
            // 
            // guna2PictureBox6
            // 
            this.guna2PictureBox6.Image = global::DVLD_Project.Properties.Resources.Calendar_32;
            this.guna2PictureBox6.ImageRotate = 0F;
            this.guna2PictureBox6.Location = new System.Drawing.Point(212, 131);
            this.guna2PictureBox6.Name = "guna2PictureBox6";
            this.guna2PictureBox6.Size = new System.Drawing.Size(34, 31);
            this.guna2PictureBox6.TabIndex = 104;
            this.guna2PictureBox6.TabStop = false;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(23, 307);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(117, 26);
            this.guna2HtmlLabel4.TabIndex = 103;
            this.guna2HtmlLabel4.Text = "Created By :";
            // 
            // lbAppID
            // 
            this.lbAppID.BackColor = System.Drawing.Color.Transparent;
            this.lbAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppID.Location = new System.Drawing.Point(260, 74);
            this.lbAppID.Name = "lbAppID";
            this.lbAppID.Size = new System.Drawing.Size(36, 26);
            this.lbAppID.TabIndex = 101;
            this.lbAppID.Text = "???";
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(212, 250);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(34, 31);
            this.guna2PictureBox2.TabIndex = 93;
            this.guna2PictureBox2.TabStop = false;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::DVLD_Project.Properties.Resources.New_Driving_License_32;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(212, 190);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(34, 31);
            this.guna2PictureBox1.TabIndex = 92;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2PictureBox7
            // 
            this.guna2PictureBox7.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.guna2PictureBox7.ImageRotate = 0F;
            this.guna2PictureBox7.Location = new System.Drawing.Point(212, 74);
            this.guna2PictureBox7.Name = "guna2PictureBox7";
            this.guna2PictureBox7.Size = new System.Drawing.Size(34, 31);
            this.guna2PictureBox7.TabIndex = 91;
            this.guna2PictureBox7.TabStop = false;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(23, 190);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(144, 26);
            this.guna2HtmlLabel3.TabIndex = 6;
            this.guna2HtmlLabel3.Text = "License Class :";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(23, 250);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(172, 26);
            this.guna2HtmlLabel2.TabIndex = 5;
            this.guna2HtmlLabel2.Text = "Application Fees :";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(23, 136);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(167, 26);
            this.guna2HtmlLabel5.TabIndex = 4;
            this.guna2HtmlLabel5.Text = "Application Date :";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(23, 79);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(181, 26);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "D.L.Application ID :";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTitle.Location = new System.Drawing.Point(137, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(460, 29);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "New Local Driving License Application";
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Crimson;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.Location = new System.Drawing.Point(480, 553);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 37);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.LightGreen;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(600, 553);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 37);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ApplicationLocalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 605);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tcApplicationInfo);
            this.Name = "ApplicationLocalLicense";
            this.Text = "ApplicationLocalLicense";
            this.Load += new System.EventHandler(this.ApplicationLocalLicense_Load);
            this.tcApplicationInfo.ResumeLayout(false);
            this.PersonInfo.ResumeLayout(false);
            this.tpApplicationInfo.ResumeLayout(false);
            this.tpApplicationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcApplicationInfo;
        private System.Windows.Forms.TabPage PersonInfo;
        private Guna.UI2.WinForms.Guna2Button btnNextPage;
        private ctrPersonCardWithFilter ctrPersonCardWithFilter1;
        private System.Windows.Forms.TabPage tpApplicationInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbAppID;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox7;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox6;
        private Guna.UI2.WinForms.Guna2ComboBox CbClassName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbApplDate;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbAppFees;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbCreatBy;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnSave;
    }
}