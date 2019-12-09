using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lxw_Helper.Common;
using System.Configuration;

namespace lxw_Helper
{
    public partial class frmParCfg : Form
    {
        public frmParCfg()
        {
            InitializeComponent();
        }
        public string ConnectionString = "";
        public bool isFirstRun = true;

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                txtPwd.Focus();
                return;
            }

            //Data Source=11.101.2.36:1521/orcl

            DataBaseHelper.IP = this.txtIP.Text;
            DataBaseHelper.Port = this.txtport.Text;
            DataBaseHelper.Instance = this.txtInstanName.Text;
            DataBaseHelper.User = this.txtName.Text;
            DataBaseHelper.Pwd = this.txtPwd.Text;
            if (this.txtOracle.Checked)
            {
                DataBaseHelper.DbType = 0;
            }
            else if (this.txtMSSQL.Checked)
            {
                DataBaseHelper.DbType = 1;
            }
            else
            {
                DataBaseHelper.DbType = 2;
            }
            DataBaseHelper.ChangeDataBase();
            //if (txtDataBase.Text == "1")
            //{
            //    ConnectionString = string.Format($"Data Source={this.txtIP.Text}:{this.txtport.Text}/{this.txtInstanName.Text}; user={this.txtName.Text}; password={this.txtPwd.Text}; persist security info=false;");
            //    DataBaseHelper.SelectDataBase("System.Data.OracleClient", ConnectionString);
            //    DataBaseHelper.GetTableSql = @"SELECT A.TABLE_NAME, B.COMMENTS
            //                  FROM USER_TABLES A
            //                  LEFT JOIN USER_TAB_COMMENTS B
            //                  ON A.TABLE_NAME = B.TABLE_NAME ORDER BY TABLE_NAME";
            //}
            ////MSSql
            //else if (txtDataBase.Text == "2")
            //{
            //    ConnectionString = string.Format($"Data Source={this.txtIP.Text};Initial Catalog={this.txtInstanName.Text};Persist Security Info=True;User ID={this.txtName.Text};Password={this.txtPwd.Text};");
            //    DataBaseHelper.SelectDataBase("System.Data.SqlClient", ConnectionString);
            //    DataBaseHelper.GetTableSql = "SELECT [NAME] TABLE_NAME FROM SYS.TABLES";
            //}
            ////MySql
            //else if (txtDataBase.Text == "3")
            //{
            //    ConnectionString = string.Format("");
            //    DataBaseHelper.SelectDataBase("", ConnectionString);
            //}

            txtConn.Text = "";
            txtConn.Text = DataBaseHelper.ConnectionString;

            //Data Source=11.101.2.36:1521/orcl
            //ConnectionString = string.Format("Data Source={0}:{4}/{1};user id={2}; password={3};persist security info=false;Min Pool Size=10;Max Pool Size=100;Pooling=true;",
            //    txtIP.Text,
            //    txtInstanName.Text,
            //    txtName.Text,
            //    txtPwd.Text,
            //    txtport.Text
            //    );
            //DbHelperOra.ConnectionString = ConnectionString;
            //string msg = "";

            string msg = string.Empty;
            if (DataBaseHelper.TestConnection(out msg))
            //if (DbHelperOra.TestConnection(out msg))
            {

                Config.IP = txtIP.Text;
                Config.Port = txtport.Text;
                Config.Name = txtName.Text;
                Config.Pwd = txtPwd.Text;
                Config.Instance = txtInstanName.Text;

                MessageBox.Show("连接成功");
                if (isFirstRun)
                {
                    this.Hide();
                    frmMain frm = new frmMain();
                    frm.Show();
                }
                else
                {
                    // this.Close();
                }
            }
            else
            {
                MessageBox.Show("连接失败:" + msg);
                this.Hide();
                frmMain frm = new frmMain();
                frm.Show();
            }

        }

        private void frmParCfg_Load(object sender, EventArgs e)
        {
            if (isFirstRun)
            {
                WindowAnimate.AnimateWindow(this.Handle, 200, WindowAnimate.AW_VER_POSITIVE | WindowAnimate.AW_SLIDE);
            }
            else
            {
                btnCancel.Visible = false;
            }
            //读取配置文件中的参数
            string IP = ConfigurationManager.AppSettings["IP"] ?? "";
            string Port = ConfigurationManager.AppSettings["Port"] ?? "";
            string Name = ConfigurationManager.AppSettings["Name"] ?? "";
            string Pwd = ConfigurationManager.AppSettings["Pwd"] ?? "";
            string Instance = ConfigurationManager.AppSettings["Instance"] ?? "";

            Config.IP = IP;
            Config.Port = Port;
            Config.Name = Name;
            Config.Pwd = Pwd;
            Config.Instance = Instance;

            if (!string.IsNullOrEmpty(IP))
            {
                txtIP.Text = IP;
            }

            if (!string.IsNullOrEmpty(Port))
            {
                txtport.Text = Port;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                txtName.Text = Name;
            }

            if (!string.IsNullOrEmpty(Pwd))
            {
                txtPwd.Text = Pwd;
            }

            if (!string.IsNullOrEmpty(Instance))
            {
                txtInstanName.Text = Instance;
            }


        }

        private void frmParCfg_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isFirstRun)
            {
                WindowAnimate.AnimateWindow(this.Handle, 500, WindowAnimate.AW_VER_POSITIVE | WindowAnimate.AW_SLIDE);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain frm = new frmMain();
            frm.Show();
        }

        private void txtOracle_Click(object sender, EventArgs e)
        {
            this.txtIP.Text = "118.112.188.39";
            this.txtport.Text = "2001";
            this.txtInstanName.Text = "orcl";
            this.txtName.Text = "cdles";
            this.txtPwd.Text = "scxd_2018";
        }

        private void txtMSSQL_Click(object sender, EventArgs e)
        {
            this.txtIP.Text = "118.24.64.59";
            this.txtport.Text = "";
            this.txtInstanName.Text = "MovieWarehouse";
            this.txtName.Text = "sa";
            this.txtPwd.Text = "jinxin20170630.";
        }

        private void txtMySQL_Click(object sender, EventArgs e)
        {
            this.txtIP.Text = "140.143.82.131";
            this.txtport.Text = "10014";
            this.txtInstanName.Text = "jinx_manage";
            this.txtName.Text = "root";
            this.txtPwd.Text = "jinxin20170630";
        }
    }
}
