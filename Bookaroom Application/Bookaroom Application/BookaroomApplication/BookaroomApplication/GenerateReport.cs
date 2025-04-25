using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookaroomApplication
{
    public partial class GenerateReport : Form
    {
        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");
        string connectionString = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Morei Dineo\Downloads\Bookaroom Application\Bookaroom Application\BookaroomApplication\Bookaroom.accdb");

        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Bookings", conn);
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
        public GenerateReport()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from the UI
                DateTime startDate = dtpStartTime.Value;
                DateTime endDate = dtbEndTime.Value;

                if (cboVenueSelection.SelectedItem == null || cboVenueCategory.SelectedItem == null || cboReportType.SelectedItem == null || cboExportFormat.SelectedItem == null)
                {
                    MessageBox.Show("Please make sure all selections (venue, category, report type, export format) are made.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedVenue = cboVenueSelection.SelectedItem.ToString();
                string selectedCategory = cboVenueCategory.SelectedItem.ToString();
                string reportType = cboReportType.SelectedItem.ToString();
                string exportFormat = cboExportFormat.SelectedItem.ToString();

                DataTable reportData = null;

                // Fetch data based on report type
                if (reportType == "Monthly Booking Summary Report")
                {
                    reportData = GetMonthlyBookingSummary(startDate, endDate);
                }
                else if (reportType == "Venue Booking Frequency Report")
                {
                    reportData = GetVenueBookingFrequencyReport(startDate, endDate);
                }
                else if (reportType == "Category-Specific Booking Summary")
                {
                    reportData = GetCategorySpecificBookingSummary(startDate, endDate, selectedCategory);
                }

                if (reportData != null)
                {
                    dataGridView1.DataSource = reportData;

                    // Generate report based on export format
                    if (exportFormat == "PDF")
                    {
                        GeneratePDFCategoryReport(reportData);
                    }
                    else if (exportFormat == "Excel")
                    {
                        GenerateExcelCategoryReport(reportData);
                    }
                    else
                    {
                        MessageBox.Show("Unknown export format selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No report data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport_Load(object sender, EventArgs e)
        {

        }


        private DataTable GetMonthlyBookingSummary(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT FORMAT(Booking_Date, 'yyyy/MM') AS BookingMonth, COUNT(*) AS TotalBookings 
                     FROM Bookings 
                     WHERE Booking_Date BETWEEN ? AND ? 
                     GROUP BY FORMAT(Booking_Date, 'yyyy/MM') 
                     ORDER BY BookingMonth";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("?", startDate);
                cmd.Parameters.AddWithValue("?", endDate);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        private DataTable GetVenueBookingFrequencyReport(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            // Define the SQL query
            string query = @"SELECT Venue.Venue_ID, COUNT(*) AS TotalBookings 
                     FROM Bookings 
                     INNER JOIN Venue ON Bookings.Venue_ID = Venue.Venue_ID 
                     WHERE Booking_Date BETWEEN @Start AND @End
                     GROUP BY Venue.Venue_Name 
                     ORDER BY Venue.Venue_Name";

            // Use the OleDbConnection and OleDbCommand within a try-catch for error handling
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Define the parameters explicitly, to avoid ambiguity with dates
                        cmd.Parameters.Add("@Start", OleDbType.Date).Value = startDate;
                        cmd.Parameters.Add("@End", OleDbType.Date).Value = endDate;

                        // Open the database connection
                        conn.Open();

                        // Fill the DataTable using an OleDbDataAdapter
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle potential errors
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Return the DataTable with the results
            return dt;
        }


        private DataTable GetCategorySpecificBookingSummary(DateTime startDate, DateTime endDate, string selectedCategory)
        {// Create a new DataTable to hold the result
            DataTable dt = new DataTable();

            // SQL query with parameter placeholders
            string query = @"SELECT Venue.Venue_Name, Venue.Venue_Category, Bookings.Booking_Date, Bookings.Booking_Duration
                     FROM Bookings
                     INNER JOIN Venue ON Bookings.Venue_ID = Venue.Venue_ID
                     WHERE Booking_Date BETWEEN @Start AND @End 
                     AND Venue.Venue_Category = @Category
                     ORDER BY Booking_Date";

            try
            {
                // Establish a connection to the database
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Add the parameters with their values
                        cmd.Parameters.AddWithValue("@Start", startDate);
                        cmd.Parameters.AddWithValue("@End", endDate);
                        cmd.Parameters.AddWithValue("@Category", selectedCategory);

                        // Open the connection
                        conn.Open();

                        // Use OleDbDataAdapter to execute the query and fill the DataTable
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle potential errors
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Return the filled DataTable
            return dt;
        }
        public void GeneratePDFCategoryReport(DataTable reportData)
        {
            string reportDirectory = @"C:\Users\Morei Dineo\Downloads\Bookaroom Application\Reports";

            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            using (Document doc = new Document())
            {
                PdfWriter.GetInstance(doc, new FileStream(Path.Combine(reportDirectory, "CategoryBookingSummary.pdf"), FileMode.Create));
                doc.Open();
                doc.Add(new Paragraph("Category Booking Summary Report"));
                PdfPTable table = new PdfPTable(reportData.Columns.Count);

                foreach (DataColumn column in reportData.Columns)
                {
                    table.AddCell(new Phrase(column.ColumnName));
                }

                foreach (DataRow row in reportData.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        table.AddCell(new Phrase(cell.ToString()));
                    }
                }

                doc.Add(table);
                doc.Close();
            }

            MessageBox.Show("PDF report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        public void GenerateExcelCategoryReport(DataTable reportData)
        {
            try
            {
                // Ensure the report directory exists
                string reportDirectory = @"C:\Users\Morei Dineo\Downloads\Bookaroom Application\Reports";

                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }

                // Define the Excel file path and delete the file if it already exists
                FileInfo file = new FileInfo(Path.Combine(reportDirectory, "CategoryBookingSummary.xlsx"));

                if (file.Exists)
                {
                    // Optional: Delete the existing file to avoid conflicts
                    file.Delete();
                }

                // Ensure license context for EPPlus version 5+
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // Create a new worksheet in the Excel file
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("CategoryBookingSummary");

                    // Load data from the DataTable into the worksheet starting at cell A1
                    worksheet.Cells["A1"].LoadFromDataTable(reportData, true);

                    // Save the package to commit changes to the file
                    package.Save();
                }

                // Inform the user that the report was generated successfully
                MessageBox.Show("Excel report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                MessageBox.Show($"An error occurred while generating the Excel report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdminPage f1 = new AdminPage();
            this.Visible = false;
            f1.ShowDialog();
        }
    }
}
