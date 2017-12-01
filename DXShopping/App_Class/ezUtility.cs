using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Configuration;

namespace ezapp
{
    /// <summary>
    /// 自定公用功能類別
    /// </summary>
    public static class ezUtility
    {
        /// <summary>
        /// 取得指定網址中的檔名部份
        /// </summary>
        /// <param name="s_Url">指定網址</param>
        /// <returns></returns>
        public static string GetUrlPageName(string sUrl)
        {
            string str_url = sUrl;
            string str_page = sUrl;
            int int_index = str_url.LastIndexOf('/');
            if (int_index >= 0) str_page = str_page.Substring((int_index + 1), (str_url.Length - 1) - int_index);
            return str_page;
        }
        /// <summary>
        /// 以程式代號查程式網址
        /// </summary>
        /// <param name="sPrgNo">程式代號</param>
        /// <returns></returns>
        public static string GetPrgUrlByPNo(string sPrgNo)
        {
            string str_url = "";
            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT url_prg FROM z_sys_prg WHERE no_prg = @no_prg";
            ezsql.ParameterAdd("@no_prg", sPrgNo, true);
            str_url = ezsql.GetSelectString("url_prg");
            if (string.IsNullOrEmpty(str_url)) str_url = "";
            if (!string.IsNullOrEmpty(str_url))
            {
                {
                    str_url = ezSession.FormRoot + "/" + sPrgNo.Substring(0, 3).ToLower() + "/" + str_url + ".aspx";
                    if (!File.Exists(HttpContext.Current.Server.MapPath(str_url))) str_url = "";
                }
            }
            ezsql.Close();
            return str_url;
        }
        /// <summary>
        /// 以程式代號查程式名稱
        /// </summary>
        /// <param name="sPrgNo">程式代號</param>
        /// <returns></returns>
        public static string GetPrgNameByPNo(string sPrgNo)
        {
            string str_name = "";
            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT name_prg FROM z_sys_prg WHERE no_prg = @no_prg";
            ezsql.ParameterAdd("@no_prg", sPrgNo, true);
            str_name = ezsql.GetSelectString("name_prg");
            if (string.IsNullOrEmpty(str_name)) str_name = "";
            ezsql.Close();
            return str_name;
        }
        /// <summary>
        /// 返回後台管理頁
        /// </summary>
        public static void RedirectToAdminUrl()
        {
            HttpContext.Current.Response.Redirect(ezSession.AdminUrl);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 返回登入頁
        /// </summary>
        public static void RedirectToLoginUrl()
        {
            HttpContext.Current.Response.Redirect(ezSession.LoginUrl);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 返回首頁
        /// </summary>
        public static void RedirectToHomeUrl()
        {
            HttpContext.Current.Response.Redirect(ezSession.HomeUrl);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 取得登入者的資訊(登入帳號及名稱)
        /// </summary>
        /// <returns></returns>
        public static string GetLoginUserInfo()
        {
            return ezSession.UserNo + " (" + ezSession.UserName + ")";
        }
        /// <summary>
        /// 取得 object 型態變數轉成字串 , Null 時為空白
        /// </summary>
        /// <param name="oValue">object 型態變數</param>
        /// <returns></returns>
        public static string GetStringValue(object oValue)
        {
            string str_value = "";
            if (oValue != null) str_value = oValue.ToString();
            return str_value;
        }
        /// <summary>
        /// 取得 object 型態變數轉成整數 , Null 時為0
        /// </summary>
        /// <param name="oValue">object 型態變數</param>
        /// <returns></returns>
        public static int GetIntValue(object oValue)
        {
            string str_value = "0";
            if (oValue != null)
            {
                str_value = oValue.ToString();
                if (string.IsNullOrEmpty(str_value)) str_value = "0";
            }
            int int_value = int.Parse(str_value);
            return int_value;
        }

        public static string GetIconFile(string sCode)
        {
            string str_ImageUrl = @"~/Images/Icons/Null24.png";
            if (sCode.ToUpper() == "P") str_ImageUrl = @"~/Images/Icons/Program24.png";
            if (sCode.ToUpper() == "G") str_ImageUrl = @"~/Images/Icons/Graph24.png";
            if (sCode.ToUpper() == "Q") str_ImageUrl = @"~/Images/Icons/Query24.png";
            if (sCode.ToUpper() == "R") str_ImageUrl = @"~/Images/Icons/Report24.png";
            if (sCode.ToUpper() == "S") str_ImageUrl = @"~/Images/Icons/Settings24.png";
            return str_ImageUrl;
        }
    }
}