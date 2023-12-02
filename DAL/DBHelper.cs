using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=MURTAZA\\SQLEXPRESS;Initial Catalog=murtaza;Integrated Security=True");
            return conn;
        }
    }
}
