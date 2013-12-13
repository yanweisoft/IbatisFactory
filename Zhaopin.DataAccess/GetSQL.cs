using System.Data.SqlClient;
namespace Zhaopin.DataAccess
{
    public class GetSQL
    {
        /// <summary>
        /// 创建连接字符串
        /// </summary>
        /// <param name="StringConnection"></param>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection(string StringConnection)
        {
            SqlConnection scn = new SqlConnection(StringConnection);
            return scn;
        }
    }
}
