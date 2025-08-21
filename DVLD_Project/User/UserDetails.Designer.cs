namespace DVLD_Project
{
    partial class UserDetails
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
            this.ctrUserInfo1 = new DVLD_Project.ctrUserInfo();
            this.SuspendLayout();
            // 
            // ctrUserInfo1
            // 
            this.ctrUserInfo1.Location = new System.Drawing.Point(12, 3);
            this.ctrUserInfo1.Name = "ctrUserInfo1";
            this.ctrUserInfo1.Size = new System.Drawing.Size(806, 429);
            this.ctrUserInfo1.TabIndex = 0;
            // 
            // UserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 437);
            this.Controls.Add(this.ctrUserInfo1);
            this.Name = "UserDetails";
            this.Text = "UserDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrUserInfo ctrUserInfo1;
    }
}