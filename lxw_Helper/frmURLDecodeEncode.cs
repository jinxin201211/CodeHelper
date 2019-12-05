using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lxw_Helper
{
    public partial class frmURLDecodeEncode : Form
    {
        public frmURLDecodeEncode()
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

  

            try
            {
                txtEncrypt.Text = System.Web.HttpUtility.UrlEncode(Decrypt);
              

               
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

 

            try
            {
                txtDecrypt.Text = System.Web.HttpUtility.UrlDecode(Encrypt);
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

        }
    }
}
