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
    public partial class frmDES : Form
    {
        public frmDES()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {

            string Decrypt = txtDecrypt.Text.Trim();
            if (string.IsNullOrEmpty(Decrypt))
            {
                txtDecrypt.Focus();
                return;
            }

            string sKey = txtKey.Text.Trim();
            if (string.IsNullOrEmpty(sKey))
            {
                txtKey.Focus();
                return;
            }

            try
            {
                txtEncrypt.Text = DESEncrypt.Encrypt(Decrypt, sKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {

            string Encrypt = txtEncrypt.Text.Trim();
            if (string.IsNullOrEmpty(Encrypt))
            {
                txtEncrypt.Focus();
                return;

            }

            string sKey = txtKey.Text.Trim();
            if (string.IsNullOrEmpty(sKey))
            {
                txtKey.Focus();
                return;
            }

            try
            {
                txtDecrypt.Text = DESEncrypt.Decrypt(Encrypt, sKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDecrypt.Text = "";
            txtEncrypt.Text = "";
            txtKey.Text = "";
        }
    }
}
