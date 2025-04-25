using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookaroomApplication
{
    public partial class Login : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int intUser_ID = Convert.ToInt32(txtUserID.Text);   
            string strPassword = txtPassword.Text;  
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
            conn.Open();

            bool blnValidInput = true;
            Validate(blnValidInput, intUser_ID, strPassword);

            
            if ((cboUserTypeLogin.SelectedIndex == 1 ) && (blnValidInput == true))

            {
                cmd = new OleDbCommand("select * from Students where Student_ID= " + intUser_ID + " and  Password = '" + strPassword+ "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Login Successful");
                    Mainpage f1 = new Mainpage();
                    this.Visible = false;
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
                conn.Close();
            }
            else
                if((cboUserTypeLogin.SelectedIndex == 2) && (blnValidInput == true))
            {
                cmd = new OleDbCommand("select * from Lecturer where Lecturer_ID= " + intUser_ID + " and  Password = '" + txtPassword.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Login Successful");
                    Mainpage f1 = new Mainpage();
                    this.Visible = false;
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
                conn.Close();
            }
            else
                if ((cboUserTypeLogin.SelectedIndex == 0) && (blnValidInput == true))
            {
                cmd = new OleDbCommand("select * from Admin where Admin_ID= " + intUser_ID + " and  Password = '" + txtPassword.Text + "'", conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Login Successful");
                    AdminPage f1 = new AdminPage();
                    this.Visible = false;
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
                conn.Close();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (cboUserTypeSignUp.SelectedIndex == 0)
            {
                StudentRegister f1 = new StudentRegister();
                this.Visible = false;
                f1.ShowDialog();
            }
            else
                  if (cboUserTypeSignUp.SelectedIndex == 1)
            {
               LecturerRegister f1 = new LecturerRegister();
                this.Visible = false;
                f1.ShowDialog();
            }
        }
        private bool Validate(bool blnValidInput, int intUser_ID, string strPassword)
        {
            
            if (intUser_ID == 0)
            {
                MessageBox.Show("Invalid userID");
                blnValidInput = false;
            }
            if (strPassword == "")
            {
                MessageBox.Show("Invalid Password");
                blnValidInput = false;
            }
            if (cboUserTypeLogin.Text == "")
            {
                MessageBox.Show("Invalid User Type");
                blnValidInput = false;
            }

            return blnValidInput;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
