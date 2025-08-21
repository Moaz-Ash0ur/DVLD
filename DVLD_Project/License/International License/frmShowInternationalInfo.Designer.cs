namespace DVLD_Project
{
    partial class frmShowInternationalInfo
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
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ctrlInternationalInfo1 = new DVLD_Project.ctrlInternationalInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::DVLD_Project.Properties.Resources.International_32;
            this.pictureBox11.Location = new System.Drawing.Point(246, 5);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(40, 45);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 41;
            this.pictureBox11.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Crimson;
            this.label10.Location = new System.Drawing.Point(292, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(262, 29);
            this.label10.TabIndex = 42;
            this.label10.Text = "International License ";
            // 
            // ctrlInternationalInfo1
            // 
            this.ctrlInternationalInfo1.Location = new System.Drawing.Point(12, 56);
            this.ctrlInternationalInfo1.Name = "ctrlInternationalInfo1";
            this.ctrlInternationalInfo1.Size = new System.Drawing.Size(832, 292);
            this.ctrlInternationalInfo1.TabIndex = 0;
            // 
            // frmShowInternationalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 336);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ctrlInternationalInfo1);
            this.Name = "frmShowInternationalInfo";
            this.Text = "frmShowInternationalInfo";
            this.Load += new System.EventHandler(this.frmShowInternationalInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlInternationalInfo ctrlInternationalInfo1;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label label10;
    }
}