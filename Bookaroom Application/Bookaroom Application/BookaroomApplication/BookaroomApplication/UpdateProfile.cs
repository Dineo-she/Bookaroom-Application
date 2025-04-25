using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BookaroomApplication
{
    public partial class UpdateProfile : Form
    {
        // Connection to the database
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        int userID;
        string connectionString = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");

        public UpdateProfile()
        {
            InitializeComponent();
            refreshGrid();
        }

        // Method to refresh the grid with the latest data
        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Students", conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error connecting to database: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // InputBox method for prompting user for input
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

        // Button click event to update the profile
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
                    using (OleDbCommand cmd = new OleDbCommand("UPDATE Students SET Student_FName = @1, Student_LName = @2, Student_Email = @3, Student_ContactNo = @4, [Password] = @5 WHERE Student_ID = @6", conn))
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

        // Event handler for loading the form and prompting the user for a Student ID
        private void UpdateProfile_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(InputBox("Please enter Student ID", "Student ID", ""));
        }

        // Unused event handler for the DataGridView click event
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Event handler for a PictureBox click event that navigates to AdminPage
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminPage f1 = new AdminPage();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
