using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FirstWeb.Models
{
    public class LoginManager
    {
        public static bool CheckLogin(string username, string password)
        {
            var strConn = WebConfigurationManager.ConnectionStrings["DeafultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                string sql = "SELECT NULL FROM Login WHERE Username=@user AND Password=@pwd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                object res = cmd.ExecuteScalar();

                return res != null;
            }
        }
    }
}