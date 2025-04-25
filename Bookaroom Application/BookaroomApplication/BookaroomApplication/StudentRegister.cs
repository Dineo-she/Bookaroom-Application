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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookaroomApplication
{
    public partial class StudentRegister : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        public StudentRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
         

            string strFName = txtFName.Text;
            string strLName = txtLName.Text;
            string strEmail = txtEmail.Text;
            string strContactNo = mtbContactNo.Text;
            string strStudentID = txtStudentID.Text;
            string strOPassword = txtOPassword.Text;
            string strCPassword = txtCPassword.Text;

            bool blnValidInput = true;
                
            ValidateInput(blnValidInput,  strFName,  strLName,  strEmail,  strContactNo,  strStudentID,  strOPassword,  strCPassword);
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb";

           

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
               
                
                
                
                // Step 4: Open the connection
                conn.Open();

                // Step 5: Define the SQL query (parameterized query to avoid SQL injection)
                string query = "INSERT INTO Students (Student_ID, [Password], Student_FName, Student_LName,Student_Email, Student_ContactNo) VALUES (?, ?, ?, ?, ?,?)";

                // Step 6: Create the command
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Step 7: Add parameters to the command
                    cmd.Parameters.AddWithValue("@Student_ID", strStudentID);  // Add Student_ID parameter
                    cmd.Parameters.AddWithValue("@Password", strOPassword);     // Add Password parameter
                    cmd.Parameters.AddWithValue("@Student_FName", strFName);             // Add First Name parameter
                    cmd.Parameters.AddWithValue("@Student_LName", strLName);  // Add Last Name parameter
                    cmd.Parameters.AddWithValue("@Student_Email", strEmail);     // Add email parameter
                    cmd.Parameters.AddWithValue("@Student_ContactNo", strContactNo); // Add ContactNo parameter
                    // Step 8: Execute the command (returns the number of rows affected)
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Step 9: Display a message based on the result
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }
        private bool ValidateInput(bool blnValidInput, string strFName, string strLName, string strEmail, string strContactNo, string strStudentID, string strOPassword, string strCPassword)
        {

            if (strFName == "")
            {
                MessageBox.Show("Please enter valid First Name", " First Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strLName == "")
            {
                MessageBox.Show("Please enter valid Last Name", "Last Name Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strEmail == "")
            {
                MessageBox.Show("Please enter valid Email", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strContactNo == "")
            {
                MessageBox.Show("Please enter valid Contact No", "Contact No Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strStudentID == "")
            {
                MessageBox.Show("Please enter valid StudentID", " Student ID Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strOPassword == "")
            {
                MessageBox.Show("Please enter valid Password", " Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }

            if (strCPassword == "")
            {
                MessageBox.Show("Please enter valid Password", " Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blnValidInput = false;
            }


            if (strOPassword != strCPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blnValidInput = false;
            }

            else
            {
                MessageBox.Show("Password confirmed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return blnValidInput;
        }
    }
}
