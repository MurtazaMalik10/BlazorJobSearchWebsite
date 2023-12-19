using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class JobApplicationDAL
    {
        // Method to submit a job application to the database
        public void SubmitJobApplication(EntJobApplication jobApplication)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_SubmitJobApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobId", jobApplication.JobId); // Include the JobId parameter
                cmd.Parameters.AddWithValue("@FullName", jobApplication.FullName);
                cmd.Parameters.AddWithValue("@Email", jobApplication.Email);
                cmd.Parameters.AddWithValue("@ContactNumber", jobApplication.ContactNumber);
                cmd.Parameters.AddWithValue("@CVFile", jobApplication.CVFile); // Save image data

                cmd.ExecuteNonQuery();
            }
        }

        // Method to retrieve a job application based on JobId from the database
        public EntJobApplication GetJobApplication(int jobId)
        {
            EntJobApplication jobApplication = new EntJobApplication();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GetJobApplicationDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JobId", jobId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        jobApplication.JobId = jobId;
                        jobApplication.FullName = reader["FullName"].ToString();
                        jobApplication.Email = reader["Email"].ToString();
                        jobApplication.ContactNumber = reader["ContactNumber"].ToString();
                        jobApplication.CVFile = reader["CVFile"].ToString(); // Retrieve image data
                    }
                    // Handle no rows found scenario if needed
                }
            }

            return jobApplication;
        }

// Method to display job application details
public static List<EntJobApplication> ShowJobApplications()
        {
            List<EntJobApplication> jobApplications = new List<EntJobApplication>();

            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ShowJobApplications", con); // Assuming the name of your stored procedure is 'SP_ShowJobApplications'
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EntJobApplication jobApp = new EntJobApplication();

                jobApp.JobId = Convert.ToInt32(reader["JobId"]);
                jobApp.FullName = reader["FullName"].ToString();
                jobApp.Email = reader["Email"].ToString();
                jobApp.ContactNumber = reader["ContactNumber"].ToString();
                cmd.Parameters.AddWithValue("@CVFile", jobApp.CVFile);
                // Set other properties accordingly

                jobApplications.Add(jobApp);
            }

            con.Close();
            return jobApplications;
        }
    }
}
