using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxGridLookup 自定公用功能類別
    /// </summary>
	public static class ezASPxGridLookup
	{
        /// <summary>
        /// 參數型態
        /// </summary>
        public enum ParmType
        {
            Integer = 0,
            String = 1
        }

        public static void Init(object sender, EventArgs e)
        {
            //Width
            (sender as ASPxGridLookup).Width = Unit.Percentage(100);
            (sender as ASPxGridLookup).GridView.Width = Unit.Pixel(500);
            //FontSize
            (sender as ASPxGridLookup).Font.Size = ezSession.FontSize;
            (sender as ASPxGridLookup).GridView.Font.Size = ezSession.FontSize;
            (sender as ASPxGridLookup).GridViewStyles.Table.Font.Size = ezSession.FontSize;
            (sender as ASPxGridLookup).GridViewStyles.Header.Font.Size = ezSession.FontSize;
            (sender as ASPxGridLookup).GridViewStyles.TitlePanel.Font.Size = ezSession.FontSize;
            (sender as ASPxGridLookup).GridViewStylesPager.Pager.Font.Size = ezSession.FontSize;
            //Focus
            (sender as ASPxGridLookup).GridViewProperties.SettingsBehavior.AllowFocusedRow = true;
            (sender as ASPxGridLookup).GridViewProperties.SettingsBehavior.AllowSelectSingleRowOnly = true;
            (sender as ASPxGridLookup).GridView.SettingsBehavior.EnableRowHotTrack = true;
            (sender as ASPxGridLookup).GridView.SettingsBehavior.AllowFocusedRow = true;
            //Filter
            (sender as ASPxGridLookup).GridView.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            (sender as ASPxGridLookup).GridView.Settings.ShowFilterRow = true;
            (sender as ASPxGridLookup).GridView.Settings.ShowFilterRowMenu = true;
            (sender as ASPxGridLookup).GridView.Settings.ShowHeaderFilterButton = true;
            (sender as ASPxGridLookup).GridViewProperties.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            (sender as ASPxGridLookup).GridViewProperties.Settings.ShowFilterRow = true;
            (sender as ASPxGridLookup).GridViewProperties.Settings.ShowFilterRowMenu = true;
            (sender as ASPxGridLookup).GridViewProperties.Settings.ShowHeaderFilterButton = true;
            //PageSize
            (sender as ASPxGridLookup).GridViewProperties.SettingsPager.PageSize = 5;
            (sender as ASPxGridLookup).GridViewProperties.SettingsPager.PageSizeItemSettings.ShowAllItem = true;
            (sender as ASPxGridLookup).GridViewProperties.SettingsPager.PageSizeItemSettings.Visible = true;
        }

        /// <summary>
        /// GridLookup 欄位值變更時儲存至 Session
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="SessionName">Session 名稱</param>
        public static void ValueChanged(object sender, EventArgs e , string SessionName)
        {
            object obj_value = (sender as ASPxGridLookup).Value;
            HttpContext.Current.Session[SessionName] = (obj_value == null) ? string.Empty : obj_value.ToString();
        }

        /// <summary>
        /// GridLookup 欄位值變更時儲存至 Session
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="SessionType">欄位型態</param>
        /// <param name="index">Session 索引</param>
        public static void ValueChanged(object sender, EventArgs e, ParmType SessionType, int index)
        {
            string SessionName = "v";
            SessionName += (SessionType == ParmType.Integer) ? "iparm" : "sparm";
            SessionName += index.ToString();
            object obj_value = (sender as ASPxGridLookup).Value;
            HttpContext.Current.Session[SessionName] = (obj_value == null) ? string.Empty : obj_value.ToString();
        }
    }
}