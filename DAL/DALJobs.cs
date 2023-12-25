using System.Data;
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
                cmd.Parameters.AddWithValue("@title", jobs.JobTitle);
                cmd.Parameters.AddWithValue("@description", jobs.Description);
                cmd.Parameters.AddWithValue("@thumbnail", jobs.Thumbnail); // Ensure this matches the expected type and size in the database
                cmd.Parameters.AddWithValue("@isactive", jobs.IsActive);
                cmd.Parameters.AddWithValue("@currentTime", jobs.TimeUploaded);
                cmd.Parameters.AddWithValue("@salary", jobs.Salary); // Add Salary parameter
                cmd.Parameters.AddWithValue("@type", jobs.Type); // Add Type parameter

                cmd.ExecuteNonQuery();
            }
        }





        public static List<EntJobs> ShowCompanyJob(string loggedInCompanyID)
        {
            List<EntJobs> jobList = new List<EntJobs>();

            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_ShowCompanyJob", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EntJobs ej = new EntJobs();

                ej.JobTitle = reader["JobTitle"].ToString();
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
                ej.Description = reader["Description"].ToString();
                ej.Thumbnail = reader["Thumbnail"].ToString();

                // Handle Salary and Type as decimal and string respectively
                if (reader["Salary"] != DBNull.Value)
                {
                    ej.Salary = Convert.ToDecimal(reader["Salary"]);
                }

                ej.Type = reader["Type"].ToString();
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
                                JobTitle = reader["JobTitle"].ToString(),
                                Description = reader["Description"].ToString(),
                                Thumbnail = reader["Thumbnail"].ToString(),
                                TimeUploaded = Convert.ToDateTime(reader["TimeUploaded"]),
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : (decimal?)null,
                                Type = reader["Type"].ToString()
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
                // Assuming your table has a column named 'TimeUploaded' for the time when the job was uploaded
                string query = "SELECT TOP 10 JobId, Thumbnail, JobTitle, Description, Salary, Type, TimeUploaded FROM Job ORDER BY TimeUploaded DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EntJobs job = new EntJobs();
                    job.JobId = Convert.ToInt32(reader["JobId"]);
                    job.Thumbnail = reader["Thumbnail"].ToString();
                    job.JobTitle = reader["JobTitle"].ToString();
                    job.Description = reader["Description"].ToString();
                    job.TimeUploaded = Convert.ToDateTime(reader["TimeUploaded"]); // Retrieve "Uploaded Time"
                    job.Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : (decimal?)null;
                    job.Type = reader["Type"].ToString();

                    latestJobs.Add(job);
                }
            }

            return latestJobs;
        }


    }
}