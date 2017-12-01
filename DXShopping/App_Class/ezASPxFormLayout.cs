using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ezapp
{
    /// <summary>
    /// ASPxGridLookup 自定公用功能類別
    /// </summary>
    public static class ezASPxFormLayout
    {
        private static bool _ezReadOnly = false;
        private static bool _ezLayoutItemWidthMax = false;
        public static string ezCommand { get; set; }
        public static bool ezReadOnly { get { return _ezReadOnly; } set { _ezReadOnly = value; } }
        public static bool ezLayoutItemWidthMax { get { return _ezLayoutItemWidthMax; } set { _ezLayoutItemWidthMax = value; } }
        public static Color ezReadOnlyBackColor { get { return ColorTranslator.FromHtml("#CCCCCC"); } }

        public static void Init(object sender, EventArgs e, bool bReadOnly, bool bLayoutItemWidthMax)
        {
            ezCommand = "fontsize";
            SearchControl(sender);
            SetReadonly(sender , bReadOnly);
            if (bLayoutItemWidthMax) SetLayoutItemWidthMax(sender);
        }

        public static void SetReadonly(object sender, bool bReadonly)
        {
            ezCommand = "readonly";
            ezReadOnly = bReadonly;
            SearchControl(sender);
        }

        public static void SetLayoutItemWidthMax(object sender)
        {
            ezCommand = "widthmax";
            ezLayoutItemWidthMax = true;
            SearchControl(sender);
        }
        public static void Reset(object sender)
        {
            ezCommand = "reset";
            SearchControl(sender);
        }

        private static void SearchControl(object sender)
        {
            foreach (var item in (sender as ASPxFormLayout).Items)
            {
                if (item is LayoutGroupBase) (item as LayoutGroupBase).ForEach(GetNestedControls);
                if (item is LayoutItem) SetValue(item as LayoutItem);
                    
            }
        }

        private static void GetNestedControls(LayoutItemBase item)
        {
            foreach (LayoutItemBase c in item.Collection)
            {
                if (c is LayoutGroup) (c as LayoutGroup).ForEach(GetNestedControls);
                if (c is LayoutItem) SetValue(c as LayoutItem);
            }
        }

        private static void SetValue(LayoutItem c)
        {
            if (string.IsNullOrEmpty(ezCommand)) return;
            c.CaptionStyle.Font.Size = ezSession.FontSize;
            foreach (Control control in c.Controls)
            {
                if (control is ASPxEdit || control is ASPxCheckBox)
                {
                    if (ezCommand == "readonly")
                    {
                        if (control is ASPxEdit)
                        {
                            ((ASPxEdit)control).ReadOnlyStyle.BackColor = ezReadOnlyBackColor;
                            if (((ASPxEdit)control).AccessKey == "#")
                                ((ASPxEdit)control).ReadOnly = true;
                            else if (((ASPxEdit)control).AccessKey == "@")
                                ((ASPxEdit)control).ReadOnly = false;
                            else
                                ((ASPxEdit)control).ReadOnly = ezReadOnly;
                        }
                        if (control is ASPxCheckBox)
                        {
                            ((ASPxCheckBox)control).ReadOnlyStyle.BackColor = ezReadOnlyBackColor;
                            if (((ASPxCheckBox)control).AccessKey == "#")
                                ((ASPxCheckBox)control).ReadOnly = true;
                            else if (((ASPxCheckBox)control).AccessKey == "@")
                                ((ASPxCheckBox)control).ReadOnly = false;
                            else
                                ((ASPxCheckBox)control).ReadOnly = ezReadOnly;
                        }
                    }
                    if (ezCommand == "fontsize") ((ASPxEdit)control).Font.Size = ezSession.FontSize;
                    if (ezCommand == "widthmax")
                    {
                        if (ezLayoutItemWidthMax)
                            if ((control is ASPxTextBox) || (control is ASPxTextBox))
                                ((ASPxEdit)control).Width = Unit.Percentage(100);
                            else
                                ((ASPxEdit)control).Width = Unit.Empty;
                    }
                    if (ezCommand == "reset")
                    {
                        if (control is ASPxCheckBox) (control as ASPxCheckBox).Checked = false;
                        if (control is ASPxComboBox) (control as ASPxComboBox).Value = "";
                        if (control is ASPxComboBox) (control as ASPxComboBox).Text = "";
                        if (control is ASPxDateEdit) (control as ASPxDateEdit).Text = "";
                        if (control is ASPxDropDownEdit) (control as ASPxDropDownEdit).Text = "";
                        if (control is ASPxGridLookup) (control as ASPxGridLookup).Text = "";
                        if (control is ASPxGridLookup) (control as ASPxGridLookup).Value = "";
                        if (control is ASPxMemo) (control as ASPxMemo).Text = "";
                        if (control is ASPxSpinEdit) (control as ASPxSpinEdit).Text = "";
                        if (control is ASPxTextBox) (control as ASPxTextBox).Text = "";
                        if (control is ASPxTimeEdit) (control as ASPxTimeEdit).Text = "";
                    }
                }
            }
        }
    }
}