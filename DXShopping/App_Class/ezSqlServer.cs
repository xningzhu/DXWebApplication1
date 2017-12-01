using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ezapp
{
    /// <summary>
    /// SQL Server ADO.Net 自定公用功能類別
    /// </summary>
    public class ezSqlServer
    {
        /// <summary>
        /// 連線名稱內部變數
        /// </summary>
        private string _ConnName = "";
        /// <summary>
        /// SqlConnection 連線物件
        /// </summary>
        public SqlConnection conn { get; set; }
        /// <summary>
        /// SqlCommand 命令
        /// </summary>
        public SqlCommand cmd { get; set; }
        /// <summary>
        /// 連線名稱變數
        /// </summary>
        public string ConnName
        {
            get { return (string.IsNullOrEmpty(_ConnName)) ? ezSession.ConnName : _ConnName; }
            set { _ConnName = value; }
        }
        /// <summary>
        /// SQL 指令
        /// </summary>
        public string CommandText
        {
            get { return cmd.CommandText; }
            set { cmd.CommandText = value; }
        }
        /// <summary>
        /// 命令模式
        /// </summary>
        public CommandType CommandType
        {
            get { return cmd.CommandType; }
            set { cmd.CommandType = value; }
        }
        /// <summary>
        /// 回傳執行後是否有記錄
        /// </summary>
        public bool HasRows
        {
            get
            {
                bool bln_hasrows = false;
                ErrorMessage = "";
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    bln_hasrows = dr.HasRows;
                    dr.Close();
                }
                catch ( Exception ex)
                {
                    ErrorMessage = ex.Message;
                }

                return bln_hasrows;
            }
        }
        /// <summary>
        /// 執行後有錯誤時的錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// ezSqlServer 建構子
        /// </summary>
        public ezSqlServer()
        {
            Connect();
        }
        /// <summary>
        /// ezSqlServer 建構子,指定自定的連線名稱
        /// </summary>
        /// <param name="sConnName"></param>
        public ezSqlServer(string sConnName)
        {
            ConnName = sConnName;
            Connect();
        }
        /// <summary>
        /// 連線
        /// </summary>
        private void Connect()
        {
            conn = new SqlConnection();
            cmd = new SqlCommand();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
            conn.Open();
            cmd.Connection = conn;
        }
        /// <summary>
        /// 關閉連線
        /// </summary>
        public void Close()
        {
            conn.Close();
        }
        /// <summary>
        /// 加入參數
        /// </summary>
        /// <param name="sParameter">參數名稱</param>
        /// <param name="oValue">參數值</param>
        /// <param name="bClear">是否先清除所有參數再加入</param>
        public void ParametersAdd(string sParameter, object oValue, bool bClear)
        {
            if (bClear) cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue(sParameter, oValue);
        }
        /// <summary>
        /// 執行 SQL 命令不回傳值
        /// </summary>
        /// <param name="bClose">是否關閉連線</param>
        public void ExecuteNonQuery(bool bClose)
        {
            ErrorMessage = "";
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                if (bClose) Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
        }
        /// <summary>
        /// 取得指定欄位的字串型態值
        /// </summary>
        /// <param name="sColName">指定欄位</param>
        /// <returns></returns>
        public string GetValueString(string sColName)
        {
            ErrorMessage = "";
            string str_value = "";
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    str_value = (dr[sColName] == null) ? "" : dr[sColName].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return str_value;
        }
        /// <summary>
        /// 取得指定欄位的整數型態值
        /// </summary>
        /// <param name="sColName">指定欄位</param>
        /// <returns></returns>
        public int GetValueInt(string sColName)
        {
            ErrorMessage = "";
            int int_value = 0;
            try
            {
                string str_value = GetValueString(sColName);
                if (string.IsNullOrEmpty(str_value)) str_value = "0";
                int_value = int.Parse(str_value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return int_value;
        }
        /// <summary>
        /// 取得指定欄位的數字型態值
        /// </summary>
        /// <param name="sColName">指定欄位</param>
        /// <returns></returns>
        public decimal GetValueDecimal(string sColName)
        {
            ErrorMessage = "";
            decimal dec_value = 0;
            try
            {
                string str_value = GetValueString(sColName);
                if (string.IsNullOrEmpty(str_value)) str_value = "0";
                dec_value = decimal.Parse(str_value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return dec_value;
        }
        /// <summary>
        /// 取得指定欄位的日期型態值
        /// </summary>
        /// <param name="sColName">指定欄位</param>
        /// <returns></returns>
        public DateTime GetValueDateTime(string sColName)
        {
            ErrorMessage = "";
            DateTime dtm_value = DateTime.MinValue;
            try
            {
                string str_value = GetValueString(sColName);
                if (string.IsNullOrEmpty(str_value))
                    dtm_value = DateTime.MinValue;
                else
                    dtm_value = DateTime.Parse(str_value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return dtm_value;
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
        /// <param name="bClose">執行後關閉資料庫</param>
        /// <returns></returns>
        public DataTable GetDataTable(bool bClose)
        {
            ErrorMessage = "";
            DataSet dsReturn = new DataSet();
            DataTable dtReturn = new DataTable();
            try
            {
                dsReturn = GetDataSet(bClose);
                dtReturn = dsReturn.Tables[0];
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return dtReturn;
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
        /// <param name="bClose">執行後關閉資料庫</param>
        /// <returns></returns>
        public DataSet GetDataSet(bool bClose)
        {
            ErrorMessage = "";
            DataSet dsReturn = new DataSet();
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dsReturn);
                adapter.Dispose();
            }
            catch (SqlException ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            if (bClose) Close();
            return dsReturn;
        }

        /// <summary>
        /// 設定 StoreProcedure
        /// </summary>
        /// <param name="sProcedureName">StoreProcedure 名稱</param>
        public void StoreProcedure(string sProcedureName)
        {
            this.CommandType = CommandType.StoredProcedure;
            this.CommandText = sProcedureName;
        }
    }
}