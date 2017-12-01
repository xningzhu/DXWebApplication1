using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;

namespace ezapp
{
    public class ezBasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string str_user_no = ezSession.UserNo;
            if (string.IsNullOrEmpty(str_user_no))
            {
                if (!ezSession.DebugMode)
                {
                    Response.Redirect(ezSession.LoginUrl);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 顯示網頁訊息
        /// </summary>
        /// <param name="MessageText">訊息文字</param>
        protected void ShowMsgBox(string MessageText)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "訊息", String.Format("<script>alert('{0}');</script>", MessageText));
            //顯示網頁訊息 適用ASP.NET AJAX
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "訊息", String.Format("alert('{0}')", MessageText), true);
        }
    }
}