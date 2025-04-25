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
    public partial class Mainpage : Form
    {
        public Mainpage()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UserView f1 = new UserView();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ViewBooking f1 = new ViewBooking();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            UpdateBooking f1 = new UpdateBooking();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            VenueBooking f1 = new VenueBooking();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       
        private void btnBookVenue_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnViewBooking_Click(object sender, EventArgs e)
        {
           
        }
    }
}
