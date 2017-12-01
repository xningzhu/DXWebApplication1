using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezapp
{
    /// <summary>
    /// Library
    /// </summary>
    public class ezLibrary
    {
        /// <summary>
        /// 以程式代號取得程式位置
        /// </summary>
        /// <param name="strPrgNo">程式代號</param>
        /// <returns></returns>
        public static string GetPrgUrlByPrgNo(string strPrgNo)
        {
            int intPrgID = 0;
            string strPrgUrl = "";
            string strPrgName = "";
            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT rowid , murl , mname FROM z_sys_program WHERE mno = @mno";
            ezsql.ParameterAdd("@mno", strPrgNo, true);
            intPrgID = ezsql.GetSelectInt("rowid", false);
            strPrgUrl = ezsql.GetSelectString("murl", false);
            strPrgName = ezsql.GetSelectString("mname", true);
            ezSession.PrgID = intPrgID.ToString();
            ezSession.PrgNo = strPrgNo;
            ezSession.PrgName = strPrgName;
            return strPrgUrl;
        }

        public static bool SetSecurity(string sPrgNo)
        {
            bool blnSecurity = false;

            ezSession.IsAdd = ezEnum.YesNo.No;
            ezSession.IsEdit = ezEnum.YesNo.No;
            ezSession.IsDelete = ezEnum.YesNo.No;
            ezSession.IsConfirm = ezEnum.YesNo.No;
            ezSession.IsPrice = ezEnum.YesNo.No;
            ezSession.IsPrint = ezEnum.YesNo.No;
            ezSession.IsDownload = ezEnum.YesNo.No;
            ezSession.IsAbolish = ezEnum.YesNo.No;
            ezSession.IsExport = ezEnum.YesNo.No;

            if (ezSession.LoginRole == ezEnum.LoginRole.User )
            {
                string strUserNo = ezSession.UserNo;
                ezSqlClient ezsql = new ezSqlClient();
                ezsql.CommandText = "SELECT isadd , isedit , isdelete , isconfirm , isprint , isexport ";
                ezsql.CommandText += "FROM z_sys_security WHERE user_no = @user_no AND prg_no = @prg_no";
                ezsql.ParameterAdd("@user_no", strUserNo, true);
                ezsql.ParameterAdd("@prg_no", sPrgNo, false);
                blnSecurity = ezsql.HasRows;
                if (blnSecurity)
                {
                    ezSession.IsAdd = (ezsql.GetSelectString("isadd") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                    ezSession.IsEdit = (ezsql.GetSelectString("isedit") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                    ezSession.IsDelete = (ezsql.GetSelectString("isdelete") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                    ezSession.IsConfirm = (ezsql.GetSelectString("isconfirm") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                    ezSession.IsPrint = (ezsql.GetSelectString("isprint") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                    ezSession.IsExport = (ezsql.GetSelectString("isexport") == "1") ? ezEnum.YesNo.Yes : ezEnum.YesNo.No;
                }
                ezsql.Close();
            }
            return blnSecurity;
        }

        public static bool SetIsPrgSecurity(string sPrgNo)
        {
            bool blnSecurity = false;
            if (ezSession.LoginRole == ezEnum.LoginRole.User)
            {
                string strUserNo = ezSession.UserNo;
                ezSqlClient ezsql = new ezSqlClient();
                ezsql.CommandText = "SELECT isadd , isedit , isdelete FROM z_sys_security WHERE user_no = @user_no AND prg_no = @prg_no";
                ezsql.ParameterAdd("@user_no", strUserNo, true);
                ezsql.ParameterAdd("@prg_no", sPrgNo, false);
                blnSecurity = ezsql.HasRows;
                ezsql.Close();
            }
            return blnSecurity;
        }

        public static bool SetIsModuleSecurity(string sModuleNo)
        {
            bool blnSecurity = false;
            if (ezSession.LoginRole == ezEnum.LoginRole.User)
            {
                string strUserNo = ezSession.UserNo;
                ezSqlClient ezsql = new ezSqlClient();
                ezsql.CommandText = "SELECT module.mno FROM  z_sys_security INNER JOIN ";
                ezsql.CommandText += "z_sys_program AS prg ON z_sys_security.prg_no = prg.mno INNER JOIN ";
                ezsql.CommandText += "z_sys_program AS module ON prg.parentid = module.rowid ";
                ezsql.CommandText += "WHERE (z_sys_security.user_no = @user_no) AND (module.mno = @module_no) AND (prg.mcode = N'1') AND ";
                ezsql.CommandText += "(prg.mlevel = '2') AND (module.mcode = N'1') AND (module.mlevel = '1')";
                ezsql.ParameterAdd("@user_no", strUserNo, true);
                ezsql.ParameterAdd("@module_no", sModuleNo, false);
                blnSecurity = ezsql.HasRows;
                ezsql.Close();
            }
            return blnSecurity;
        }

        public static string GetUserNo(string sUserName)
        {
            string strUserNo = sUserName;
            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT mno FROM z_org_user WHERE mname = @mname";
            ezsql.ParameterAdd("@mname", sUserName, true);
            if (ezsql.HasRows)
            { 
                strUserNo = ezsql.GetSelectString("mno", false);
            }
            ezsql.Close();
            return strUserNo;
        }

        public static string GetUserName(string sUserNo)
        {
            string strUserName = sUserNo;
            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT mname FROM z_org_user WHERE mno = @mno";
            ezsql.ParameterAdd("@mno", sUserNo, true);
            if (ezsql.HasRows)
            { 
                strUserName = ezsql.GetSelectString("mname", false);
            }
            ezsql.Close();
            return strUserName;
        }
    }
}