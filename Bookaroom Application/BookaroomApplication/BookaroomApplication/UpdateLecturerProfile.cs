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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookaroomApplication
{
    public partial class UpdateLecturerProfile : Form
    {
        int userID;
        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Lecturer", conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Connection:" + e);
            }
            finally
            {
                conn.Close();
            }
        }
        public static string InputBox(string prompt, string title, string defaultValue)
        {
            // Code for the input box
            InputBoxDialog ib = new InputBoxDialog
            {
                FormPrompt = prompt,
                FormCaption = title,
                DefaultValue = defaultValue
            };
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }
        public UpdateLecturerProfile()
        {
            InitializeComponent();
            refreshGrid();
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        string connectionString = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");



        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
             // Get values from form controls
            string userFName = txtFName.Text;    // First name from TextBox
            string userLName = txtLName.Text;    // Last name from TextBox
            string userEmail = txtEmail.Text;    // Email from TextBox
            string userPhone = txtContactNo.Text; // Phone number from TextBox
            string userPassword = txtOPassword.Text; // Password from TextBox

            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(userFName) || string.IsNullOrWhiteSpace(userLName) ||
                    string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(userPhone) ||
                    string.IsNullOrWhiteSpace(userPassword))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Define the SQL update query with parameters
                    using (OleDbCommand cmd = new OleDbCommand("UPDATE Lecturer SET Lecturer_FName = @1, Lecturer_LName = @2, Lecturer_Email = @3, Lecturer_ContactNo = @4, [Password] = @5 WHERE Lecturer_ID = @6", conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@1", userFName);
                        cmd.Parameters.AddWithValue("@2", userLName);
                        cmd.Parameters.AddWithValue("@3", userEmail);
                        cmd.Parameters.AddWithValue("@4", userPhone);
                        cmd.Parameters.AddWithValue("@5", userPassword);  // Use the hashed password
                        cmd.Parameters.AddWithValue("@6", userID);  // Assume userID is set elsewhere in your code

                        // Output the query and parameters for debugging
                        Console.WriteLine("SQL Query: " + cmd.CommandText);
                        foreach (OleDbParameter param in cmd.Parameters)
                        {
                            Console.WriteLine($"{param.ParameterName} = {param.Value}");
                        }

                        // Execute the update command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully.");
                            refreshGrid();  // Refresh the grid after the update
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminPage f1 = new AdminPage();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void UpdateLecturerProfile_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(InputBox("Please enter Student ID", "Student ID", ""));
        }
    }
}
