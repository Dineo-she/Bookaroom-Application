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
    public partial class UserView : Form
    {
        public UserView()
        {
            InitializeComponent();
        }
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb";
        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {

            // Get the selected user type
            string userType = cboUser.SelectedItem.ToString();

            if (userType == "Student")
            {
                // Retrieve and show student details
                string studentInput = txtUserID.Text;
                if (int.TryParse(studentInput, out int intStudentID))
                {
                    LoadStudentData(intStudentID); // Load student data if valid Student ID is entered
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer for Student ID.");
                }
            }
            else if (userType == "Lecturer")
            {
                // Retrieve and show lecturer details
                string lecturerInput = txtUserID.Text;
                if (int.TryParse(lecturerInput, out int intLecturerID))
                {
                    LoadLecturerData(intLecturerID); // Load lecturer data if valid Lecturer ID is entered
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer for Lecturer ID.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid user type.");
            }
        }

        private void LoadStudentData(int intStudentID)
        {
            string query = "SELECT Student_ID, Student_FName, Student_LName, Student_Email, Student_ContactNo FROM Students WHERE Student_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("?", intStudentID);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Display the student data in the corresponding labels
                        lblUserID.Text = reader["Student_ID"].ToString();
                        lblFName.Text = reader["Student_FName"].ToString();
                        lblLName.Text = reader["Student_LName"].ToString();
                        lblEmail.Text = reader["Student_Email"].ToString();
                        lblContactNo.Text = reader["Student_ContactNo"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No student found with the entered Student ID.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadLecturerData(int intLecturerID)
        {
            string query = "SELECT Lecturer_ID, Lecturer_FName, Lecturer_LName, Lecturer_Email, Lecturer_ContactNo FROM Lecturers WHERE Lecturer_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("?", intLecturerID);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Display the lecturer data in the corresponding labels
                        lblUserID.Text = reader["Lecturer_ID"].ToString();
                        lblFName.Text = reader["Lecturer_FName"].ToString();
                        lblLName.Text = reader["Lecturer_LName"].ToString();
                        lblEmail.Text = reader["Lecturer_Email"].ToString();
                        lblContactNo.Text = reader["Lecturer_ContactNo"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No lecturer found with the entered Lecturer ID.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Mainpage f1 = new Mainpage();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
