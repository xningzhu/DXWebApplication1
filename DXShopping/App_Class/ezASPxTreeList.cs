using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxTreeList 自定公用功能類別
    /// </summary>
    public static class ezASPxTreeList
    {
        /// <summary>
        /// 初始化設定
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bPageMode">是否要分頁</param>
        /// <param name="bAllowNodeDragDrop">是否允許拖放編輯</param>
        /// <param name="bSecurity">是否檢查使用者是否有權限</param>
        public static void Init(object sender, EventArgs e, bool bPageMode, bool bAllowNodeDragDrop, bool bSecurity)
        {
            //環境設定
            (sender as ASPxTreeList).Font.Size = ezSession.FontSize;
            (sender as ASPxTreeList).Settings.GridLines = GridLines.Both;
            (sender as ASPxTreeList).Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
            (sender as ASPxTreeList).Width = Unit.Percentage(100);
            (sender as ASPxTreeList).SettingsBehavior.AllowFocusedNode = true;
            //編輯設定
            (sender as ASPxTreeList).SettingsEditing.AllowNodeDragDrop = bAllowNodeDragDrop;
            //編輯彈出視窗設定
            (sender as ASPxTreeList).SettingsEditing.Mode = TreeListEditMode.EditForm;
            (sender as ASPxTreeList).SettingsPopupEditForm.AllowResize = true;
            (sender as ASPxTreeList).SettingsPopupEditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            (sender as ASPxTreeList).SettingsPopupEditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            (sender as ASPxTreeList).SettingsPopupEditForm.Modal = true;
            (sender as ASPxTreeList).SettingsPopupEditForm.Width = Unit.Pixel(800);
            //頁面模式
            if (bPageMode)
            {
                (sender as ASPxTreeList).SettingsPager.Mode = TreeListPagerMode.ShowPager;
                (sender as ASPxTreeList).SettingsPager.AlwaysShowPager = true;
                (sender as ASPxTreeList).SettingsPager.PageSizeItemSettings.ShowAllItem = true;
                (sender as ASPxTreeList).SettingsPager.PageSizeItemSettings.Visible = true;
            }
            else
                (sender as ASPxTreeList).SettingsPager.Mode = TreeListPagerMode.ShowAllNodes;
            //設定權限
            if (bSecurity) SetSecutity(sender);
            //設定語言
            SetLanguage(sender);
            //設定第一層新增鍵
            SetShowNewButtonInHeader(sender);
        }

        private static void SetShowNewButtonInHeader(object sender)
        {
            if ((sender as ASPxTreeList).SettingsDataSecurity.AllowInsert)
            {
                for (int i = 0; i < (sender as ASPxTreeList).Columns.Count; i++)
                {
                    if (((sender as ASPxTreeList).Columns[i]) is TreeListCommandColumn)
                    {
                        if (((sender as ASPxTreeList).Columns[i] as TreeListCommandColumn).NewButton.Visible)
                            ((sender as ASPxTreeList).Columns[i] as TreeListCommandColumn).ShowNewButtonInHeader = true;
                    }
                }
            }
        }

        private static void SetLanguage(object sender)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (currentCulture.Name == "zh-TW")
            {
                (sender as ASPxTreeList).SettingsPager.Summary.EmptyText = "無資料";
                (sender as ASPxTreeList).SettingsPager.Summary.AllPagesText = "頁數: 第{0}頁,共{1}頁 ({2} 筆)";
                (sender as ASPxTreeList).SettingsPager.Summary.Text = "頁數: 第{0}頁,共{1}頁 ({2} 筆)";
            }
        }

        /// <summary>
        /// 設定權限
        /// </summary>
        /// <param name="sender">sender</param>
        private static void SetSecutity(object sender)
        {
            if (HttpContext.Current.Session["IsAdd"] == null) HttpContext.Current.Session["IsAdd"] = ezEnum.YesNo.Yes;
            if (HttpContext.Current.Session["IsEdit"] == null) HttpContext.Current.Session["IsEdit"] = ezEnum.YesNo.Yes;
            if (HttpContext.Current.Session["IsDelete"] == null) HttpContext.Current.Session["IsDelete"] = ezEnum.YesNo.Yes;

            if ((ezEnum.YesNo)HttpContext.Current.Session["IsAdd"] == ezEnum.YesNo.No) (sender as ASPxTreeList).SettingsDataSecurity.AllowInsert = false;
            if ((ezEnum.YesNo)HttpContext.Current.Session["IsEdit"] == ezEnum.YesNo.No) (sender as ASPxTreeList).SettingsDataSecurity.AllowEdit = false;
            if ((ezEnum.YesNo)HttpContext.Current.Session["IsDelete"] == ezEnum.YesNo.No) (sender as ASPxTreeList).SettingsDataSecurity.AllowDelete = false;
        }
        /// <summary>
        /// 設定 FocusedNodeChanged 有作用 , 在 INIT 中呼叫
        /// </summary>
        /// <param name="sender">sender</param>
        public static void SetFocusedNodeChangedEvent(object sender)
        {
            (sender as ASPxTreeList).EnableCallbacks = false;
            (sender as ASPxTreeList).SettingsBehavior.ProcessFocusedNodeChangedOnServer = true;
        }
        /// <summary>
        /// 設定 SelectionChanged 事件有作用 , 在 INIT 中呼叫
        /// </summary>
        /// <param name="sender"></param>
        public static void SetSelectionChangedEvent(object sender)
        {
            (sender as ASPxTreeList).EnableCallbacks = false;
            (sender as ASPxTreeList).SettingsBehavior.ProcessSelectionChangedOnServer = true;
        }
        /// <summary>
        /// 取得焦點所在列的欄位值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ColumnName">欄位名稱</param>
        /// <returns></returns>
        public static object GetFocusedNodeValue(object sender, string ColumnName)
        {
            object objValue = null;
            TreeListNode node = (sender as ASPxTreeList).FocusedNode;
            if (node != null)
            {
                objValue = node[ColumnName];
            }
            return objValue;
        }
        /// <summary>
        /// 取得指定層級焦點所在列的欄位值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ColumnName">欄位名稱</param>
        /// <param name="iLevel">層級</param>
        /// <returns></returns>
        public static object GetFocusedNodeValue(object sender, string ColumnName, int iLevel)
        {
            object objValue = null;
            TreeListNode node = (sender as ASPxTreeList).FocusedNode;
            if (node != null)
            {
                if (node.Level == iLevel) objValue = node[ColumnName];
            }
            return objValue;
        }
        /// <summary>
        /// 取得選取第一筆的欄位值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ColumnName">欄位名稱</param>
        /// <returns></returns>
        public static object GetSelectedNodeValue(object sender, string ColumnName)
        {
            object objValue = null;
            List<TreeListNode> nodes = (sender as ASPxTreeList).GetSelectedNodes();
            if (nodes.Count > 0)
            {
                objValue = nodes[0].GetValue(ColumnName);
            }
            return objValue;
        }
        /// <summary>
        /// 取得選取所有的欄位值,以文字陣列型態存在
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ColumnName">欄位名稱</param>
        /// <returns></returns>
        public static List<string> GetSelectedNodeValues(object sender, string ColumnName)
        {
            List<string> str_values = new List<string>();
            List<TreeListNode> nodes = (sender as ASPxTreeList).GetSelectedNodes();
            if (nodes.Count > 0)
            {
                foreach (TreeListNode node in nodes)
                {
                    str_values.Add(node.GetValue(ColumnName).ToString());
                }
            }
            return str_values;
        }
    }
}