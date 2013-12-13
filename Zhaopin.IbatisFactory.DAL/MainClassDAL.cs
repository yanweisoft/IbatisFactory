using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zhaopin.DataAccess;
using System.Data;
using System.Data.SqlClient;
namespace Zhaopin.IbatisFactory.DAL
{
    public class MainClassDAL
    {


        #region
        string StrTestSql = @" 
 SELECT  name FROM sys.sysdatabases  ORDER
 BY  crdate ASC";
        #endregion
        public bool TestSql(string ConnectionString)
        {

            bool result = false;
            try
            {
                DataSet ds = SqlHelper.GetTable(ConnectionString, StrTestSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch
            {
                result = false;
            }

            return result;
        }

        public DataSet  LoadDataBase(string ConnectionString)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.GetTable(ConnectionString, StrTestSql);
            return ds; 
        }

    }
}
