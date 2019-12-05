using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lxw_Helper.Common;

namespace lxw_Helper
{
    public partial class frmSQLWindow : Form
    {
        public frmSQLWindow()
        {
            InitializeComponent();
        }

        public string strSQL = "";

        private void btnExc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCMD.Text))
            {
                txtCMD.Focus();
            }

            try
            {
                dgvInfo.DataSource = null;
                dgvInfo.DataSource = DbHelperOra.Query(txtCMD.Text).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void frmSQLWindow_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strSQL))
            {
                txtCMD.Text = strSQL;

                btnExc_Click(null, null);
            }
        }
    }
}
