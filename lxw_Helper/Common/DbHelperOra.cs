using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;


namespace lxw_Helper.Common
{
    public class DbHelperOra
    {

        public static string ConnectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public DbHelperOra(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLStr">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLStr)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLStr, connection);
                    command.Fill(ds, "ds");
                    connection.Close();
                }
                catch (OracleException ex)
                {
                    connection.Close();

                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnection(out string msg)
        {
            bool R = false;
            msg = "";
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        R = true;
                    }
                    connection.Close();
                }
                catch (OracleException ex)
                {
                    connection.Close();
                    msg = ex.Message;
                }
            }
            return R;
        }

    }
}
