using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxMenu 自定公用功能類別
    /// </summary>
    public class ezASPxMenu
    {
        public static void Init(object sender, EventArgs e)
        {
            SetFontSize(sender);
        }
        private static void SetFontSize(object sender)
        {
            (sender as ASPxMenu).Font.Size = ezSession.FontSize;
        }
    }
}