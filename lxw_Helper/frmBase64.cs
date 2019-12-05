using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace lxw_Helper
{
    public partial class frmBase64 : Form
    {
        public frmBase64()
        {
            InitializeComponent();
        }

        private void btnPhoto2base64_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先选择照片！");
                return;
            }

            try
            {


                //Image img = this.pictureBox1.Image;
                //Bitmap map = new Bitmap(img);
                //Bitmap map = new Bitmap(img);
                //Image img = map;


                Bitmap bmp = new Bitmap(this.pictureBox1.Image);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                this.richTextBox1.Text = Convert.ToBase64String(arr);
                txtLen.Text = richTextBox1.Text.Length.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("照片转base64失败:" + ex.Message);
            }

        }

        private void btnbase642Photo_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Trim() == "")
            {
                MessageBox.Show("无base64字符串！");
                return;
            }
            try
            {
                txtPhotoName.Text = "";
                string base64 = this.richTextBox1.Text;

                base64 = base64.Replace(" ", "+");

                byte[] imageBytes = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

                this.pictureBox1.Image = image;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            //dialog.Filter = "所有文件(*.*)|*.*";
            dialog.Filter = "图像文件(*.jpg;*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && dialog.FileName != "")
            {
                pictureBox1.ImageLocation = dialog.FileName;
                txtPhotoName.Text = dialog.FileName;
            }
            else
            {
                txtPhotoName.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            txtPhotoName.Text = "";
        }

        private void btnSavePhoto_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("无可保存照片！");
                return;
            }

            //文件名  
            string curFileName = "";
            //图像对象  
            System.Drawing.Bitmap curBitmap = new Bitmap(this.pictureBox1.Image);



            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter =
                "BMP文件 (*.bmp) | *.bmp|" +
                "Gif文件 (*.gif) | *.gif|" +
                "JPEG文件 (*.jpg) | *.jpg|" +
                "PNG文件 (*.png) | *.png";
            saveDlg.ShowHelp = true;
            if (saveDlg.ShowDialog() == DialogResult.OK && saveDlg.FileName != "")
            {
                string fileName = saveDlg.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnSaveTXT_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text.Trim() == "")
            {
                MessageBox.Show("无可保存信息！");
                return;

            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {

                // Save the contents of the RichTextBox into the file.
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("文件已成功保存");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string base64 = this.richTextBox1.Text;

            base64 = base64.ToLower();

            richTextBox1.Text = base64;
        }

        private void btnGetLength_Click(object sender, EventArgs e)
        {
            txtLen.Text = richTextBox1.Text.Length.ToString();
        }
    }
}
