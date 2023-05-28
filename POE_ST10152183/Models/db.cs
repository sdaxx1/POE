using Microsoft.Data.SqlClient;
using POE_ST10152183.Models;
using System.Data;

namespace POE_ST10152183.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=lab000000\\SQLEXPRESS;Initial Catalog=st10152183_POE_Database;Integrated Security=True;Encrypt=False");
    public int logincheck(AdminAccounts aa)
        {
            SqlCommand com = new SqlCommand("ad_login",con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@AdminUserName", aa.AdminUserName);
            com.Parameters.AddWithValue("@AdminPassword", aa.AdminPassword);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName= "@isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res=Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;
        }
    }
}
