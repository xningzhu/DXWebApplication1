using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxButton 自定公用功能類別
    /// </summary>
    public static class ezASPxButton
    {
        public static void Init(object sender, EventArgs e)
        {
            SetFontSize(sender);
        }
        public static void Init(object sender, EventArgs e, bool bVisible)
        {
            SetFontSize(sender);
            (sender as ASPxButton).Visible = bVisible;
        }
        public static void Init(object sender, EventArgs e, bool bVisible, bool bMaxWidth)
        {
            SetFontSize(sender);
            (sender as ASPxButton).Visible = bVisible;
            (sender as ASPxButton).Width = Unit.Percentage(100);
        }

        private static void SetFontSize(object sender)
        {
            (sender as ASPxButton).Font.Size = ezSession.FontSize;
        }
    }
}