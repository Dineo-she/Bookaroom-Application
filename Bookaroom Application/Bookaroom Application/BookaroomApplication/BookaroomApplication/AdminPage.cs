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
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            UpdateUserMiddlePage f1 = new UpdateUserMiddlePage();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBoxCreateBooking_Click(object sender, EventArgs e)
        {
            UpdateBooking  f1 = new UpdateBooking();
            this.Visible = false;
            f1.ShowDialog();

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            GenerateReport f1 = new GenerateReport();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
