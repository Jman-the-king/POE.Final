using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace POE.Final
{
    public partial class _Default : Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["LecturerDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        

            ///-----------------------------------------------------------------------------------------------------------////


            protected void SubmitButton_Click(object sender, EventArgs e)
            {
                string lastName = LastNameTextBox.Text;
                //int hoursWorked;
                //decimal hourlyRate;
                dynamic id = IdTextBox.Text;

                //if (decimal.TryParse(HourlyRateTextBox.Text, out hourlyRate))
                //{
                //    string filePath = UploadFile();

                //    using (SqlConnection connection = new SqlConnection(connectionString))
                //    {
                //        string query = "INSERT INTO Lecturer (Id, LastName, HoursWorked, HourlyRate,  FilePath) VALUES (@ids, @LastName, @HoursWorked, @HourlyRate, @FilePath)";
                //        SqlCommand command = new SqlCommand(query, connection);
                //        command.Parameters.AddWithValue("@ids", id);
                //        command.Parameters.AddWithValue("@LastName", lastName);
                //        command.Parameters.AddWithValue("@HoursWorked", hoursWorked);
                //        command.Parameters.AddWithValue("@HourlyRate", hourlyRate);
                //        command.Parameters.AddWithValue("@FilePath", filePath);
                //        connection.Open();
                //        command.ExecuteNonQuery();
                //    }

                // Parse Hours Worked and Hourly Rate from the GridView
                if (int.TryParse(HourlyRateTextBox.Text, out int hoursWorked) &&  // Hours Worked
                    decimal.TryParse(HourlyRateTextBox.Text, out decimal hourlyRate)) // Hourly Rate
                {
                    // Display Hours Worked and Hourly Rate
                    HoursWorkedTextBox.Text = hoursWorked.ToString();
                    HourlyRateTextBox.Text = hourlyRate.ToString("F2");

                    // Calculate the Salary
                    decimal salary = CalculateSalary(hoursWorked, hourlyRate);

                    // Display the Salary
                    SalaryTextBox.Text = salary.ToString("F2");

                    // Make the result panel visible
                    ResultPanel.Visible = true;

                // File upload and database operation (only if both validations pass)
                string filePath = UploadFile();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Lecturer (Id, LastName, HoursWorked, HourlyRate, FilePath) VALUES (@ids, @LastName, @HoursWorked, @HourlyRate, @FilePath)";
                        SqlCommand command = new SqlCommand(query, connection);

                        string Rstatus = "Pending";

                        // Add parameters to the query
                        command.Parameters.AddWithValue("@ids", id);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@HoursWorked", hoursWorked);
                        command.Parameters.AddWithValue("@HourlyRate", hourlyRate);
                        command.Parameters.AddWithValue("@FilePath", filePath);
                        command.Parameters.AddWithValue("@Status", Rstatus);

                        // Execute the query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }


                    ClearFields();
                    BindGrid();

                }
                else
                {
                    
                    if (!int.TryParse(HoursWorkedTextBox.Text, out _))
                    {
                        HoursWorkedTextBox.Text = "Invalid number of hours worked";
                    }

                    if (!decimal.TryParse(HourlyRateTextBox.Text, out _))
                    {
                        HourlyRateTextBox.Text = "Invalid hourly rate";
                    }

                    // Handle invalid input
                    ResultPanel.Visible = false; // Ensure the panel remains hidden if inputs are invalid
            }

            }
            
        

        private decimal CalculateSalary(int hoursWorked, decimal hourlyRate)
        {
            return hoursWorked * hourlyRate;
        }



        ///-----------------------------------------------------------------------------------------------------------////

    
            
            private void BindGrid()
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Lecturer";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    LecturerGridView.DataSource = dt;
                    LecturerGridView.DataBind();
                }
            }

            ///-----------------------------------------------------------------------------------------------------------////
            private void ClearFields()
            {
                IdTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                HoursWorkedTextBox.Text = string.Empty;
                HourlyRateTextBox.Text = string.Empty;
            }

            ///-----------------------------------------------------------------------------------------------------------////

            protected void EmployeesGridView_SelectedIndexChanged(object sender, EventArgs e)
            {

                string Rstatus = "Pending";

                GridViewRow row = LecturerGridView.SelectedRow;
                IdTextBox.Text = row.Cells[0].Text;
                LastNameTextBox.Text = row.Cells[1].Text;
                HoursWorkedTextBox.Text = row.Cells[2].Text;
                HourlyRateTextBox.Text = row.Cells[3].Text;
                SalaryTextBox.Text = row.Cells[4].Text;
                Rstatus = row.Cells[5].Text;
            }

            ///-----------------------------------------------------------------------------------------------------------////

            private string UploadFile()
            {
                if (FileUploadControl.HasFile)
                {
                    string fileName = Path.GetFileName(FileUploadControl.FileName);
                    string filePath = Server.MapPath("~/Uploads/") + fileName;
                    FileUploadControl.SaveAs(filePath);
                    return "~/Uploads/" + fileName; // Store relative path in the database
                }
                return null; // No file uploaded
            }

        }
    }




