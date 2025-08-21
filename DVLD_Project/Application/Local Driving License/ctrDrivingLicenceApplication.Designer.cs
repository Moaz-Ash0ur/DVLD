namespace DVLD_Project
{
    partial class ctrDrivingLicenceApplication
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbClassType = new System.Windows.Forms.Label();
            this.lbPasstedTestCount = new System.Windows.Forms.Label();
            this.lbLocalAppID = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lnShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ctrApplicationInfo1 = new DVLD_Project.ctrApplicationInfo();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox11);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.lbClassType);
            this.groupBox1.Controls.Add(this.lbPasstedTestCount);
            this.groupBox1.Controls.Add(this.lbLocalAppID);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.lnShowLicenseInfo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(786, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driving License Application Info";
            // 
            // lbClassType
            // 
            this.lbClassType.AutoSize = true;
            this.lbClassType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClassType.Location = new System.Drawing.Point(567, 35);
            this.lbClassType.Name = "lbClassType";
            this.lbClassType.Size = new System.Drawing.Size(29, 20);
            this.lbClassType.TabIndex = 12;
            this.lbClassType.Text = "??";
            this.lbClassType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPasstedTestCount
            // 
            this.lbPasstedTestCount.AutoSize = true;
            this.lbPasstedTestCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPasstedTestCount.Location = new System.Drawing.Point(567, 78);
            this.lbPasstedTestCount.Name = "lbPasstedTestCount";
            this.lbPasstedTestCount.Size = new System.Drawing.Size(29, 20);
            this.lbPasstedTestCount.TabIndex = 11;
            this.lbPasstedTestCount.Text = "??";
            this.lbPasstedTestCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLocalAppID
            // 
            this.lbLocalAppID.AutoSize = true;
            this.lbLocalAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLocalAppID.Location = new System.Drawing.Point(197, 40);
            this.lbLocalAppID.Name = "lbLocalAppID";
            this.lbLocalAppID.Size = new System.Drawing.Size(29, 20);
            this.lbLocalAppID.TabIndex = 10;
            this.lbLocalAppID.Text = "??";
            this.lbLocalAppID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD_Project.Properties.Resources.LocalDriving_License;
            this.pictureBox4.Location = new System.Drawing.Point(131, 71);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 27);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // lnShowLicenseInfo
            // 
            this.lnShowLicenseInfo.AutoSize = true;
            this.lnShowLicenseInfo.Location = new System.Drawing.Point(167, 78);
            this.lnShowLicenseInfo.Name = "lnShowLicenseInfo";
            this.lnShowLicenseInfo.Size = new System.Drawing.Size(148, 20);
            this.lnShowLicenseInfo.TabIndex = 8;
            this.lnShowLicenseInfo.TabStop = true;
            this.lnShowLicenseInfo.Text = "Show License Info :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(301, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Application For License :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(384, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Passted Test :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "D.L.App ID :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrApplicationInfo1
            // 
            this.ctrApplicationInfo1.Location = new System.Drawing.Point(3, 153);
            this.ctrApplicationInfo1.Name = "ctrApplicationInfo1";
            this.ctrApplicationInfo1.Size = new System.Drawing.Size(786, 225);
            this.ctrApplicationInfo1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.TestType_512;
            this.pictureBox1.Location = new System.Drawing.Point(514, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 195;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD_Project.Properties.Resources.LocalDriving_License;
            this.pictureBox2.Location = new System.Drawing.Point(514, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 194;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.pictureBox11.Location = new System.Drawing.Point(131, 34);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(31, 26);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 197;
            this.pictureBox11.TabStop = false;
            // 
            // ctrDrivingLicenceApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrApplicationInfo1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrDrivingLicenceApplication";
            this.Size = new System.Drawing.Size(795, 385);
            this.Load += new System.EventHandler(this.ctrDrivingLicenceApplication_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbClassType;
        private System.Windows.Forms.Label lbPasstedTestCount;
        private System.Windows.Forms.Label lbLocalAppID;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.LinkLabel lnShowLicenseInfo;
        private ctrApplicationInfo ctrApplicationInfo1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox11;
    }
}
