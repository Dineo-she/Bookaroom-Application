namespace BookaroomApplication
{
    partial class UpdateUserMiddlePage
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
            this.btnUpdateLecturerProfile = new System.Windows.Forms.Button();
            this.btnUpdateStudentProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdateLecturerProfile
            // 
            this.btnUpdateLecturerProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateLecturerProfile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnUpdateLecturerProfile.Location = new System.Drawing.Point(179, 68);
            this.btnUpdateLecturerProfile.Name = "btnUpdateLecturerProfile";
            this.btnUpdateLecturerProfile.Size = new System.Drawing.Size(271, 62);
            this.btnUpdateLecturerProfile.TabIndex = 72;
            this.btnUpdateLecturerProfile.Text = "Update Lecturer";
            this.btnUpdateLecturerProfile.UseVisualStyleBackColor = true;
            this.btnUpdateLecturerProfile.Click += new System.EventHandler(this.btnUpdateLecturerProfile_Click);
            // 
            // btnUpdateStudentProfile
            // 
            this.btnUpdateStudentProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateStudentProfile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnUpdateStudentProfile.Location = new System.Drawing.Point(179, 159);
            this.btnUpdateStudentProfile.Name = "btnUpdateStudentProfile";
            this.btnUpdateStudentProfile.Size = new System.Drawing.Size(271, 62);
            this.btnUpdateStudentProfile.TabIndex = 73;
            this.btnUpdateStudentProfile.Text = "Update Student";
            this.btnUpdateStudentProfile.UseVisualStyleBackColor = true;
            this.btnUpdateStudentProfile.Click += new System.EventHandler(this.btnUpdateStudentProfile_Click);
            // 
            // UpdateUserMiddlePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(632, 282);
            this.Controls.Add(this.btnUpdateStudentProfile);
            this.Controls.Add(this.btnUpdateLecturerProfile);
            this.Name = "UpdateUserMiddlePage";
            this.Text = "UpdateUserMiddlePage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateLecturerProfile;
        private System.Windows.Forms.Button btnUpdateStudentProfile;
    }
}