using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zhaopin.IbatisFactory.DAL;
using System.Data;
using Zhaopin.IbatisFactory.Common;

namespace Zhaopin.IbatisFactory.BLL
{
    public class ContentFromClassBLL
    {
        ContentFormClassDAL dal = new ContentFormClassDAL();
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public DataSet GetTables(string ConnectionString)
        {
            return dal.GetTables(ConnectionString);
        }

        public FolderClass GetList()
        {
            var obj = (FolderClass)XmlLib.Deserialize(typeof(FolderClass), "Template/folder.xml");
            return obj;
        } 

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="ConnectionString"></param>
        

        public DataSet GetTableGetTableColumns(string ConnectionString, string tablename)
        {
            return dal.GetTableGetTableColumns(ConnectionString, tablename);
        } 

        public DataSet GetTableDescription(string ConnectionString, string tablename)
        { 
            return dal.GetTableDescription(ConnectionString, tablename); 
        } 
    }
}
