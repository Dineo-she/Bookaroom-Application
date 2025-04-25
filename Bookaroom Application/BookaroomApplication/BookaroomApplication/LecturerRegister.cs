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
    public partial class LecturerRegister : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        public LecturerRegister()
        {
            InitializeComponent();
        }
        private void btnLSignUp_Click(object sender, EventArgs e)
        {
           

            string strFName = txtFName.Text;
            string strLName = txtLName.Text;
            string strEmail = txtEmail.Text;
            string strContactNo = txtContact.Text;
            string strLecturerID = txtLecturerID.Text;
            string strOPassword = txtOPassword.Text;
            string strCPassword = txtCPassword.Text;

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                
                conn.Open();

                
                string query = "INSERT INTO Lecturer (Lecturer_ID, [Password], Lecturer_FName, Lecturer_LName,Lecturer_Email, Lecturer_ContactNo) VALUES (?, ?, ?, ?, ?,?)";

                // Step 6: Create the command
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
               {
                    // Step 7: Add parameters to the command
                    cmd.Parameters.AddWithValue("@Lecturer_ID", strLecturerID);  // Add Lecturer_ID parameter
                    cmd.Parameters.AddWithValue("@Password", strOPassword);     // Add Password parameter
                    cmd.Parameters.AddWithValue("@Lecturer_FName", strFName);             // Add Name parameter
                    cmd.Parameters.AddWithValue("@Lecturer_LName", strLName);  // Add Lecturer_ID parameter
                    cmd.Parameters.AddWithValue("@Lecturer_Email", strEmail);     // Add Password parameter
                    cmd.Parameters.AddWithValue("@Lecturer_ContactNo", strContactNo);
                    // Step 8: Execute the command (returns the number of rows affected)
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        MessageBox.Show("Data successfully inserted into the database.");
                        Mainpage f1 = new Mainpage();
                        this.Visible = false;
                        f1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error inserting data into the database.");
                    }
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
          
        }
    }
}
