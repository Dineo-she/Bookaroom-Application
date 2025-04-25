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
    public partial class UpdateBooking : Form
    {
        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Bookings", conn);
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
        public UpdateBooking()
        {
            InitializeComponent();
            refreshGrid();
        }
        public static string InputBox(string prompt, string title, string defaultValue)
        {
            // code for the input box
            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;

        int intBookingID;
        
        
        private void btnUpdateBooking_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");

            // Get values from controls
            string strVenueID = cboVenueID.Text;
            int intDuration = Convert.ToInt32(numupDuration.Value);
            DateTime dtBookingDate = dtpBookingDate.Value;
            DateTime dtBookingTime = dtpStartTime.Value;

            try
            {
                // Open the connection
                conn.Open();

                // Define the SQL update query using parameters
                OleDbCommand cmd = new OleDbCommand("UPDATE Bookings SET Booking_Date = @1, Booking_Duration = @2, Venue_ID = @3, Booking_Time = @4 WHERE Booking_ID = @5", conn);

                // Add the parameters with the correct values
                cmd.Parameters.AddWithValue("@1", dtBookingDate);
                cmd.Parameters.AddWithValue("@2", intDuration);
                cmd.Parameters.AddWithValue("@3", strVenueID);
                cmd.Parameters.AddWithValue("@4", dtBookingTime);
                cmd.Parameters.AddWithValue("@5", intBookingID);  // Assuming `intBookingID` is defined somewhere

                // Display the SQL query with parameter values (for debugging)
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
                    MessageBox.Show("1 record updated");
                }
                else
                {
                    MessageBox.Show("No record found with the specified ID.");
                }

                // Refresh the grid (assumed to be a method defined elsewhere)
                refreshGrid();
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        
    
}


        private void UpdateBooking_Load(object sender, EventArgs e)
        {
            intBookingID = Convert.ToInt32(InputBox("Pleae enter Booking ID","Booking ID",""));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
