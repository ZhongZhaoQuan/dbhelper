using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Model
{
    public class DBHelper
    {
        //连接字符串
        public static string sqlStr = "server = .;database = baseDB ; uid = sa ; pwd = 123456";
        //数据库连接对象
        public static SqlConnection conn = null;
        //初始化
        public static void SqlConn()
        {
            if (conn==null)
            {
                conn = new SqlConnection(sqlStr);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Dispose();
                conn.Open();
            }
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sqlStr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlStr,conn);
            int res = cmd.ExecuteNonQuery();
            return res > 0;
        }
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlStr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }

        /// <summary>
        /// 获取表
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static DataTable DataTable(string sqlStr)
        {
            SqlConn();
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sqlStr, conn);
            dap.Fill(dt);
            conn.Close();
            return dt;
        }
        /// <summary>
        /// 查询返回SqlDataReader
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sqlStr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 无参数无返值存错过程
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public static bool Proc(string procName)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            int res = cmd.ExecuteNonQuery();
            return res > 0;
        }
        public static SqlDataReader ProcReader(string procName)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            return  cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public static object ProcScalar(string procName)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            object res =  cmd.ExecuteScalar();
            return res;
        }
        public static DataTable ProcTable(string procName)
        {
            SqlConn();
            SqlDataAdapter dap = new SqlDataAdapter(procName, conn);
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dap.Fill(dt);
            conn.Close();
            return dt;
        }
        /// <summary>
        /// 有参数无返回值存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool ProcParameters(string procName,SqlParameter [] parameters)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            int res = cmd.ExecuteNonQuery();
            return res > 0;
        }
        public static SqlDataReader ProcReader(string procName, SqlParameter[] parameters)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public static object ProcScalar(string procName, SqlParameter[] parameters)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            object res = cmd.ExecuteScalar();
            return res;
        }
        public static DataTable ProcTable(string procName, SqlParameter[] parameters)
        {
            SqlConn();
            SqlDataAdapter dap = new SqlDataAdapter(procName, conn);
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddRange(parameters);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}
