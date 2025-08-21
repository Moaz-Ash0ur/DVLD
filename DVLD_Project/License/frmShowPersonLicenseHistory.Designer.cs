namespace DVLD_Project
{
    partial class frmShowPersonLicenseHistory
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
            this.ctrPersonCardWithFilter1 = new DVLD_Project.ctrPersonCardWithFilter();
            this.ctrDriverLicence1 = new DVLD_Project.ctrDriverLicence();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.FilterEnabled = true;
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(12, 48);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(791, 367);
            this.ctrPersonCardWithFilter1.TabIndex = 0;
            this.ctrPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrPersonCardWithFilter1_OnPersonSelected);
            // 
            // ctrDriverLicence1
            // 
            this.ctrDriverLicence1.Location = new System.Drawing.Point(12, 421);
            this.ctrDriverLicence1.Name = "ctrDriverLicence1";
            this.ctrDriverLicence1.Size = new System.Drawing.Size(941, 312);
            this.ctrDriverLicence1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(348, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(289, 39);
            this.lblTitle.TabIndex = 130;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 728);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctrDriverLicence1);
            this.Controls.Add(this.ctrPersonCardWithFilter1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowPersonLicenseHistory";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "frmShowPersonLicenseHistory";
            this.Load += new System.EventHandler(this.frmShowPersonLicenseHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrPersonCardWithFilter ctrPersonCardWithFilter1;
        private ctrDriverLicence ctrDriverLicence1;
        private System.Windows.Forms.Label lblTitle;
    }
}