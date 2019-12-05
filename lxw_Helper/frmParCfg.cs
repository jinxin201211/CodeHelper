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
            ConnectionString = string.Format("Data Source={0}:{4}/{1};user id={2}; password={3};persist security info=false;Min Pool Size=10;Max Pool Size=100;Pooling=true;",
                txtIP.Text,
                txtInstanName.Text,
                txtName.Text,
                txtPwd.Text,
                txtport.Text
                );

            txtConn.Text = "";
            txtConn.Text = ConnectionString;


            DbHelperOra.ConnectionString = ConnectionString;
            string msg = "";
            if (DbHelperOra.TestConnection(out msg))
            {

                Config.IP = txtIP.Text;
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
            string Name = ConfigurationManager.AppSettings["Name"] ?? "";
            string Pwd = ConfigurationManager.AppSettings["Pwd"] ?? "";
            string Instance = ConfigurationManager.AppSettings["Instance"] ?? "";

            Config.IP = IP;
            Config.Name = Name;
            Config.Pwd = Pwd;
            Config.Instance = Instance;

            if (!string.IsNullOrEmpty(IP))
            {
                txtIP.Text = IP;
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
    }
}
