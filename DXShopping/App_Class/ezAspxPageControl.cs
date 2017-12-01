using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ezAspxPageControl 自定公用功能類別
    /// </summary>
    public static class ezAspxPageControl
    {
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        public static void Init(object sender, EventArgs e)
        {
            (sender as ASPxPageControl).ActiveTabIndex = 0;
            (sender as ASPxPageControl).Font.Size = ezSession.FontSize;
            (sender as ASPxPageControl).TabStyle.Font.Size = ezSession.FontSize;
        }
    }
}