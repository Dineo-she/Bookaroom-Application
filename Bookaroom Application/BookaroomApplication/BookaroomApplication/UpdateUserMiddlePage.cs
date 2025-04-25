using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookaroomApplication
{
    public partial class UpdateUserMiddlePage : Form
    {
        public UpdateUserMiddlePage()
        {
            InitializeComponent();
        }

        private void btnUpdateLecturerProfile_Click(object sender, EventArgs e)
        {
            UpdateLecturerProfile f1 = new UpdateLecturerProfile();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void btnUpdateStudentProfile_Click(object sender, EventArgs e)
        {
            UpdateProfile f1 = new UpdateProfile();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
