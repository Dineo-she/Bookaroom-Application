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
    public partial class ViewBooking : Form
    {
        public ViewBooking()
        {
            InitializeComponent();
        }
        string conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb";
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

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void ViewBooking_Load(object sender, EventArgs e)
        {
            string input = InputBox("Please enter booking ID?", "Booking ID", "1");
            if (int.TryParse(input, out int intBookingID))
            {
                LoadData(intBookingID); // Load data if a valid integer BookingID is entered
            }
            else
            {
                MessageBox.Show("Please enter a valid integer for Booking ID.");
            }
        }


        private void LoadData(int intBookingID)
        {
            // SQL Query to check if the Booking ID exists and fetch the associated data
            string query = "SELECT Bookings.Booking_Date, Bookings.Booking_Duration,Bookings.Booking_Time, Bookings.Venue_ID, Venue.Venue_Capacity, Venue.Event_Type,  Venue.Venue_Category " +
               "FROM Bookings INNER JOIN Venue ON Bookings.Venue_ID = Venue.Venue_ID " +
               "WHERE Bookings.Booking_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(conn))

            {
                // Create command object
                OleDbCommand command = new OleDbCommand(query, connection);

                // Add the BookingID parameter to the SQL query
                command.Parameters.AddWithValue("?", intBookingID);


                try
                {
                    // Open connection
                    connection.Open();


                    // Execute query and obtain data
                    OleDbDataReader reader = command.ExecuteReader();

                    // Check if there is data (i.e., if the BookingID exists)
                    if (reader.Read())
                    {
                        // If the Booking ID exists, fetch the details
                        DateTime bookingDate = Convert.ToDateTime(reader["Booking_Date"]);
                        DateTime bookingTime = Convert.ToDateTime(reader["Booking_Time"]);
                        int intDuration = Convert.ToInt32(reader["Booking_Duration"]);
                        string strVenueID = (reader["Venue_ID"]).ToString();
                        int venueCapacity = Convert.ToInt32(reader["Venue_Capacity"]);
                        string eventType = reader["Event_Type"].ToString();
                        string Venuecategory = reader["Venue_Category"].ToString();

                        // Display the data in the corresponding labels
                        lblDate.Text =  bookingDate.ToShortDateString();
                        lblBookingTime.Text = bookingTime.ToShortTimeString();
                        lblVenue.Text =  strVenueID;
                        lblDuration.Text = intDuration.ToString() + " hours";
                        lblCapacity.Text =  venueCapacity.ToString();
                        lblTypeOfEvent.Text = eventType;
                        lblVenueCategory.Text = Venuecategory;
                    }
                    else
                    {
                        // If no data found, show a message to the user
                        MessageBox.Show("No booking found with the entered Booking ID.");

                        
                    }

                    // Close the reader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error: " + ex.Message);
                }
                
            }
        }

        private void btnViewBooking_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UserView f1 = new UserView();
            this.Visible = false;
            f1.ShowDialog();
        }
    }

}
