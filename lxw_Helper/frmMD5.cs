using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Security;

namespace lxw_Helper
{
    public partial class frmMD5 : Form
    {
        public frmMD5()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDecrypt.Text = "";
            txtEncrypt.Text = "";
        }

        private void btnEncrypt16_Click(object sender, EventArgs e)
        {

            string Decrypt = txtDecrypt.Text.Trim();
            if (string.IsNullOrEmpty(Decrypt))
            {
                txtDecrypt.Focus();
                return;
            }

            try
            {
                txtEncrypt.Text = MD5.GetMD5(Decrypt, 16);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEncrypt32_Click(object sender, EventArgs e)
        {
            string Decrypt = txtDecrypt.Text.Trim();
            if (string.IsNullOrEmpty(Decrypt))
            {
                txtDecrypt.Focus();
                return;
            }

            try
            {
                txtEncrypt.Text = MD5.GetMD5(Decrypt, 32);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
