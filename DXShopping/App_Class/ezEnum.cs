using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezapp
{
    /// <summary>
    /// 自定義枚舉類型
    /// </summary>
    public class ezEnum
    {
        /// <summary>
        /// 是或否
        /// </summary>
        public enum YesNo
        {
            /// <summary>
            /// 是
            /// </summary>
            Yes = 1,
            /// <summary>
            /// 否
            /// </summary>
            No = 0
        }
        /// <summary>
        /// 系統語系
        /// </summary>
        public enum Language
        {
            /// <summary>
            /// 繁體中文
            /// </summary>
            zh_TW = 0,
            /// <summary>
            /// 簡體中文
            /// </summary>
            zh_CN = 1,
            /// <summary>
            /// 英文
            /// </summary>
            en_US = 2
        }
        /// <summary>
        /// AppSetting Key 值
        /// </summary>
        public enum AppKey
        {
            AppName = 0,
            DebugMode = 1,
            AdminUrl = 2,
            LoginUrl = 3,
            HomeUrl = 4,
            ConnName = 5,
            Language = 6,
            PrgIcon = 7
        }
        public enum LoginRole
        {
            /// <summary>
            /// 使用者
            /// </summary>
            User = 0,
            /// <summary>
            /// 管理者
            /// </summary>
            Admin = 1,
            /// <summary>
            /// 會員
            /// </summary>
            Member = 2,
            /// <summary>
            /// 廠商
            /// </summary>
            Vendor = 3,
            /// <summary>
            /// 客戶
            /// </summary>
            Customer = 4,
            /// <summary>
            /// 遊客
            /// </summary>
            Guest = 5
        }
        public enum CompCode
        {
            /// <summary>
            /// 沒有公司
            /// </summary>
            None = 0,
            /// <summary>
            /// 公司
            /// </summary>
            Company = 1,
            /// <summary>
            /// 廠商
            /// </summary>
            Vendor = 2,
            /// <summary>
            /// 客戶
            /// </summary>
            Customer = 3
        }
    }
}