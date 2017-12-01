using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ezapp
{
    /// <summary>
    /// SQL Server ADO 簡易操作物件
    /// </summary>
    public class ezSqlClient
    {
        //設定屬性用私有變數
        private SqlConnection _conn;
        private SqlCommand _cmd;
        private string _ConnName = "dbconn";
        private string _ErrorMessage = "";

        /// <summary>
        /// SqlClient 建構子
        /// </summary>
        public ezSqlClient()
        {
            SqlClientInit("dbconn");
        }

        /// <summary>
        /// SqlClient 建構子
        /// </summary>
        /// <param name="sCommName">連線名稱</param>
        public ezSqlClient(string sCommName)
        {
            SqlClientInit(sCommName);
        }

        /// <summary>
        /// 初始化 SqlClient
        /// </summary>
        /// <param name="sCommName">連線名稱</param>
        private void SqlClientInit(string sCommName)
        {
            ConnName = sCommName;
            _conn = new SqlConnection();
            _cmd = new SqlCommand();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings[ConnName].ConnectionString.ToString();
            cmd.Connection = conn;
            Open();
        }


        /// <summary>
        /// 資料庫連線物件
        /// </summary>
        public SqlConnection conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        /// <summary>
        /// SQL 命令物件
        /// </summary>
        public SqlCommand cmd
        {
            get { return _cmd; }
            set { _cmd = value; }
        }

        /// <summary>
        /// 連線字串 KEY Name 屬性
        /// </summary>
        public string ConnName
        {
            get { return _ConnName; }
            set { _ConnName = value; }
        }

        /// <summary>
        /// 錯誤訊息屬性
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        /// <summary>
        /// 命令類型
        /// </summary>
        public CommandType CmdCommandType
        {
            get { return cmd.CommandType; }
            set { cmd.CommandType = value; }
        }

        /// <summary>
        /// SQL 命令語句
        /// </summary>
        public string CommandText
        {
            get { return cmd.CommandText; }
            set { cmd.CommandText = value; }
        }

        /// <summary>
        /// 加入 SQL 命令參數，不清除所有參數
        /// </summary>
        /// <param name="ParmName">參數名稱</param>
        /// <param name="ParmValue">參數值</param>
        public void ParameterAdd(string ParmName, object ParmValue)
        {
            ParameterAdd(ParmName, ParmValue, false);
        }

        /// <summary>
        /// 加入 SQL 命令參數
        /// </summary>
        /// <param name="ParmName">參數名稱</param>
        /// <param name="ParmValue">參數值</param>
        /// <param name="ClearParm">清除所有參數後再加入</param>
        public void ParameterAdd(string ParmName, object ParmValue, bool ClearParm)
        {
            if (ClearParm) cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue(ParmName, ParmValue);
        }

        /// <summary>
        /// 檢查是否有記錄
        /// </summary>
        public bool HasRows
        {
            get
            {
                ErrorMessage = "";
                bool blnReturn = false;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    if (!Open()) return false;
                }

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    blnReturn = dr.HasRows;
                    dr.Close();
                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message.ToString();
                }
                return blnReturn;
            }
        }

        /// <summary>
        /// 開啟資料庫連線
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            ErrorMessage = "";
            bool blnReturn = true;
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Connection = conn;
            }
            catch (SqlException ex)
            {
                ErrorMessage = ex.Message.ToString();
                blnReturn = false;
            }
            return blnReturn;
        }

        /// <summary>
        /// 關閉資料庫連線
        /// </summary>
        public void Close()
        {
            ErrorMessage = "";
            CmdCommandType = CommandType.Text;
            conn.Close();
        }

        /// <summary>
        /// 設定 StoreProcedure
        /// </summary>
        /// <param name="ProcName">StoreProcedure 名稱</param>
        public void SetStoreProcedure(string ProcName)
        {
            CmdCommandType = CommandType.StoredProcedure;
            CommandText = ProcName;
        }

        /// <summary>
        /// 執行 SQL 命令
        /// </summary>
        /// <returns></returns>
        public bool ExecuteNonQuery()
        {
            return ExecuteNonQuery(false);
        }

        /// <summary>
        /// 執行 SQL 命令,並自動關閉資料庫連線
        /// </summary>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(bool CloseDB)
        {
            ErrorMessage = "";
            bool blnReturn = true;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    if (!Open()) blnReturn = false;
                }
                if (blnReturn) cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ErrorMessage = ex.Message.ToString();
                blnReturn = false;
            }
            if (CloseDB) Close();
            return blnReturn;
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(文字型態),並自動關閉資料庫連線
        /// </summary>
        /// <param name="ColumnName">欄位名稱</param>
        /// <returns></returns>
        public string GetSelectString(string ColumnName)
        {
            return GetSelectString(ColumnName, true);
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(文字型態)
        /// </summary>
        /// <param name="ColumnName">欄位名稱</param>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public string GetSelectString(string ColumnName, bool CloseDB)
        {
            ErrorMessage = "";
            string str_value = "";
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    if (!Open()) return "";
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    str_value = dr[ColumnName].ToString();
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                str_value = "";
                ErrorMessage = ex.Message.ToString();
            }
            if (CloseDB) Close();
            return str_value;
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(數字型態),並自動關閉資料庫連線
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public decimal GetSelectDecimal(string ColumnName)
        {
            return GetSelectDecimal(ColumnName, true);
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(數字型態)
        /// </summary>
        /// <param name="ColumnName">欄位名稱</param>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public decimal GetSelectDecimal(string ColumnName, bool CloseDB)
        {
            ErrorMessage = "";
            string str_value = GetSelectString(ColumnName, CloseDB);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            decimal dec_value = decimal.Parse(str_value);
            return dec_value;
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(整數型態),並自動關閉資料庫連線
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public int GetSelectInt(string ColumnName)
        {
            return GetSelectInt(ColumnName, true);
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(整數型態)
        /// </summary>
        /// <param name="ColumnName">欄位名稱</param>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public int GetSelectInt(string ColumnName, bool CloseDB)
        {
            ErrorMessage = "";
            string str_value = GetSelectString(ColumnName, CloseDB);
            if (string.IsNullOrEmpty(str_value)) str_value = "0";
            int int_value = int.Parse(str_value);
            return int_value;
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(日期型態),並自動關閉資料庫連線
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public DateTime GetSelectDateTime(string ColumnName)
        {
            return GetSelectDateTime(ColumnName, true);
        }

        /// <summary>
        /// 執行 SQL 指令並取出第一筆所指定欄位的值(日期型態)
        /// </summary>
        /// <param name="ColumnName">欄位名稱</param>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public DateTime GetSelectDateTime(string ColumnName, bool CloseDB)
        {
            ErrorMessage = "";
            string str_value = GetSelectString(ColumnName, CloseDB);
            if (string.IsNullOrEmpty(str_value)) str_value = DateTime.MinValue.ToString();
            DateTime dtm_value = DateTime.Parse(str_value);
            return dtm_value;
        }

        /// <summary>
        /// 執行 SQL 指令並取回 DataSet,並自動關閉資料庫連線
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            return GetDataSet(true);
        }

        /// <summary>
        /// 執行 SQL 指令並取回 DataSet
        /// </summary>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public DataSet GetDataSet(bool CloseDB)
        {
            ErrorMessage = "";
            DataSet ds_dataset = new DataSet();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    if (!Open()) return ds_dataset;
                }
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds_dataset);
                    adapter.Dispose();
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            if (CloseDB) Close();
            return ds_dataset;
        }

        /// <summary>
        /// 執行 SQL 指令並取回 DataTable,並自動關閉資料庫連線
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            return GetDataTable(true);
        }

        /// <summary>
        /// 執行 SQL 指令並取回 DataTable
        /// </summary>
        /// <param name="CloseDB">執行後關閉資料庫</param>
        /// <returns></returns>
        public DataTable GetDataTable(bool CloseDB)
        {
            DataSet ds_dataset = new DataSet();
            DataTable dt_datatable = new DataTable();
            ds_dataset = GetDataSet(CloseDB);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                dt_datatable = ds_dataset.Tables[0];
            }
            return dt_datatable;
        }
    }
}