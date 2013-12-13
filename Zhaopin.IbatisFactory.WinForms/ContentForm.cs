using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zhaopin.IbatisFactory.BLL;
using Zhaopin.IbatisFactory.Common;
using System.Diagnostics;
namespace Zhaopin.WinForms
{
    public partial class ContentForm : Form
    {
        public ContentForm()
        {
            InitializeComponent();
        }
        public string ConnectionString;
        public string SelectPath;
        public string ServerName;
        public string DbName;

        public ContentForm(string strContent, string strServer, string strDb)
        {
            InitializeComponent();
            ConnectionString = strContent;
            ServerName = strServer;
            DbName = strDb;
            LoadTables();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadTables()
        {
            lbldb.Text = DbName;
            lblserver.Text = ServerName;
            ContentFromClassBLL bll = new ContentFromClassBLL();
            DataSet ds = bll.GetTables(ConnectionString);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                listBox1.Items.Clear();
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        listBox1.Items.Add(ds.Tables[0].Rows[i]["name"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            btnok.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;

            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择输出目录";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectPath = folderBrowserDialog1.SelectedPath;
            }
            txtPath.Text = SelectPath;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnok.Enabled = true;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            string NewPath = txtPath.Text.Trim();
            string tableName = string.Empty;
            string StrNameSpace = txtNameSpace.Text.Trim();


            foreach (var item in listBox1.SelectedItems)
            {
                tableName += item.ToString() + ",";
            }
            if (tableName != "")
            {
                tableName = tableName.Substring(0, tableName.ToString().Length - 1);
            }
            string[] tables = tableName.Split(',');
            ContentFromClassBLL bll = new ContentFromClassBLL();
            FolderClass fc = bll.GetList();
            foreach (var item in fc.ItemList)
            {
                #region 创建目录
                string strTemplate = item.Path;
                string strListTemplate = item.ItemTemplate;
                for (int i = 0; i < tables.Length; i++)
                {
                    string cell = tables[0];
                    string strPath = NewPath + "\\" + string.Format(item.NewPath, cell);    //生成的新目录
                    ContentFromClassBLL bllcf = new ContentFromClassBLL();
                    DataSet ds = bllcf.GetTableGetTableColumns(ConnectionString, cell);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sbColunms1 = new StringBuilder();// col1,col2,col3
                        StringBuilder sbColunms2 = new StringBuilder(); //#col1#,#col2#,#col3#
                        StringBuilder sbColunms3 = new StringBuilder();// col1=#col1#,col2=#col2#,col3=#col3#
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            List<string> list = new List<string>();
                            sbColunms1.Append(ds.Tables[0].Rows[j]["Name"].ToString() + ",");
                            sbColunms2.AppendFormat("#{0}#,", ds.Tables[0].Rows[j]["Name"].ToString());
                            sbColunms3.AppendFormat("[{0}]=#{0}#,", ds.Tables[0].Rows[j]["Name"].ToString());
                        }
                        StringBuilder sblist = new StringBuilder();
                        string col1 = sbColunms1.ToString().Substring(0, sbColunms1.ToString().Length - 1);
                        string col2 = sbColunms2.ToString().Substring(0, sbColunms2.ToString().Length - 1);
                        string col3 = sbColunms3.ToString().Substring(0, sbColunms3.ToString().Length - 1);
                        string collist = string.Empty;
                        if (!string.IsNullOrEmpty(strListTemplate))
                        {
                            string strItem = FileHelper.GetFileContent(strListTemplate);
                            DataSet dscolumns = bll.GetTableDescription(ConnectionString, cell);
                            if (dscolumns != null && dscolumns.Tables[0].Rows.Count > 0)
                            {
                                for (int k = 0; k < dscolumns.Tables[0].Rows.Count; k++)
                                {
                                    string name = dscolumns.Tables[0].Rows[k]["ColName"].ToString();
                                    string des = dscolumns.Tables[0].Rows[k]["comment"].ToString();
                                    string type = Common.GetDataType(dscolumns.Tables[0].Rows[k]["TypeName"].ToString());
                                    sblist.AppendFormat(strItem, des, type, name);
                                }
                            }
                        }

                        #region  读取配置文件
                        string filename = strPath;
                        string StrContent = FileHelper.GetFileContent(strTemplate);
                        if (StrContent != "")
                        {
                            string ab = sblist.ToString();

                            StrContent = StrContent.Replace("$tablename$", tableName).Replace("$colunms1$", col1).Replace("$colunms2$", col2).Replace("$colunms3$", col3).Replace("$namespace$", StrNameSpace).Replace("$tablename2$", tableName.ToString().ToLower()).Replace("$colnumslist$", sblist.ToString());
                        }
                        FileHelper.CreateFile(strPath, StrContent);

                        #endregion

                    }
                }

                #endregion
            }
            //MessageBox.Show("导出成功！");
            var result = MessageBox.Show("导出成功，现在是否打开文件夹？", "友情提示", MessageBoxButtons.YesNo);
            if (result ==DialogResult .Yes)
            {
                string strPath = NewPath;
                Process.Start(strPath);
            }

        }

     
    }
}
