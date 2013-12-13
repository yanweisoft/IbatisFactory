using System.Data;
using System.Data.SqlClient;

namespace Zhaopin.DataAccess
{
    public class SqlHelper
    {
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static DataSet GetTable(string ConnectionString, string SqlString)
        {
            SqlConnection scn = GetSQL.GetSqlConnection(ConnectionString);
            scn.Open();
            SqlCommand scm = new SqlCommand(SqlString, scn);
            SqlDataAdapter ada = new SqlDataAdapter(scm);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            scn.Close();

            return ds;
        }


        /// <summary>
        /// 根根据表名获取数据
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlString"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static DataSet GetTableColumns(string ConnectionString, string SqlString, string tablename)
        {
            SqlConnection scn = GetSQL.GetSqlConnection(ConnectionString);
            scn.Open();
            SqlCommand scm = new SqlCommand(SqlString, scn);
            SqlDataAdapter ada = new SqlDataAdapter(scm);

            SqlParameter sp = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            sp.Value = tablename;
            scm.Parameters.Add(sp);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            scn.Close();
            return ds;
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static object GetObject(string ConnectionString, string SqlString)
        {
            SqlConnection scn = GetSQL.GetSqlConnection(ConnectionString);
            scn.Open();
            SqlCommand scm = new SqlCommand(SqlString, scn);
            object oo = scm.ExecuteScalar();
            scn.Close();
            return oo;
        }
    }
}
