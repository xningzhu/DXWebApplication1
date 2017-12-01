using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DXShopping
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) CheckUser();
        }

        private void CheckUser()
        {
            Session["UserNo"] = "";
            bool bln_hasrows = false;
            string str_sql = "SELECT mname FROM users WHERE mno = @mno AND mpass = @mpass";
            string str_string = WebConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

            string str_user_no = inputUser.Value;
            string str_password = inputPassword.Value;
            SqlConnection conn = new SqlConnection(str_string);
            SqlCommand cmd = new SqlCommand(str_sql, conn);
            conn.Open();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@mno", str_user_no);
            cmd.Parameters.AddWithValue("@mpass", str_password);
            SqlDataReader dr = cmd.ExecuteReader();
            bln_hasrows = dr.HasRows;
            if (bln_hasrows)
            {
                dr.Read();
                Session["UserNo"] = str_user_no;
                Session["UserName"] = dr["mname"].ToString();
            }
            dr.Close();
            conn.Close();

            if (bln_hasrows)
                Response.Redirect("default.aspx", true);
            else
                Response.Write("輸入錯誤!!");
        }
    }
}