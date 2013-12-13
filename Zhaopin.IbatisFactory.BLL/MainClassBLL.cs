using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zhaopin.IbatisFactory.DAL;
using System.Data;
namespace Zhaopin.IbatisFactory.BLL
{
    public class MainClassBLL
    {
        MainClassDAL dal = new MainClassDAL();  
        public DataSet  LoadDataBase(string server, string uid, string pwd)
        {
            string strConnection = string.Format("Data Source={0};Initial Catalog=master;User ID={1};PWD={2}", server, uid, pwd);
            return dal.LoadDataBase(strConnection);
        }


    }
}
