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
    public partial class About : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["LecturerDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

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

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int id = int.Parse(IdTextBox.Text);
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            decimal salary;

            if (decimal.TryParse(SalaryTextBox.Text, out salary))
            {
                string filePath = UploadFile(); // You might want to handle file updating differently.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Employees SET Name = @Name, Surname = @Surname, Salary = @Salary, FilePath = @FilePath WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Salary", salary);
                    command.Parameters.AddWithValue("@FilePath", filePath);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                ClearFields();
                BindGrid();
            }
        }

        ///-----------------------------------------------------------------------------------------------------------////

    }
}