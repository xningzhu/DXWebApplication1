using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Microsoft.AspNet.Identity;

namespace DXShopping {
    public partial class RootMaster : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) SetLogin();
            ASPxLabel2.Text = DateTime.Now.Year + Server.HtmlDecode(" &copy; Copyright by [company name]");
        }
        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e) {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private void SetLogin()
        {
            string str_user_no = (Session["UserNo"] == null) ? "" : Session["UserNo"].ToString();
            if (!string.IsNullOrEmpty(str_user_no))
                mv_login.SetActiveView(vi_logined);
            else
                mv_login.SetActiveView(vi_guest);


        }
    }
}