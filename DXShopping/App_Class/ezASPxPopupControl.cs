using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxPopupControl 自定公用功能類別
    /// </summary>
	public static class ezASPxPopupControl
	{
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        public static void Init(object sender, EventArgs e)
        {
            (sender as ASPxPopupControl).PopupHorizontalAlign = DevExpress.Web.PopupHorizontalAlign.WindowCenter;
            (sender as ASPxPopupControl).PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.WindowCenter;
            (sender as ASPxPopupControl).Modal = true;
            (sender as ASPxPopupControl).Font.Size = ezSession.FontSize;
            (sender as ASPxPopupControl).ShowCloseButton = false;
            (sender as ASPxPopupControl).AllowDragging = true;
            //(sender as ASPxPopupControl).ContentStyle.Paddings.Padding = Unit.Pixel(0);
        }
	}
}