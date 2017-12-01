using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.XtraScheduler;

namespace ezapp
{
    /// <summary>
    /// ezASPxScheduler 自定公用功能類別
    /// </summary>
    public static class ezASPxScheduler
    {
        /// <summary>
        /// Init 自定預設設定值
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        public static void Init(object sender, EventArgs e, bool bShowTime)
        {
            (sender as ASPxScheduler).Font.Size = ezSession.FontSize;
            //中文修正
            (sender as ASPxScheduler).Views.DayView.ShortDisplayName = "日曆";
            (sender as ASPxScheduler).Views.WorkWeekView.ShortDisplayName = "工作週";
            (sender as ASPxScheduler).Views.WeekView.ShortDisplayName = "週曆";
            (sender as ASPxScheduler).Views.MonthView.ShortDisplayName = "月曆";
            (sender as ASPxScheduler).Views.TimelineView.ShortDisplayName = "時間軸";
            (sender as ASPxScheduler).Views.FullWeekView.ShortDisplayName = "整週";
            (sender as ASPxScheduler).OptionsView.NavigationButtons.NextCaption = "下個行程";
            (sender as ASPxScheduler).OptionsView.NavigationButtons.PrevCaption = "上個行程";

            if (!bShowTime)
            {
                (sender as ASPxScheduler).MonthView.AppointmentDisplayOptions.StartTimeVisibility = AppointmentTimeVisibility.Never;
                (sender as ASPxScheduler).MonthView.AppointmentDisplayOptions.EndTimeVisibility = AppointmentTimeVisibility.Never;
            }
        }

        public static void PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
        {
            foreach (DevExpress.Web.MenuItem Item in e.Menu.Items)
            {
                if (Item.Name == "OpenAppointment") Item.Text = "開啟行程";
                if (Item.Name == "EditSeries") Item.Text = "新的行程";
                if (Item.Name == "RestoreOccurrence") Item.Text = "還原";
                if (Item.Name == "DeleteAppointment") Item.Text = "刪除行程";
                if (Item.Name == "NewAppointment") Item.Text = "新的行程";
                if (Item.Name == "NewAllDayEvent") Item.Text = "新的全天行程";
                if (Item.Name == "NewRecurringAppointment") Item.Text = "新的連續行程";
                if (Item.Name == "NewRecurringEvent") Item.Text = "新的連續事件";
                if (Item.Name == "GotoThisDay") Item.Text = "回到日曆";
                if (Item.Name == "GotoToday") Item.Text = "回到今天";
                if (Item.Name == "GotoDate") Item.Text = "回到指定日期";
                if (Item.Name == "SwitchViewMenu")
                {
                    Item.Text = "切換顯式方式";
                    foreach (DevExpress.Web.MenuItem SubItem in Item)
                    {
                        if (SubItem.Name == "SwitchToDayView") SubItem.Text = "切換成日曆";
                        if (SubItem.Name == "SwitchToWorkWeekView") SubItem.Text = "切換成工作週曆";
                        if (SubItem.Name == "SwitchToFullWeekView") SubItem.Text = "切換成整週曆";
                        if (SubItem.Name == "SwitchToMonthView") SubItem.Text = "切換成月曆";
                        if (SubItem.Name == "SwitchToTimelineView") SubItem.Text = "切換成時間軸曆";
                    }
                }
                if (Item.Name == "StatusSubMenu")
                {
                    Item.Text = "狀態選單";
                    foreach (DevExpress.Web.MenuItem SubItem in Item)
                    {
                        if (SubItem.Text == "Free") SubItem.Text = "有空";
                        if (SubItem.Text == "Tentative") SubItem.Text = "暫時";
                        if (SubItem.Text == "Busy") SubItem.Text = "忙碌";
                        if (SubItem.Text == "Out Of Office") SubItem.Text = "離開辦公室";
                        if (SubItem.Text == "Working Elsewhere") SubItem.Text = "在別處工作";
                    }
                }
                if (Item.Name == "LabelSubMenu")
                {
                    Item.Text = "標籤選單";
                    foreach (DevExpress.Web.MenuItem SubItem in Item)
                    {
                        if (SubItem.Text == "None") SubItem.Text = "無";
                        if (SubItem.Text == "Important") SubItem.Text = "重要的";
                        if (SubItem.Text == "Business") SubItem.Text = "商業";
                        if (SubItem.Text == "Personal") SubItem.Text = "私人";
                        if (SubItem.Text == "Vacation") SubItem.Text = "假期";
                        if (SubItem.Text == "Must Attend") SubItem.Text = "必須參加";
                        if (SubItem.Text == "Travel Required") SubItem.Text = "需要出差";
                        if (SubItem.Text == "Needs Preparation") SubItem.Text = "需要簡報";
                        if (SubItem.Text == "Birthday") SubItem.Text = "生日";
                        if (SubItem.Text == "Anniversary") SubItem.Text = "週年";
                        if (SubItem.Text == "Phone Call") SubItem.Text = "電話連絡";
                    }
                }
            }
        }

        public static void AppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            if (e.Action == SchedulerFormAction.Create)
                e.Container.Caption = "建立新的行程";
            else
                e.Container.Caption = "修改行程";
        }

        public static void GotoDateFormShowing(object sender, GotoDateFormEventArgs e)
        {
            e.Container.Caption = "回到指定日期";
        }

        /// <summary>
        /// 設定行事曆約會行程是否唯讀狀態
        /// </summary>
        /// <param name="blnReadonly"></param>
        public static void SetReadOnly(object sender, EventArgs e, bool blnReadonly)
        {
            if (blnReadonly)
            {
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentCopy = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDragBetweenResources = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.None;
                (sender as ASPxScheduler).OptionsCustomization.AllowDisplayAppointmentForm = AllowDisplayAppointmentForm.Never;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentMultiSelect = false;
            }
            else
            {
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentCopy = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentDragBetweenResources = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.All;
                (sender as ASPxScheduler).OptionsCustomization.AllowDisplayAppointmentForm = AllowDisplayAppointmentForm.Auto;
                (sender as ASPxScheduler).OptionsCustomization.AllowAppointmentMultiSelect = true;
            }
        }

        public static void SetToday(object sender, EventArgs e)
        {
            (sender as ASPxScheduler).Start = DateTime.Today;
        }
    }
}