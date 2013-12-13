using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zhaopin.DataAccess;
using System.Data;
using System.Data.SqlClient;
namespace Zhaopin.IbatisFactory.DAL
{


    public class ContentFormClassDAL
    {


        #region  SQL
        string SQLAllTablesSql = @"select name from sysobjects where xtype='U' ";

        string SQLTableColumns = @"select name from syscolumns where id=(select max(id)
from sysobjects where xtype='u' and name=@name) 
";

        string SQLTableDescription = @"SELECT     CASE WHEN EXISTS
         (SELECT     1
           FROM          sysobjects
            WHERE      xtype = 'PK' AND parent_obj = a.id AND name IN
             (SELECT     name
              FROM          sysindexes
                  WHERE      indid IN
              (SELECT     indid
               FROM          sysindexkeys
              WHERE      id = a.id AND colid = a.colid))) THEN '1' ELSE '0' END AS 'key', CASE WHEN COLUMNPROPERTY(a.id, a.name,
                      'IsIdentity') = 1 THEN '1' ELSE '0' END AS 'identity', a.name AS ColName, c.name AS TypeName, a.length AS 'byte', COLUMNPROPERTY(a.id, a.name,
                      'PRECISION') AS 'length', a.xscale, a.isnullable, ISNULL(e.text, '') AS 'default', ISNULL(p.value, '') AS 'comment'
FROM         sys.syscolumns AS a INNER JOIN
                      sys.sysobjects AS b ON a.id = b.id INNER JOIN
                      sys.systypes AS c ON a.xtype = c.xtype LEFT OUTER JOIN
                      sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                      sys.extended_properties AS p ON a.id = p.major_id AND a.colid = p.minor_id
WHERE     (b.name = @name) AND (c.status <> '1')";

        #endregion


        public DataSet GetTables(string ConnectionString)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.GetTable(ConnectionString, SQLAllTablesSql);
            return ds;
        } 

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public DataSet GetTableGetTableColumns(string ConnectionString, string tablename)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.GetTableColumns(ConnectionString, SQLTableColumns, tablename);

            return ds;
        }


        public DataSet GetTableDescription(string ConnectionString,string tablename)
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.GetTableColumns(ConnectionString, SQLTableDescription, tablename); 
            return ds;
        }







    }
}
