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
using System.Windows.Input;

namespace BookaroomApplication
{
    public partial class VenueBooking : Form
    {

        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Venue", conn);
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
        public VenueBooking()
        {
            InitializeComponent();
            refreshGrid();

        }
        // Method to generate a VenueAvailabilityID (GUID)
        public string GenerateVenueAvailabilityID()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateConfirmationID()
        {
            return "CONF-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }


        private void btnBookVenue_Click(object sender, EventArgs e)
        {
            int intCapacity = Convert.ToInt32(numUpdownCapacity.Value);
           string strVenueID =cboVenueID.Text;
            string strVenueCategory = cboEventCategory.Text;
            string strEventType = cboEventType.Text;
           DateTime dtBookingDate = dtpBookingDate.Value;
            DateTime dtbookingTime = dtpStartTime.Value;            // Extract time (with full date, but we will use the time part)
            TimeSpan dtbookingTimeSpan = dtbookingTime.TimeOfDay;
            int intDuration = Convert.ToInt32( numupDuration.Value );
            bool blnValidInput = true;
       

            string conn = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
            Validate(blnValidInput, intCapacity, strVenueID, strVenueCategory, strEventType, intDuration);
           
             if (!blnValidInput)
             {
        return; // Exit if validation failed
             }

            int venueCapacity = GetVenueCapacity(strVenueID);
            if (intCapacity > venueCapacity)
            {
                MessageBox.Show($"The entered capacity of {intCapacity} exceeds the venue capacity of {venueCapacity}.");
                return; // Exit if the capacity is too high
            }

            // Proceed with venue ID check and booking
            if (IsVenueIDValid(conn, strVenueID, strVenueCategory, strEventType))
    {
        if (CheckBookingAvailability(conn, dtBookingDate))
        {
            // Add the booking if available
            AddBooking(conn, strVenueID, dtBookingDate, dtbookingTimeSpan, intDuration);
            MessageBox.Show("Booking confirmed.");
            Mainpage f1 = new Mainpage();
            this.Visible = false;
            f1.ShowDialog();
        }
        else
        {
            MessageBox.Show("The booking date is already taken. Please choose another date.");
        }
    }
    else
    {
        MessageBox.Show("Venue information incorrect.");
    }    


        }

        // Method to check booking date availability

        private bool CheckBookingAvailability(string connection, int intCapacity)
        {
            bool isVenueCapacityValid = true;


            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Venue WHERE Venue_Capacity = " + intCapacity ;
                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Venue_Capacity", intCapacity);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        isVenueCapacityValid = false; // Date is already taken
                    }
                }
            }

            return isVenueCapacityValid;

        }
        private bool IsVenueIDValid(string connection, string strVenueID , string strVenueCategory, string strEventType)
        {
            bool venueExists = false;

            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                try
                {
                    conn.Open();

                    // Query to check if the Venue_ID exists in the Venues table
                    string query = "SELECT COUNT(*) FROM Venue WHERE Venue_ID = ? AND Venue_Category = ? AND Event_Type = ?";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        // Add the parameter for Venue_ID
                        command.Parameters.AddWithValue("?", strVenueID);
                        command.Parameters.AddWithValue("?", strVenueCategory);
                        command.Parameters.AddWithValue("?", strEventType);

                        // ExecuteScalar() returns the count of rows matching the Venue_ID
                        int count = (int)command.ExecuteScalar();

                        // If count > 0, venue exists
                        venueExists = (count > 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return venueExists;
        }

        private bool CheckBookingAvailability(string connection, DateTime dtBookingDate)
        {
            bool isAvailable = true;

            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Bookings WHERE Booking_Date = #" + dtBookingDate.ToString("yyyy/MM/dd") + "#";
                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Booking_Date", dtBookingDate);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        isAvailable = false; // Date is already taken
                    }
                }
            }

            return isAvailable;
        }

        // Method to add a booking to the database



        private void AddBooking(string connection, string strVenueID, DateTime dtBookingDate, TimeSpan dtbookingTimeSpan, int intDuration)
        {

            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Bookings (Venue_ID, Booking_Date, Booking_Time, Booking_Duration) VALUES (?, ?, ?, ?)";

                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@VenueID", strVenueID);
                    command.Parameters.AddWithValue("@BookingDate", dtBookingDate);
                    command.Parameters.AddWithValue("@BookingTime", dtBookingDate.Date + dtbookingTimeSpan);
                    command.Parameters.AddWithValue("@BookingDuration", intDuration);

                    // Execute the insert query
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Retrieve the last inserted BookingID
                        string retrieveIDQuery = "SELECT @@IDENTITY AS BookingID"; // Retrieves the last auto-incremented BookingID
                        using (OleDbCommand retrieveCommand = new OleDbCommand(retrieveIDQuery, conn))
                        {
                            int bookingID = Convert.ToInt32(retrieveCommand.ExecuteScalar()); // Get the BookingID
                            MessageBox.Show($"Booking confirmed!\n\nBooking ID: {bookingID}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to add booking.");
                    }
                }
            }       
        }
        
        private bool Validate(bool blnValidInput, int intCapacity, string strVenueID, string strVenueCategory, string strEventType, int intDuration)
        {
            if (intCapacity <= 0)
            {
                MessageBox.Show("Invalid capacity");
                blnValidInput = false;
            }
            if (strVenueID == " ")
            {
                MessageBox.Show("Invalid Venue ID");
                blnValidInput = false;
            }
            if (strVenueCategory == " ")
            {
                MessageBox.Show("Invalid Venue Category");
                blnValidInput = false;
            }
            if (strEventType == " ")
            {
                MessageBox.Show("Invalid Event Type");
                blnValidInput = false;
            }
            if (intDuration <= 0)
            {
                MessageBox.Show("Invalid duration");
                blnValidInput = false;
            }
            return blnValidInput;
        }
        private int GetVenueCapacity(string strVenueID)
        {
            int capacity = 0;

            using (OleDbConnection conn = new OleDbConnection(this.conn.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Venue_Capacity FROM Venue WHERE Venue_ID = ?";

                using (OleDbCommand command = new OleDbCommand(query, conn))
                {
                    command.Parameters.AddWithValue("?", strVenueID);
                    object result = command.ExecuteScalar();

                    // Check if a result was returned
                    if (result != null)
                    {
                        capacity = Convert.ToInt32(result); // Get the capacity as an integer
                    }
                }
            }

            return capacity;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            f1.ShowDialog();
        }

        private void VenueBooking_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UserView f1 = new UserView();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
    

}

