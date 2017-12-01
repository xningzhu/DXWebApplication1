using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.Configuration;
using System.Threading;
using System.Globalization;

namespace ezapp
{
    /// <summary>
    /// 網頁底層自定公用功能類別
    /// </summary>
    public class ezPage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //取得目前執行網頁的名稱
            string str_page_name = System.IO.Path.GetFileName(Request.PhysicalPath);
            //判斷目前執行網頁的名稱是否為登入頁
            if (str_page_name.ToLower() == ezUtility.GetUrlPageName(ezSession.LoginUrl).ToLower())
            {
                //為登入頁則重設 Session 變數
                ezSession.SessionInit();
            }
            else
            {
                //如果未登入狀態
                if (string.IsNullOrEmpty(ezSession.UserNo))
                {
                    //判斷是否為除錯模式
                    if (ezSession.DebugMode)
                    {
                        //Session 變數初始化
                        ezSession.SessionInit();
                        //設定角色
                        ezSession.LoginRole = ezEnum.LoginRole.User;
                        //開發模式設定用者為 Guest
                        ezSession.UserNo = "Guest";
                        ezSession.UserName = "訪客";
                    }
                    else
                    {
                        //不為除錯模式則跳回登入頁
                        ezUtility.RedirectToLoginUrl();
                    }
                }
            }
        }

        protected override void InitializeCulture()
        {
            if (Session["Language"] == null) Session["Language"] = ezEnum.Language.zh_TW;
            string str_culture = Session["Language"].ToString();
            str_culture = str_culture.Replace('_', '-');
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(str_culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(str_culture);
            base.InitializeCulture();
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