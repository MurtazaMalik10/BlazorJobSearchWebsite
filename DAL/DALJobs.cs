﻿using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class DALJobs
    {
        public static void SaveJob(EntJobs jobs)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_SaveJob", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Make sure the parameter names and types match the stored procedure parameters
                cmd.Parameters.AddWithValue("@companyId", jobs.CompanyID);
                cmd.Parameters.AddWithValue("@title", jobs.JobTitle);
                cmd.Parameters.AddWithValue("@catId", jobs.CatId);
                cmd.Parameters.AddWithValue("@description", jobs.Description);
                cmd.Parameters.AddWithValue("@thumbnail", jobs.Thumbnail); // Ensure this matches the expected type and size in the database
                cmd.Parameters.AddWithValue("@isactive", jobs.IsActive);
                cmd.Parameters.AddWithValue("@currentTime", jobs.TimeUploaded);

                cmd.ExecuteNonQuery();
            }
        }




        public static List<EntJobs> ShowCompanyJob(string loggedInCompanyID)
        {
            List<EntJobs> jobList = new List<EntJobs>();

            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ShowCompanyJob", con);
            cmd.Parameters.Add("@companyId", SqlDbType.NVarChar).Value = loggedInCompanyID; // Use the correct parameter name
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EntJobs ej = new EntJobs();

                ej.JobTitle = reader["JobTitle"].ToString();
                ej.CompanyID = reader["companyID"].ToString();
                ej.CatName = reader["CategoryName"].ToString();
                jobList.Add(ej);
            }

            con.Close();
            return jobList;
        }


        public static List<EntJobs> ShowJob()
        {
            List<EntJobs> jobList = new List<EntJobs>();

            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_showjobs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EntJobs ej = new EntJobs();

                ej.JobId = Convert.ToInt32(reader["JobId"]);
                ej.JobTitle = reader["JobTitle"].ToString();
                ej.CatId = (int?)Convert.ToDecimal(reader["CatId"]);
                ej.Description = reader["Description"].ToString();
                ej.Thumbnail = reader["Thumbnail"].ToString();

                jobList.Add(ej);
            }

            con.Close();
            return jobList;
        }

        public static EntJobs GetJobById(int jobId)
        {
            EntJobs job = null;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_ViewJob", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JobId", jobId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            job = new EntJobs
                            {
                                JobId = Convert.ToInt32(reader["JobId"]),
                                CompanyID = reader["CompanyID"].ToString(),
                                JobTitle = reader["JobTitle"].ToString(),
                                Description = reader["Description"].ToString(),
                                Thumbnail = reader["Thumbnail"].ToString(),
                            };
                        }
                    }
                }
            }

            return job;
        }

        public static void DeleteJob(int jobId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_DeleteJob", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter for JobID
                cmd.Parameters.AddWithValue("@JobID", jobId);

                // Execute the delete command
                cmd.ExecuteNonQuery();
            }
        }

        public static List<EntJobs> GetLatestJobsWithThumbnails()
        {
            List<EntJobs> latestJobs = new List<EntJobs>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                // Assuming you have a DateTime column named 'DateCreated' indicating when the job was created
                string query = "SELECT TOP 10 * FROM Job ORDER BY TimeUploaded DESC"; // Change the query according to your table structure

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EntJobs job = new EntJobs();
                    job.JobId = Convert.ToInt32(reader["JobId"]);
                    job.Thumbnail = reader["Thumbnail"].ToString();
                    job.JobTitle = reader["JobTitle"].ToString();
                    job.Description = reader["Description"].ToString();

                    // Populate other job details as needed

                    latestJobs.Add(job);
                }
            }

            return latestJobs;
        }
    }
}