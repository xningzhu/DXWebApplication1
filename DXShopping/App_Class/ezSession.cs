using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// Session 自定公用功能類別
    /// </summary>
    public static class ezSession
    {
        /// <summary>
        /// 初始化 Session 值
        /// </summary>
        public static void SessionInit()
        {
            //讀取 AppSettings 值
            object objAppName = WebConfigurationManager.AppSettings["AppName"];
            object objDebugMode = WebConfigurationManager.AppSettings["DebugMode"];
            object objAdminUrl = WebConfigurationManager.AppSettings["AdminUrl"];
            object objLoginUrl = WebConfigurationManager.AppSettings["LoginUrl"];
            object objHomeUrl = WebConfigurationManager.AppSettings["HomeUrl"];
            object objConnName = WebConfigurationManager.AppSettings["ConnName"];
            object objLanguage = WebConfigurationManager.AppSettings["Language"];
            object objPrgIcon = WebConfigurationManager.AppSettings["PrgIcon"];
            object objFontSize = WebConfigurationManager.AppSettings["FontSize"];
            object objMultiLanguage = WebConfigurationManager.AppSettings["MultiLanguage"];
            object objMultiCompany = WebConfigurationManager.AppSettings["MultiCompany"];
            object objWebsiteAddress = WebConfigurationManager.AppSettings["WebsiteAddress"];
            object objAdminSendEmail = WebConfigurationManager.AppSettings["AdminSendEmail"];
            object objAdminRecEmail = WebConfigurationManager.AppSettings["AdminRecEmail"];
            object objServiceEmail = WebConfigurationManager.AppSettings["ServiceEmail"];

            //設定預設值
            string strAppName = (objAppName == null || objAppName.ToString() == "") ? "資訊管理系統" : objAppName.ToString();
            string strDebugMode = (objDebugMode == null || objDebugMode.ToString() == "") ? "1" : objDebugMode.ToString();
            string strAdminUrl = (objAdminUrl == null || objAdminUrl.ToString() == "") ? "~/admin/manager.aspx" : objAdminUrl.ToString();
            string strLoginUrl = (objLoginUrl == null || objLoginUrl.ToString() == "") ? "~/login.aspx" : objLoginUrl.ToString();
            string strHomeUrl = (objHomeUrl == null || objHomeUrl.ToString() == "") ? "~/home.aspx" : objHomeUrl.ToString();
            string strConnName = (objConnName == null || objConnName.ToString() == "") ? "dbconn" : objConnName.ToString();
            string strLanguage = (objLanguage == null || objLanguage.ToString() == "") ? "zh-TW" : objLanguage.ToString();
            string strPrgIcon = (objPrgIcon == null || objPrgIcon.ToString() == "") ? "1" : objPrgIcon.ToString();
            string strFontSize = (objFontSize == null || objFontSize.ToString() == "") ? "M" : objFontSize.ToString();
            string strMultiLanguage = (objMultiLanguage == null || objMultiLanguage.ToString() == "") ? "0" : objMultiLanguage.ToString();
            string strMultiCompany = (objMultiCompany == null || objMultiCompany.ToString() == "") ? "0" : objMultiCompany.ToString();
            string strWebsiteAddress = (objWebsiteAddress == null || objWebsiteAddress.ToString() == "") ? "http://www.ezmall.com.tw" : objWebsiteAddress.ToString();
            string strAdminSendEmail = (objAdminSendEmail == null || objAdminSendEmail.ToString() == "") ? "" : objAdminSendEmail.ToString();
            string strAdminRecEmail = (objAdminRecEmail == null || objAdminRecEmail.ToString() == "") ? "" : objAdminRecEmail.ToString();
            string strServiceEmail = (objServiceEmail == null || objServiceEmail.ToString() == "") ? "" : objServiceEmail.ToString();
            FontUnit fuSize = (strFontSize == "XS") ? FontUnit.XSmall : (strFontSize == "S") ? FontUnit.Small : (strFontSize == "L") ? FontUnit.Large : FontUnit.Medium;

            bool blnDebugMode = (strDebugMode == "1") ? true : false;
            bool blnPrgIcon = (strPrgIcon == "1") ? true : false;
            bool blnMultiLanguage = (strMultiLanguage == "1") ? true : false;
            bool blnMultiCompany = (strMultiCompany == "1") ? true : false;

            ezEnum.Language enuLanguage = ezEnum.Language.zh_TW;
            if (strLanguage == "zh-TW") enuLanguage = ezEnum.Language.zh_TW;
            if (strLanguage == "zh-CN") enuLanguage = ezEnum.Language.zh_CN;
            if (strLanguage == "en-US") enuLanguage = ezEnum.Language.en_US;

            //設定 Session 變數
            AppName = strAppName;
            DebugMode = blnDebugMode;
            AdminUrl = strAdminUrl;
            LoginUrl = strLoginUrl;
            HomeUrl = strHomeUrl;
            ConnName = strConnName;
            Language = enuLanguage;
            PrgIcon = blnPrgIcon;
            FontSize = fuSize;
            MultiLanguage = blnMultiLanguage;
            MultiCompany = blnMultiCompany;
            WebsiteAddress = strWebsiteAddress;
            AdminSendEmail = strAdminSendEmail;
            AdminRecEmail = strAdminRecEmail;
            ServiceEmail = strServiceEmail;

            PrgID = "0";
            PrgNo = "";
            PrgName = "";
            FormRoot = "~/Forms";
            UserID = "0";
            UserNo = "";
            UserName = "";
            RoleNo = "";
            CompNo = "";
            CompName = "";
            ConnectionString = "";
            ProviderName = "";
            SearchText = "";

            IsAdd = ezEnum.YesNo.Yes;
            IsEdit = ezEnum.YesNo.Yes;
            IsDelete = ezEnum.YesNo.Yes;
            IsConfirm = ezEnum.YesNo.Yes;
            IsAbolish = ezEnum.YesNo.Yes;
            IsPrice = ezEnum.YesNo.Yes;
            IsPrint = ezEnum.YesNo.Yes;
            IsDownload = ezEnum.YesNo.Yes;
            IsExport = ezEnum.YesNo.Yes;

            string str_parm = "";
            for (int i = 0; i < 10; i++)
            {
                str_parm = "iparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = 0;
                str_parm = "viparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = 0;
                str_parm = "riparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = 0;
                str_parm = "sparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = "";
                str_parm = "vsparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = "";
                str_parm = "rsparm" + (i + 1).ToString();
                HttpContext.Current.Session[str_parm] = "";
            }
        }

        /// <summary>
        /// 取得 Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static int GetIntParm(int iIndex)
        {
            string str_parm = "iparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            int int_value = (obj_value == null) ? 0 : int.Parse(obj_value.ToString());
            return int_value;
        }

        /// <summary>
        /// 設定 Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="iValue">Session 值</param>
        /// <returns></returns>
        public static void SetIntParm(int iIndex, int iValue)
        {
            string str_parm = "iparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = iValue;
        }

        /// <summary>
        /// 取得 GridLookUp Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static int GeGlutIntParm(int iIndex)
        {
            string str_parm = "viparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            int int_value = (obj_value == null) ? 0 : int.Parse(obj_value.ToString());
            return int_value;
        }

        /// <summary>
        /// 設定 GridLookUp Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="iValue">Session 值</param>
        /// <returns></returns>
        public static void SetGluIntParm(int iIndex, int iValue)
        {
            string str_parm = "viparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = iValue;
        }

        /// <summary>
        /// 取得 Report Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static int GeReporttIntParm(int iIndex)
        {
            string str_parm = "riparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            int int_value = (obj_value == null) ? 0 : int.Parse(obj_value.ToString());
            return int_value;
        }

        /// <summary>
        /// 設定 Report Session iparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="iValue">Session 值</param>
        /// <returns></returns>
        public static void SeReportIntParm(int iIndex, int iValue)
        {
            string str_parm = "riparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = iValue;
        }

        /// <summary>
        /// 取得 Session sparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static string GetStringParm(int iIndex)
        {
            string str_parm = "sparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            string str_value = (obj_value == null) ? string.Empty : obj_value.ToString();
            return str_value;
        }

        /// <summary>
        /// 設定 Session sparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="sValue">Session 值</param>
        /// <returns></returns>
        public static void SetStringParm(int iIndex, string sValue)
        {
            string str_parm = "sparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = sValue;
        }

        /// <summary>
        /// 取得 GridLookUp Session vsparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static string GeGlutStringParm(int iIndex)
        {
            string str_parm = "vsparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            string str_value = (obj_value == null) ? string.Empty : obj_value.ToString();
            return str_value;
        }

        /// <summary>
        /// 設定 GridLookUp Session vsparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="sValue">Session 值</param>
        /// <returns></returns>
        public static void SetGluStringParm(int iIndex, string sValue)
        {
            string str_parm = "vsparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = sValue;
        }

        /// <summary>
        /// 取得 Report Session rsparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <returns></returns>
        public static string GeReporttStringParm(int iIndex)
        {
            string str_parm = "rsparm" + (iIndex).ToString();
            object obj_value = HttpContext.Current.Session[str_parm];
            string str_value = (obj_value == null) ? string.Empty : obj_value.ToString();
            return str_value;
        }

        /// <summary>
        /// 設定 Report Session rsparm 值
        /// </summary>
        /// <param name="iIndex">索引</param>
        /// <param name="sValue">Session 值</param>
        /// <returns></returns>
        public static void SeReportStringParm(int iIndex, string sValue)
        {
            string str_parm = "rsparm" + (iIndex).ToString();
            HttpContext.Current.Session[str_parm] = sValue;
        }

        /// <summary>
        /// 取得或設定 AppName 系統名稱
        /// </summary>
        public static string AppName
        {
            get { return (HttpContext.Current.Session["AppName"] == null) ? "資訊管理系統" : HttpContext.Current.Session["AppName"].ToString(); }
            set { HttpContext.Current.Session["AppName"] = value; }
        }
        /// <summary>
        /// 取得或設定 AdminUrl 管理者網頁名稱
        /// </summary>
        public static string AdminUrl
        {
            get { return (HttpContext.Current.Session["AdminUrl"] == null) ? "~/admin/manager.aspx" : HttpContext.Current.Session["AdminUrl"].ToString(); }
            set { HttpContext.Current.Session["AdminUrl"] = value; }
        }
        /// <summary>
        /// 取得或設定 LoginUrl 登入網頁名稱
        /// </summary>
        public static string LoginUrl
        {
            get { return (HttpContext.Current.Session["LoginUrl"] == null) ? "~/login.aspx" : HttpContext.Current.Session["LoginUrl"].ToString(); }
            set { HttpContext.Current.Session["LoginUrl"] = value; }
        }
        /// <summary>
        /// 取得或設定 HomeUrl 首頁名稱
        /// </summary>
        public static string HomeUrl
        {
            get { return (HttpContext.Current.Session["HomeUrl"] == null) ? "~/home.aspx" : HttpContext.Current.Session["HomeUrl"].ToString(); }
            set { HttpContext.Current.Session["HomeUrl"] = value; }
        }
        /// <summary>
        /// 取得或設定  SearchText 搜尋條件
        /// </summary>
        public static string SearchText
        {
            get { return (HttpContext.Current.Session["SearchText"] == null) ? "" : HttpContext.Current.Session["SearchText"].ToString(); }
            set { HttpContext.Current.Session["SearchText"] = value; }
        }
        /// <summary>
        /// 取得或設定預設控制項字型大小
        /// </summary>
        public static FontUnit FontSize
        {
            get
            {
                if (HttpContext.Current.Session["FontSize"] == null)
                {
                    object objFontSize = WebConfigurationManager.AppSettings["FontSize"];
                    HttpContext.Current.Session["FontSize"] = (objFontSize == null || objFontSize.ToString() == "") ? "M" : objFontSize.ToString();
                }
                string str_size = (HttpContext.Current.Session["FontSize"] == null) ? "M" : HttpContext.Current.Session["FontSize"].ToString();
                FontUnit fuSize = FontUnit.Medium;
                if (str_size == "XS") fuSize = FontUnit.XSmall;
                if (str_size == "S") fuSize = FontUnit.Small;
                if (str_size == "M") fuSize = FontUnit.Medium;
                if (str_size == "L") fuSize = FontUnit.Large;
                return fuSize;
            }
            set
            {
                string str_size = "M";
                if (value == FontUnit.XSmall) str_size = "XS";
                if (value == FontUnit.Small) str_size = "S";
                if (value == FontUnit.Large) str_size = "L";
                HttpContext.Current.Session["FontSize"] = str_size;
            }
        }
        /// <summary>
        /// 取得或設定  MultiLanguage 是否為多國語系功能
        /// </summary>
        public static bool MultiLanguage
        {
            get { return (HttpContext.Current.Session["MultiLanguage"] == null) ? false : (HttpContext.Current.Session["MultiLanguage"].ToString() == "1" ? true : false); }
            set { HttpContext.Current.Session["MultiLanguage"] = (value) ? "1" : "0"; }
        }
        /// <summary>
        /// 取得或設定  MultiCompany 是否為多公司別
        /// </summary>
        public static bool MultiCompany
        {
            get { return (HttpContext.Current.Session["MultiCompany"] == null) ? false : (HttpContext.Current.Session["MultiCompany"].ToString() == "1" ? true : false); }
            set { HttpContext.Current.Session["MultiCompany"] = (value) ? "1" : "0"; }
        }
        /// <summary>
        /// 取得或設定網站的位址
        /// </summary>
        public static string WebsiteAddress
        {
            get { return (HttpContext.Current.Session["WebsiteAddress"] == null) ? "~/Forms" : HttpContext.Current.Session["WebsiteAddress"].ToString(); }
            set { HttpContext.Current.Session["WebsiteAddress"] = value; }
        }
        /// <summary>
        /// 取得或設定管理員寄出信箱
        /// </summary>
        public static string AdminSendEmail
        {
            get { return (HttpContext.Current.Session["AdminSendEmail"] == null) ? "" : HttpContext.Current.Session["AdminSendEmail"].ToString(); }
            set { HttpContext.Current.Session["AdminSendEmail"] = value; }
        }
        /// <summary>
        /// 取得或設定管理員接收信箱
        /// </summary>
        public static string AdminRecEmail
        {
            get { return (HttpContext.Current.Session["AdminRecEmail"] == null) ? "" : HttpContext.Current.Session["AdminRecEmail"].ToString(); }
            set { HttpContext.Current.Session["AdminRecEmail"] = value; }
        }
        /// <summary>
        /// 取得或設定會員問題服務信箱
        /// </summary>
        public static string ServiceEmail
        {
            get { return (HttpContext.Current.Session["ServiceEmail"] == null) ? "" : HttpContext.Current.Session["ServiceEmail"].ToString(); }
            set { HttpContext.Current.Session["ServiceEmail"] = value; }
        }
        /// <summary>
        /// 取得或設定 FormRoot 程式所在根目錄
        /// </summary>
        public static string FormRoot
        {
            get { return (HttpContext.Current.Session["FormRoot"] == null) ? "~/Forms" : HttpContext.Current.Session["FormRoot"].ToString(); }
            set { HttpContext.Current.Session["FormRoot"] = value; }
        }
        /// <summary>
        /// 取得或設定登入系統角色
        /// </summary>
        public static ezEnum.LoginRole LoginRole
        {
            get { return (HttpContext.Current.Session["LoginRole"] == null) ? ezEnum.LoginRole.User : (ezEnum.LoginRole)HttpContext.Current.Session["LoginRole"]; }
            set { HttpContext.Current.Session["LoginRole"] = value; }
        }
        /// <summary>
        /// 取得或設定 UserID 使用者 rowid
        /// </summary>
        public static string UserID
        {
            get { return (HttpContext.Current.Session["UserID"] == null) ? "0" : HttpContext.Current.Session["UserID"].ToString(); }
            set { HttpContext.Current.Session["UserID"] = value; }
        }
        /// <summary>
        /// 取得或設定 UserNo 使用者代號
        /// </summary>
        public static string UserNo
        {
            get { return (HttpContext.Current.Session["UserNo"] == null) ? "" : HttpContext.Current.Session["UserNo"].ToString(); }
            set { HttpContext.Current.Session["UserNo"] = value; }
        }
        /// <summary>
        /// 取得或設定 UserName 使用者名稱
        /// </summary>
        public static string UserName
        {
            get { return (HttpContext.Current.Session["UserName"] == null) ? "" : HttpContext.Current.Session["UserName"].ToString(); }
            set { HttpContext.Current.Session["UserName"] = value; }
        }
        /// <summary>
        /// 取得或設定 RoleNo 使用者角色
        /// </summary>
        public static string RoleNo
        {
            get { return (HttpContext.Current.Session["RoleNo"] == null) ? "" : HttpContext.Current.Session["RoleNo"].ToString(); }
            set { HttpContext.Current.Session["RoleNo"] = value; }
        }
        /// <summary>
        /// 取得或設定 CompCode 公司類別 M=公司 C=客戶 V=廠商
        /// </summary>
        public static ezEnum.CompCode CompCode
        {
            get
            {
                ezEnum.CompCode eCompCode = ezEnum.CompCode.None;
                string strComCode = (HttpContext.Current.Session["CompCode"] == null) ? "N" : HttpContext.Current.Session["CompCode"].ToString();
                if (strComCode == "M") eCompCode = ezEnum.CompCode.Company;
                if (strComCode == "C") eCompCode = ezEnum.CompCode.Customer;
                if (strComCode == "V") eCompCode = ezEnum.CompCode.Vendor;
                return eCompCode;
            }
            set
            {
                string str_code = "N";
                if (value == ezEnum.CompCode.Company) str_code = "M";
                if (value == ezEnum.CompCode.Customer) str_code = "C";
                if (value == ezEnum.CompCode.Vendor) str_code = "V";
                HttpContext.Current.Session["CompCode"] = str_code;
            }
        }
        /// <summary>
        /// 取得或設定 CompNo 公司代號
        /// </summary>
        public static string CompNo
        {
            get { return (HttpContext.Current.Session["CompNo"] == null) ? "" : HttpContext.Current.Session["CompNo"].ToString(); }
            set { HttpContext.Current.Session["CompNo"] = value; }
        }
        /// <summary>
        /// 取得或設定 CompName 公司名稱
        /// </summary>
        public static string CompName
        {
            get { return (HttpContext.Current.Session["CompName"] == null) ? "" : HttpContext.Current.Session["CompName"].ToString(); }
            set { HttpContext.Current.Session["CompName"] = value; }
        }
        /// <summary>
        /// 取得或設定 ConnectionString 連線字串名稱
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string str_string = (HttpContext.Current.Session["ConnectionString"] == null) ? "" : HttpContext.Current.Session["ConnectionString"].ToString();
                if (string.IsNullOrEmpty(str_string))
                {
                    str_string = WebConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
                    HttpContext.Current.Session["ConnectionString"] = str_string;
                }
                return str_string;
            }
            set { HttpContext.Current.Session["ConnectionString"] = value; }
        }
        /// <summary>
        /// 取得或設定 ProviderName 連線資料庫提供者名稱
        /// </summary>
        public static string ProviderName
        {
            get
            {
                string str_provider = (HttpContext.Current.Session["ProviderName"] == null) ? "" : HttpContext.Current.Session["ProviderName"].ToString();
                if (string.IsNullOrEmpty(str_provider))
                {
                    str_provider = WebConfigurationManager.ConnectionStrings[ConnName].ProviderName;
                    HttpContext.Current.Session["ProviderName"] = str_provider;
                }
                return str_provider;
            }
            set { HttpContext.Current.Session["ProviderName"] = value; }
        }
        /// <summary>
        /// 取得或設定 ConnName 資料庫連線名稱
        /// </summary>
        public static string ConnName
        {
            get { return (HttpContext.Current.Session["ConnName"] == null) ? "dbconn" : HttpContext.Current.Session["ConnName"].ToString(); }
            set
            {
                string str_conn_name = value;
                HttpContext.Current.Session["ConnName"] = str_conn_name;
                ConnectionString = WebConfigurationManager.ConnectionStrings[str_conn_name].ConnectionString;
                ProviderName = WebConfigurationManager.ConnectionStrings[str_conn_name].ProviderName;
            }
        }
        /// <summary>
        /// 取得或設定 PrgID 程式ID
        /// </summary>
        public static string PrgID
        {
            get { return (HttpContext.Current.Session["PrgID"] == null) ? "0" : HttpContext.Current.Session["PrgID"].ToString(); }
            set { HttpContext.Current.Session["PrgID"] = value; }
        }
        /// <summary>
        /// 取得或設定 PrgNo 程式代號
        /// </summary>
        public static string PrgNo
        {
            get { return (HttpContext.Current.Session["PrgNo"] == null) ? "" : HttpContext.Current.Session["PrgNo"].ToString(); }
            set { HttpContext.Current.Session["PrgNo"] = value; }
        }
        /// <summary>
        /// 取得或設定 PrgName 程式名稱
        /// </summary>
        public static string PrgName
        {
            get { return (HttpContext.Current.Session["PrgName"] == null) ? "" : HttpContext.Current.Session["PrgName"].ToString(); }
            set { HttpContext.Current.Session["PrgName"] = value; }
        }
        /// <summary>
        /// 取得或設定 Language 語系名稱
        /// </summary>
        public static ezEnum.Language Language
        {
            get { return (HttpContext.Current.Session["Language"] == null) ? ezEnum.Language.zh_TW : (ezEnum.Language)HttpContext.Current.Session["Language"]; }
            set { HttpContext.Current.Session["Language"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsAdd 是否有新增權限
        /// </summary>
        public static ezEnum.YesNo IsAdd
        {
            get { return (HttpContext.Current.Session["IsAdd"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsAdd"]; }
            set { HttpContext.Current.Session["IsAdd"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsAdd 是否有修改權限
        /// </summary>
        public static ezEnum.YesNo IsEdit
        {
            get { return (HttpContext.Current.Session["IsEdit"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsEdit"]; }
            set { HttpContext.Current.Session["IsAdd"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsAdd 是否有刪除權限
        /// </summary>
        public static ezEnum.YesNo IsDelete
        {
            get { return (HttpContext.Current.Session["IsDelete"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsDelete"]; }
            set { HttpContext.Current.Session["IsDelete"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsConfirm 是否有審核權限
        /// </summary>
        public static ezEnum.YesNo IsConfirm
        {
            get { return (HttpContext.Current.Session["IsConfirm"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsConfirm"]; }
            set { HttpContext.Current.Session["IsConfirm"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsAbolish 是否有反確認權限
        /// </summary>
        public static ezEnum.YesNo IsAbolish
        {
            get { return (HttpContext.Current.Session["IsAbolish"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsAbolish"]; }
            set { HttpContext.Current.Session["IsAbolish"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsPrice 是否有查詢單價權限
        /// </summary>
        public static ezEnum.YesNo IsPrice
        {
            get { return (HttpContext.Current.Session["IsPrice"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsPrice"]; }
            set { HttpContext.Current.Session["IsPrice"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsPrint 是否有列印權限
        /// </summary>
        public static ezEnum.YesNo IsPrint
        {
            get { return (HttpContext.Current.Session["IsPrint"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsPrint"]; }
            set { HttpContext.Current.Session["IsPrint"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsDownload 是否有下載權限
        /// </summary>
        public static ezEnum.YesNo IsDownload
        {
            get { return (HttpContext.Current.Session["IsDownload"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsDownload"]; }
            set { HttpContext.Current.Session["IsDownload"] = value; }
        }
        /// <summary>
        /// 取得或設定 IsExport 是否有匯出權限
        /// </summary>
        public static ezEnum.YesNo IsExport
        {
            get { return (HttpContext.Current.Session["IsExport"] == null) ? ezEnum.YesNo.Yes : (ezEnum.YesNo)HttpContext.Current.Session["IsExport"]; }
            set { HttpContext.Current.Session["IsExport"] = value; }
        }
        /// <summary>
        /// 取得或設定 DebugMode 是否為除錯模式
        /// </summary>
        public static bool DebugMode
        {
            get { return (HttpContext.Current.Session["DebugMode"] == null) ? false : (HttpContext.Current.Session["DebugMode"].ToString() == "1" ? true : false); }
            set { HttpContext.Current.Session["DebugMode"] = (value) ? "1" : "0"; }
        }
        /// <summary>
        /// 取得或設定 PrgIcon 選單中自動加入圖示
        /// </summary>
        public static bool PrgIcon
        {
            get { return (HttpContext.Current.Session["PrgIcon"] == null) ? false : (HttpContext.Current.Session["PrgIcon"].ToString() == "1" ? true : false); }
            set { HttpContext.Current.Session["PrgIcon"] = (value) ? "1" : "0"; }
        }
    }
}