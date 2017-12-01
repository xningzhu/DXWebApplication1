using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxRoundPanel 自定公用功能類別
    /// </summary>
	public static class ezASPxRoundPanel
	{
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bHeaderText">標題名稱</param>
        public static void Init(object sender, EventArgs e, bool bHeaderText)
        {
            if (bHeaderText)
            {
                object objPrgNo = HttpContext.Current.Session["PrgNo"];
                object objPrgName = HttpContext.Current.Session["PrgName"];
                string strPrgNo = (objPrgNo == null) ? "" : objPrgNo.ToString();
                string strPrgName = (objPrgName == null) ? "" : objPrgName.ToString();

                if (string.IsNullOrEmpty(strPrgNo))
                {
                    string str_page_name = System.IO.Path.GetFileName(HttpContext.Current.Request.PhysicalPath);

                    //程式代號及名稱則到資料庫中以網頁名稱查詢
                    ezSession.PrgNo = "None";
                    ezSession.PrgName = "(未設定)";

                    if (str_page_name.LastIndexOf('.') >= 0) str_page_name = str_page_name.Substring(0, str_page_name.LastIndexOf('.'));

                    string str_url_prg = "%" + str_page_name + "%";
                    ezSqlClient ezsql = new ezSqlClient();
                    ezsql.CommandText = "SELECT count(*) as counts FROM z_sys_prg WHERE url_prg like @url_prg";
                    ezsql.ParameterAdd("@url_prg", str_url_prg, true);
                    if (ezsql.HasRows)
                    {
                        int int_count = ezsql.GetSelectInt("counts");
                        if (int_count == 1)
                        {
                            ezsql.CommandText = "SELECT no_prg , name_prg FROM z_sys_prg WHERE url_prg like @url_prg";
                            ezsql.ParameterAdd("@url_prg", str_url_prg, true);
                            ezSession.PrgNo = ezsql.GetSelectString("no_prg");
                            ezSession.PrgName = ezsql.GetSelectString("name_prg");
                            strPrgNo = ezSession.PrgNo;
                            strPrgName = ezSession.PrgName;
                        }
                    }
                    ezsql.Close();
                }

                string strHeaderText = (string.IsNullOrEmpty(strPrgNo)) ? "未指定" : strPrgNo + " [" + strPrgName + "]";
                (sender as ASPxRoundPanel).HeaderText = strHeaderText;
            }
            (sender as ASPxRoundPanel).Font.Size = ezSession.FontSize;
            (sender as ASPxRoundPanel).ShowCollapseButton = false;
        }
	}
}