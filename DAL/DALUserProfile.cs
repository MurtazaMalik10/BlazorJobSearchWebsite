using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class DALUserProfile
    {
        public static void SaveUserProfile(EntUserProfile userProfile)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_SaveUserProfileData", con);
                cmd.Parameters.AddWithValue("@UserID", userProfile.UserID);
                cmd.Parameters.AddWithValue("@UserName", userProfile.UserName);
                cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                cmd.Parameters.AddWithValue("@TimeCreated", userProfile.TimeCreated);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }

        public static List<EntUserProfile> GetAllUserProfiles()
        {
            List<EntUserProfile> userProfiles = new List<EntUserProfile>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_ShowUserProfileData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntUserProfile userProfile = new EntUserProfile
                        {
                            UserID = reader["UserID"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            TimeCreated = Convert.ToDateTime(reader["TimeCreated"])
                        };
                        userProfiles.Add(userProfile);
                    }
                }
            }

            return userProfiles;
        }

        public static void DeleteUserProfile(string userID)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_DeleteUserProfile", con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }

        // Other methods as needed...
    }
}
