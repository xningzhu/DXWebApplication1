using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ezapp
{

    public static class ezAppSettings
    {
        public static string GetAppValue(ezEnum.AppKey eAppKey)
        {
            string str_key = eAppKey.ToString();
            object objValue = WebConfigurationManager.AppSettings[str_key];
            return (objValue == null) ? "" : objValue.ToString();
        }
    }
}