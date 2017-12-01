using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxGridView 自定公用功能類別
    /// </summary>
    public static class ezASPxGridView
    {
        /// <summary>
        /// 參數型態
        /// </summary>
        public enum ParmType
        {
            Integer = 0,
            String = 1
        }

        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bSecurity">是否檢查使用者是否有權限</param>
        public static void Init(object sender, EventArgs e, bool bSecurity)
        {
            Init(sender, e, bSecurity, 3 , 800);
        }
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bSecurity">是否檢查使用者是否有權限</param>
        /// <param name="iEditFormColumnCount">編輯的欄位數</param>
        public static void Init(object sender, EventArgs e, bool bSecurity, int iEditFormColumnCount)
        {
            Init(sender, e, bSecurity, iEditFormColumnCount, 800);
        }
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bSecurity">是否檢查使用者是否有權限</param>
        /// <param name="iEditFormColumnCount">編輯的欄位數</param>
        /// <param name="iEditFormWidth">編輯的寛度(pixel)</param>
        public static void Init(object sender, EventArgs e, bool bSecurity, int iEditFormColumnCount, int iEditFormWidth)
        {
            //設定編輯模式
            if ((sender as ASPxGridView).SettingsEditing.Mode != GridViewEditingMode.Batch)
                (sender as ASPxGridView).SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;

            (sender as ASPxGridView).SettingsEditing.EditFormColumnCount = iEditFormColumnCount;
            //設定彈跳式視窗
            (sender as ASPxGridView).SettingsPopup.EditForm.AllowResize = true;
            (sender as ASPxGridView).SettingsPopup.EditForm.Modal = true;
            (sender as ASPxGridView).SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            (sender as ASPxGridView).SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            (sender as ASPxGridView).SettingsPopup.EditForm.Width = Unit.Pixel(iEditFormWidth);
            //設定頁數
            (sender as ASPxGridView).SettingsPager.AllButton.Visible = true;
            (sender as ASPxGridView).SettingsPager.PageSizeItemSettings.Visible = true;
            //設定參數
            (sender as ASPxGridView).SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            (sender as ASPxGridView).SettingsText.CommandBatchEditCancel = "取消";
            (sender as ASPxGridView).SettingsText.CommandBatchEditUpdate = "存檔";
            (sender as ASPxGridView).SettingsContextMenu.Enabled = true;
            //設定樣式
            (sender as ASPxGridView).Font.Size = ezSession.FontSize;
            (sender as ASPxGridView).SettingsBehavior.ConfirmDelete = true;
            (sender as ASPxGridView).Styles.TitlePanel.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            (sender as ASPxGridView).StylesEditors.ReadOnly.BackColor = ColorTranslator.FromHtml("#CCCCCC");
            (sender as ASPxGridView).Styles.TitlePanel.BackColor = ColorTranslator.FromHtml("#444444");
            (sender as ASPxGridView).Styles.TitlePanel.ForeColor = Color.White;
            (sender as ASPxGridView).Styles.TitlePanel.HorizontalAlign = HorizontalAlign.Left;
            (sender as ASPxGridView).Styles.TitlePanel.Font.Size = ezSession.FontSize;
            (sender as ASPxGridView).Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.False;
            (sender as ASPxGridView).Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.False;
            //設定過濾條件
            (sender as ASPxGridView).Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            (sender as ASPxGridView).Settings.ShowFilterRow = true;
            (sender as ASPxGridView).Settings.ShowFilterRowMenu = true;
            (sender as ASPxGridView).Settings.ShowHeaderFilterButton = true;
            (sender as ASPxGridView).Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            (sender as ASPxGridView).Settings.ShowFilterRow = true;
            (sender as ASPxGridView).Settings.ShowFilterRowMenu = true;
            (sender as ASPxGridView).Settings.ShowHeaderFilterButton = true;
            //設定權限
            if (bSecurity) SetSecutity(sender);
            //設定語言
            SetLanguage(sender);
        }

        public static void StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e , string ColumnName , string SessionName)
        {
            object obj_value = (sender as ASPxGridView).GetRowValuesByKeyValue(e.EditingKeyValue, ColumnName);
            HttpContext.Current.Session[SessionName] = (obj_value == null) ? string.Empty : obj_value.ToString();
        }

        public static void StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e, string ColumnName, ParmType SessionType , int index)
        {
            string SessionName = "v";
            SessionName += (SessionType == ParmType.Integer) ? "iparm" : "sparm";
            SessionName += index.ToString();
            object obj_value = (sender as ASPxGridView).GetRowValuesByKeyValue(e.EditingKeyValue, ColumnName);
            HttpContext.Current.Session[SessionName] = (obj_value == null) ? string.Empty : obj_value.ToString();
        }

        private static void SetLanguage(object sender)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (currentCulture.Name == "zh-TW")
            {
                (sender as ASPxGridView).SettingsPager.Summary.EmptyText = "無資料";
                (sender as ASPxGridView).SettingsPager.Summary.AllPagesText = "頁數: 第{0}頁,共{1}頁 ({2} 筆)";
                (sender as ASPxGridView).SettingsPager.Summary.Text = "頁數: 第{0}頁,共{1}頁 ({2} 筆)";
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

            if ((ezEnum.YesNo)HttpContext.Current.Session["IsAdd"] == ezEnum.YesNo.No) (sender as ASPxGridView).SettingsDataSecurity.AllowInsert = false;
            if ((ezEnum.YesNo)HttpContext.Current.Session["IsEdit"] == ezEnum.YesNo.No) (sender as ASPxGridView).SettingsDataSecurity.AllowEdit = false;
            if ((ezEnum.YesNo)HttpContext.Current.Session["IsDelete"] == ezEnum.YesNo.No) (sender as ASPxGridView).SettingsDataSecurity.AllowDelete = false;
        }
        /// <summary>
        /// BeforePerformDataSelect 中將父 Gridview 主鍵欄位(PK)值填入 Session 變數中
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sParmName">Session 變數名稱</param>
        public static void BeforePerformDataSelect(object sender, EventArgs e, string sParmName)
        {
            HttpContext.Current.Session[sParmName] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }
        /// <summary>
        /// BeforePerformDataSelect 中將父 Gridview 指定欄位值填入 Session 變數中
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sParmName">Session 變數名稱</param>
        /// <param name="sColName">指定欄位</param>
        public static void BeforePerformDataSelect(object sender, EventArgs e, string sParmName, string sColName)
        {
            HttpContext.Current.Session[sParmName] = (sender as ASPxGridView).GetMasterRowFieldValues(sColName).ToString();
        }
        /// <summary>
        /// 取得選取欄位的值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ColumnName">選取欄位</param>
        /// <returns></returns>
        public static object GetSelectedRowValue(object sender, string ColumnName)
        {
            object objValue = null;
            if ((sender as ASPxGridView).Selection.Count > 0)
            {
                List<Object> selectedValues = (sender as ASPxGridView).GetSelectedFieldValues(ColumnName);
                if (selectedValues.Count > 0) objValue = selectedValues[0];
            }
            return objValue;
        }
        /// <summary>
        /// 加入錯誤的訊息
        /// </summary>
        /// <param name="errors">error</param>
        /// <param name="column">column</param>
        /// <param name="errorText">錯誤訊息</param>
        public static void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        /// <summary>
        /// 檢查輸入欄位是否未輸入
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的要檢查否未輸入欄位名稱</param>
        public static void CheckRequire(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName)
        {
            CheckRequire(sender, e, sDataColName, sDataColName, "", true);
        }

        /// <summary>
        /// 檢查輸入欄位是否未輸入
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的要檢查否未輸入欄位名稱</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        public static void CheckRequire(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sDispColName, bool bShowInColumn)
        {
            CheckRequire(sender, e, sDataColName, sDispColName, "", bShowInColumn);
        }

        /// <summary>
        /// 檢查輸入欄位是否未輸入
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的要檢查否未輸入欄位名稱</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="sErrorText">錯誤訊息 , 空白為顯示 [此欄位不可空白!!]</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckRequire(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sDispColName, string sErrorText, bool bShowInColumn)
        {
            if (CheckColumnExists(sender, sDataColName))
            {
                if (string.IsNullOrEmpty(sErrorText)) sErrorText = "欄位不可空白!!";
                if (!bShowInColumn && !string.IsNullOrEmpty(sDispColName))
                    sErrorText = (sender as ASPxGridView).Columns[sDispColName].Caption + sErrorText;

                if (e.NewValues[sDataColName] == null)
                {
                    if (bShowInColumn && !string.IsNullOrEmpty(sDispColName))
                        AddError(e.Errors, (sender as ASPxGridView).Columns[sDispColName], sErrorText);
                    else
                        e.RowError = sErrorText;
                }
            }
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sdsSqlDataSource">SqlDataSource 物件</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, SqlDataSource sdsSqlDataSource)
        {
            string str_table_name = ezSqlDataSource.GetTableNameFromInsertQuery(sdsSqlDataSource);
            GridViewDataColumn dataColumn = (GridViewDataColumn)(sender as ASPxGridView).Columns[sDataColName];
            string str_column_name = dataColumn.FieldName;
            CheckDuplicate(sender, e, sDataColName, str_table_name, str_column_name, sDataColName, "", "", true);
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sdsSqlDataSource">SqlDataSource 物件</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, SqlDataSource sdsSqlDataSource, string sDispColName, bool bShowInColumn)
        {
            string str_table_name = ezSqlDataSource.GetTableNameFromInsertQuery(sdsSqlDataSource);
            GridViewDataColumn dataColumn = (GridViewDataColumn)(sender as ASPxGridView).Columns[sDataColName];
            string str_column_name = dataColumn.FieldName;
            CheckDuplicate(sender, e, sDataColName, str_table_name, str_column_name, sDispColName, "", bShowInColumn);
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sdsSqlDataSource">SqlDataSource 物件</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="sWhereString">另要加入的 Where 條件式</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, SqlDataSource sdsSqlDataSource, string sDispColName, string sWhereString, bool bShowInColumn)
        {
            string str_table_name = ezSqlDataSource.GetTableNameFromInsertQuery(sdsSqlDataSource);
            GridViewDataColumn dataColumn = (GridViewDataColumn)(sender as ASPxGridView).Columns[sDataColName];
            string str_column_name = dataColumn.FieldName;
            CheckDuplicate(sender, e, sDataColName, str_table_name, str_column_name, sDispColName, sWhereString, "", bShowInColumn);
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sdsSqlDataSource">SqlDataSource 物件</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="sWhereString">另要加入的 Where 條件式</param>
        /// <param name="sErrorText">錯誤訊息 , 空白為顯示 [不可重覆輸入!!]</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, SqlDataSource sdsSqlDataSource, string sDispColName, string sWhereString, string sErrorText, bool bShowInColumn)
        {
            string str_table_name = ezSqlDataSource.GetTableNameFromInsertQuery(sdsSqlDataSource);
            GridViewDataColumn dataColumn = (GridViewDataColumn)(sender as ASPxGridView).Columns[sDataColName];
            string str_column_name = dataColumn.FieldName;
            CheckDuplicate(sender, e, sDataColName, str_table_name, str_column_name, sDispColName, sWhereString, sErrorText, bShowInColumn);
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sTableName">要查詢的表格名稱</param>
        /// <param name="sColName">要查詢的表格的欄位名稱</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sTableName, string sColName)
        {
            CheckDuplicate(sender, e, sDataColName, sTableName, sColName, sDataColName, "", "", true);
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sTableName">要查詢的表格名稱</param>
        /// <param name="sColName">要查詢的表格的欄位名稱</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sTableName, string sColName, string sDispColName, bool bShowInColumn)
        {
            if (CheckColumnExists(sender, sDataColName))
            {
                CheckDuplicate(sender, e, sDataColName, sTableName, sColName, sDispColName, "", "", bShowInColumn);
            }
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sTableName">要查詢的表格名稱</param>
        /// <param name="sColName">要查詢的表格的欄位名稱</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="sWhereString">另要加入的 Where 條件式</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sTableName, string sColName, string sDispColName, string sWhereString, bool bShowInColumn)
        {
            if (CheckColumnExists(sender, sDataColName))
            {
                CheckDuplicate(sender, e, sDataColName, sTableName, sColName, sDispColName, sWhereString, "", bShowInColumn);
            }
        }

        /// <summary>
        /// 檢查輸入欄位是否重覆
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sDataColName">Gridview 中的欄位名稱</param>
        /// <param name="sTableName">要查詢的表格名稱</param>
        /// <param name="sColName">要查詢的表格的欄位名稱</param>
        /// <param name="sDispColName">顯示在Gridview 中的那個欄位</param>
        /// <param name="sWhereString">另要加入的 Where 條件式</param>
        /// <param name="sErrorText">錯誤訊息 , 空白為顯示 [不可重覆輸入!!]</param>
        /// <param name="bShowInColumn">錯誤訊息是否顯示在欄位後面</param>
        public static void CheckDuplicate(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e, string sDataColName, string sTableName, string sColName, string sDispColName, string sWhereString, string sErrorText, bool bShowInColumn)
        {
            int int_data = 0;
            string str_data = "";
            string str_type = (e.NewValues[sDataColName] == null) ? "" : e.NewValues[sDataColName].GetType().Name;

            //欄位型態不是文字或整數時不判斷
            if (str_type != "String" && str_type != "Int32") return;

            //未輸入時不檢查
            if (e.NewValues[sDataColName] == null) return;
            if (str_type == "String" && e.NewValues[sDataColName].ToString() == "") return;

            //修改時有修改才要判斷重覆
            if (!e.IsNewRow) if (e.NewValues[sDataColName] == e.OldValues[sDataColName]) return;


            ezSqlClient ezsql = new ezSqlClient();
            ezsql.CommandText = "SELECT " + sDataColName + " FROM " + sTableName + " WHERE " + sColName + " = @new_value ";
            str_data = (e.NewValues[sDataColName] == null) ? "" : e.NewValues[sDataColName].ToString();
            if (str_type == "String") ezsql.ParameterAdd("@new_value", str_data, true);
            if (str_type == "Int32")
            {
                if (string.IsNullOrEmpty(str_data)) str_data = "0";
                int_data = int.Parse(str_data);
                ezsql.ParameterAdd("@new_value", int_data, true);
            }
            //修改時要確認不要判斷到同筆,避免誤判
            if (!e.IsNewRow)
            {
                ezsql.CommandText += "AND " + sColName + " <> @old_value ";
                str_data = (e.OldValues[sDataColName] == null) ? "" : e.OldValues[sDataColName].ToString();
                if (str_type == "String") ezsql.ParameterAdd("@old_value", str_data, false);
                if (str_type == "int")
                {
                    if (string.IsNullOrEmpty(str_data)) str_data = "0";
                    int_data = int.Parse(str_data);
                    ezsql.ParameterAdd("@old_value", int_data, false);
                }
            }
            if (!string.IsNullOrEmpty(sWhereString)) ezsql.CommandText += "AND " + sWhereString;
            bool bln_hasrow = ezsql.HasRows;
            ezsql.Close();

            if (string.IsNullOrEmpty(sErrorText)) sErrorText = "不可重覆輸入!!";
            if (!bShowInColumn && !string.IsNullOrEmpty(sDispColName))
                sErrorText = (sender as ASPxGridView).Columns[sDispColName].Caption + sErrorText;

            if (bln_hasrow)
            {
                if (bShowInColumn && !string.IsNullOrEmpty(sDispColName))
                    AddError(e.Errors, (sender as ASPxGridView).Columns[sDispColName], sErrorText);
                else
                    e.RowError = sErrorText;
            }
        }

        private static bool CheckColumnExists(object sender, string sColName)
        {
            bool bln_checked = false;
            List<string> fieldNames = new List<string>();
            fieldNames = ezGetColumnNames(sender);
            foreach (string fieldName in fieldNames)
            {
                if (sColName == fieldName) { bln_checked = true; break; }
            }
            return bln_checked;
        }

        /// <summary>
        /// 輸入欄位預設值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sColName">Gridview 中的欄位名稱</param>
        /// <param name="oValue">預設值</param>
        public static void InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e, string sColName, object oValue)
        {
            e.NewValues[sColName] = oValue;
        }
        /// <summary>
        /// 設定 Title 列名稱 (在 Init 事件中呼叫)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="sTitle">標題名稱</param>
        public static void SetTitle(object sender, EventArgs e, string sTitle)
        {
            (sender as ASPxGridView).Settings.ShowTitlePanel = true;
            (sender as ASPxGridView).Styles.TitlePanel.HorizontalAlign = HorizontalAlign.Left;
            (sender as ASPxGridView).Styles.TitlePanel.BackColor = ColorTranslator.FromHtml("#333333");
            (sender as ASPxGridView).Styles.TitlePanel.ForeColor = Color.White;
            (sender as ASPxGridView).Styles.TitlePanel.Font.Size = FontUnit.Medium;
            (sender as ASPxGridView).SettingsText.Title = sTitle;
        }
        /// <summary>
        /// 設定頁數選項 (在 Init 事件中呼叫)
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bShowNumericButtons">顯示數字頁數</param>
        /// <param name="bNextPageButton">顯示下一頁</param>
        /// <param name="bPrevPageButton">顯示上一頁</param>
        /// <param name="bSummary">顯示合計頁</param>
        /// <param name="bAllButton">顯示全部鍵</param>
        /// <param name="bPageSizeItemSettings">顯示頁數下拉清單</param>
        public static void SetPager(object sender, EventArgs e, bool bShowNumericButtons, bool bNextPageButton, bool bPrevPageButton, bool bSummary, bool bAllButton, bool bPageSizeItemSettings)
        {
            (sender as ASPxGridView).SettingsPager.ShowNumericButtons = bShowNumericButtons;
            (sender as ASPxGridView).SettingsPager.NextPageButton.Visible = bNextPageButton;
            (sender as ASPxGridView).SettingsPager.PrevPageButton.Visible = bPrevPageButton;
            (sender as ASPxGridView).SettingsPager.Summary.Visible = bSummary;
            (sender as ASPxGridView).SettingsPager.AllButton.Visible = bAllButton;
            (sender as ASPxGridView).SettingsPager.PageSizeItemSettings.Visible = bPageSizeItemSettings;
        }
        /// <summary>
        /// 設定是否為單選
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="bSingleSelect">是否為單選</param>
        public static void SetSingleSelect(object sender, EventArgs e, bool bSingleSelect)
        {
            (sender as ASPxGridView).SettingsBehavior.AllowSelectSingleRowOnly = bSingleSelect;
            (sender as ASPxGridView).SettingsBehavior.ProcessSelectionChangedOnServer = true;
        }

        /// <summary>
        /// 取得複選記錄欄位之選取值
        /// </summary>
        /// <param name="sColumnName">欄位陣列名稱</param>
        /// <returns>物件陣列變數</returns>
        public static List<object> ezGetSelectedFieldValues(object sender, string[] sColumnName)
        {
            List<object> selectedValues = (sender as ASPxGridView).GetSelectedFieldValues(sColumnName);
            return selectedValues;
        }

        /// <summary>
        /// 取得所有欄位名稱
        /// </summary>
        /// <param name="sender">sender</param>
        /// <returns></returns>
        public static List<string> ezGetColumnNames(object sender)
        {
            List<string> fieldNames = new List<string>();
            foreach (GridViewColumn column in (sender as ASPxGridView).Columns)
            {
                if (column is GridViewDataColumn)
                {
                    GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                    fieldNames.Add(dataColumn.FieldName);
                }
            }
            return fieldNames;
        }
    }
}