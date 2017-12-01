using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXShopping
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gv_master_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string str_msalary = e.NewValues["msalary"].ToString();
            int int_salary = int.Parse(str_msalary);
            if (int_salary > 100000 || int_salary < 3000)
            {
                AddError(e.Errors, (sender as ASPxGridView).Columns["msalary"], "基本薪資不可小於 3000 或大於 100000！");
            }
        }
        public static void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

    }
}