using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState_Project.Data_Access_Point
{
    public class SqlConnect_point
    {
        private string connectionString { get; set; } = "";

        public SqlConnect_point()
        {
            this.connectionString = "Server=DESKTOP-O60KE7T\\SQLEXPRESS;Database=Project_RealState; Trusted_Connection=True;";
        }


        //This method is for SELECT queries
        public DataTable ExecuteSelectQuery(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Create and open the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        // Use SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }
        
        //This method is for SELECT queries but with parameters
        public DataTable ExecuteSelectQuery(string query , SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Create and open the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // Add the parameters to the command
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        // Use SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }


        //This method is for SELECT queries and returns a single row
        public DataRow GetSingleRow(string query, SqlParameter[] parameters)
        {
            DataRow row = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Create a DataRow dynamically based on the schema
                                DataTable dt = new DataTable();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                                }
                                row = dt.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader.GetValue(i);
                                }
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                MessageBox.Show("No row found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return row;
        }


        // Method to execute a scalar query (returns single value)
        public object ExecuteScalarQuery(string query)
        {
            object result = null;

            try
            {
                // Create and open the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and get the first column of the first row
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }



        // Method to execute INSERT, UPDATE, or DELETE queries
        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            int rowsAffected = 0;

            try
            {
                // Create and open the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameters to the command
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        // Execute the query and get number of rows affected
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return rowsAffected;
        }
        

        // Method to call stored procedures
        public void CallStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Handle messages
                    connection.InfoMessage += (sender, e) =>
                    {
                        MessageBox.Show($"Message from SQL server: {e.Message}", "SQL Server Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; // Specify CommandType
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        command.ExecuteNonQuery(); // Use ExecuteNonQuery as the SP does not return a result set
                    }
                }
            }
            catch (SqlException ex)
            {
                //Check SQL state code for more granular error handling
                string errorMessage = $"Error calling stored procedure: {ex.Message}";

                if (ex.Number == 50001) // Property not found
                {
                    errorMessage = "Property with the specified ID does not exist.";
                }
                else if (ex.Number == 50002) // Incorrect Owner ID
                {
                    errorMessage = "Incorrect Owner ID for the specified property.";
                }
                MessageBox.Show(errorMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calling stored procedure: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }



    }
}
