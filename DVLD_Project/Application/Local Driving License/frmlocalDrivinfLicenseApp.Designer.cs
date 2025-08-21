namespace DVLD_Project
{
    partial class frmlocalDrivinfLicenseApp
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
            this.ctrDrivingLicenceApplication1 = new DVLD_Project.ctrDrivingLicenceApplication();
            this.SuspendLayout();
            // 
            // ctrDrivingLicenceApplication1
            // 
            this.ctrDrivingLicenceApplication1.Location = new System.Drawing.Point(12, 12);
            this.ctrDrivingLicenceApplication1.Name = "ctrDrivingLicenceApplication1";
            this.ctrDrivingLicenceApplication1.Size = new System.Drawing.Size(795, 385);
            this.ctrDrivingLicenceApplication1.TabIndex = 0;
            // 
            // frmlocalDrivinfLicenseApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 402);
            this.Controls.Add(this.ctrDrivingLicenceApplication1);
            this.Name = "frmlocalDrivinfLicenseApp";
            this.Text = "frmlocalDrivinfLicenseApp";
            this.Load += new System.EventHandler(this.frmlocalDrivinfLicenseApp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrDrivingLicenceApplication ctrDrivingLicenceApplication1;
    }
}