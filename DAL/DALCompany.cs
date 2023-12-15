using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DAL
{
    public class DALCompany
    {
        public static void SaveCompany(EntCompany company)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_SaveCompanyData", con);
            cmd.Parameters.AddWithValue("@FirstName", company.FirstName);
            cmd.Parameters.AddWithValue("@LastName", company.LastName);
            cmd.Parameters.AddWithValue("@Email", company.Email);
            cmd.Parameters.AddWithValue("@Roles", company.Roles);
            cmd.Parameters.AddWithValue("@Password", company.Password);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public static List<EntCompany> GetCompanies()
        {
            List<EntCompany> companies = new List<EntCompany>();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_showCompanyData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EntCompany company = new EntCompany();
                        company.CompanyID = Convert.ToInt32(reader["CompanyID"]);
                        company.FirstName = reader["FirstName"].ToString();
                        company.LastName = reader["LastName"].ToString();
                        company.Roles = reader["Roles"].ToString();
                        company.Email = reader["Email"].ToString();
                        company.Password = reader["Password"].ToString();
                        //company.MobileNumber = reader["MobileNumber"].ToString();
                        //company.Reg_Date = reader["Reg_Date"].ToString();
                        //company.Last_Login = reader["Last_Login"].ToString();
                        //company.DisplayPicture = reader["DisplayPicture"].ToString();
                        companies.Add(company);
                    }
                }
            }

            return companies;
        }




        public static void DeleteCompany(EntCompany company)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DeleteCompany", con);
            cmd.Parameters.AddWithValue("@CompanyId", company.CompanyID);
            cmd.CommandType = CommandType.StoredProcedure;
            _ = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static EntCompany ShowCompany(string email, string password)
        {
            EntCompany updateCompany = new Entities.EntCompany();
            using SqlConnection con = DBHelper.GetConnection();
            con.Open();
            using SqlCommand cmd = new SqlCommand("SP_showCompanyData", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // No need to add parameters for SP_showCompanyData

            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                updateCompany.FirstName = dr["FirstName"].ToString();
                updateCompany.LastName = dr["LastName"].ToString();
                updateCompany.Roles = dr["Roles"].ToString();
                updateCompany.Email = dr["Email"].ToString();
            }

            return updateCompany;
        }


        public static EntCompany ProfileCompany()
        {
            EntCompany ProfileCompany = new Entities.EntCompany();
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_CompanyProfile", con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ProfileCompany.FirstName = dr["FirstName"].ToString();
                ProfileCompany.LastName = dr["LastName"].ToString();
                ProfileCompany.Roles = dr["Jobs"].ToString();
                ProfileCompany.Email = dr["Email"].ToString();
                ProfileCompany.MobileNumber = dr["MobileNumber"].ToString();
                ProfileCompany.Reg_Date = dr["Reg_Date"].ToString();
                ProfileCompany.Last_Login = dr["Last_Login"].ToString();
                ProfileCompany.DisplayPicture = dr["DisplayPicture"].ToString();

            }
            return ProfileCompany;
        }
        public static List<EntCompany> GetCompanyUsingLinq()
        {
            List<EntCompany> companies = GetCompanies();

            var filteredCompanies = companies.Where(b => b.Roles == "Company").ToList();

            return filteredCompanies;
        }
        public static List<EntCompany> GetUserUsingLinq()
        {
            List<EntCompany> companies = GetCompanies();

            var filteredUsers = companies.Where(u => u.Roles == "User").ToList();

            return filteredUsers;
        }
        public static EntCompany CompanyLogin(string email, string password)
        {
            EntCompany company = new EntCompany();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_LoginCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        company.FirstName = reader["FirstName"].ToString();
                        company.LastName = reader["LastName"].ToString();
                        company.Roles = reader["Roles"].ToString();
                        company.Email = reader["Email"].ToString();
                        // Add more properties as needed

                        reader.Close();
                    }
                }
            }

            return company;
        }

    }
}