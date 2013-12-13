using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zhaopin.IbatisFactory.BLL;

namespace Zhaopin.WinForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public string strServer = string.Empty;
        public string strUid = string.Empty;
        public string strPwd = string.Empty;

        public void initData()
        {
            strServer = txtServer.Text;
            strUid = txtUid.Text;
            strPwd = txtPwd.Text;
        }




        private void btnTest_Click_1(object sender, EventArgs e)
        {
            initData();
            MainClassBLL bll = new MainClassBLL();
            DataSet ds = bll.LoadDataBase(strServer, strUid, strPwd);
            cbList.Items.Clear();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cbList.Items.Add(ds.Tables[0].Rows[i]["name"].ToString());
                }
                cbList.Items.Insert(0, "请选择");
                cbList.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("加载失败！");
            }
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            string db = cbList.Text;
            if (db.Equals("请选择") || db.Equals(""))
            {
                MessageBox.Show("请选择一个数据库!");
                return;
            }

            initData();
            string strDb = cbList.Text.Trim();
            string ConnectionString = string.Format("Data Source={0};Initial Catalog={3};User ID={1};PWD={2}", strServer, strUid, strPwd, strDb);

            ContentForm cf = new ContentForm(ConnectionString, strServer, strDb);
            cf.Show();
        }

       


    }
}
