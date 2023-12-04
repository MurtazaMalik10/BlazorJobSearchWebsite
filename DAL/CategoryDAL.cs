using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Entities;

namespace DAL
{
    public class CategoryDAL
    {
        public static void SaveCategory(EntCategory ec)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_AddCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CatName", ec.Category);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<EntCategory> GetCategory()
        {
            List<EntCategory> categoryList = new List<EntCategory>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_ShowCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EntCategory eb = new EntCategory();
                    eb.Id = Convert.ToInt32(reader["CatId"]);
                    eb.Category = reader["CatName"].ToString();
                    categoryList.Add(eb);
                }
            }

            return categoryList;
        }

        public static void UpdateCategory(EntCategory ec)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CatId", ec.Id);
                    cmd.Parameters.AddWithValue("@CatName", ec.Category);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw ex;
            }
        }

        public static void DeleteCategory(int CategoryId)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CatId", CategoryId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw ex;
            }
        }
    }
}
