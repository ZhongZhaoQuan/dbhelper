using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;//数据库连接字符串 引用

namespace DAL
{
    public class DBHelper
    {
        //数据库连接
        //public static string sqlstr = "server=.;database=QingBlog;uid=sa;pwd=123456;";
        public static string sqlstr = ConfigurationManager.ConnectionStrings["connsql"].ConnectionString;
        //数据库连接对象
        public static SqlConnection sqlconn = null;
        //初始化连接对象
        public static void SqlConn()
        {

            if (sqlconn == null)
            {
                sqlconn = new SqlConnection(sqlstr);
            }

            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }

            if (sqlconn.State == ConnectionState.Broken)
            {
                sqlconn.Close();
                sqlconn.Dispose();
                sqlconn.Open();
            }
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sqlstr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlstr, sqlconn);
            int result = cmd.ExecuteNonQuery();
            sqlconn.Close();
            return result > 0;
        }
        /// <summary>
        /// 获取表
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static DataTable DataTable(string sqlstr)
        {
            SqlConn();
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sqlstr, sqlconn);
            dap.Fill(dt);
            sqlconn.Close();
            return dt;
        }
        /// <summary>
        /// 查询返回SqlDataReader
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sqlstr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlstr, sqlconn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static object Execute(string sqlstr)
        {
            SqlConn();
            SqlCommand cmd = new SqlCommand(sqlstr, sqlconn);
            object result = cmd.ExecuteScalar();
            sqlconn.Close();
            return result;
        }
    }
}
