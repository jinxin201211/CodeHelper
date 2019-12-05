namespace lxw_Helper
{
    partial class frmBase64
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSelectPhoto = new System.Windows.Forms.Button();
            this.btnPhoto2base64 = new System.Windows.Forms.Button();
            this.btnbase642Photo = new System.Windows.Forms.Button();
            this.txtPhotoName = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClear2 = new System.Windows.Forms.Button();
            this.btnSavePhoto = new System.Windows.Forms.Button();
            this.btnSaveTXT = new System.Windows.Forms.Button();
            this.txtLen = new System.Windows.Forms.TextBox();
            this.btnGetLength = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(540, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(400, 400);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // btnSelectPhoto
            // 
            this.btnSelectPhoto.Location = new System.Drawing.Point(12, 445);
            this.btnSelectPhoto.Name = "btnSelectPhoto";
            this.btnSelectPhoto.Size = new System.Drawing.Size(75, 32);
            this.btnSelectPhoto.TabIndex = 2;
            this.btnSelectPhoto.Text = "选择图片";
            this.btnSelectPhoto.UseVisualStyleBackColor = true;
            this.btnSelectPhoto.Click += new System.EventHandler(this.btnSelectPhoto_Click);
            // 
            // btnPhoto2base64
            // 
            this.btnPhoto2base64.Location = new System.Drawing.Point(416, 166);
            this.btnPhoto2base64.Name = "btnPhoto2base64";
            this.btnPhoto2base64.Size = new System.Drawing.Size(119, 32);
            this.btnPhoto2base64.TabIndex = 3;
            this.btnPhoto2base64.Text = "照片转base64>>";
            this.btnPhoto2base64.UseVisualStyleBackColor = true;
            this.btnPhoto2base64.Click += new System.EventHandler(this.btnPhoto2base64_Click);
            // 
            // btnbase642Photo
            // 
            this.btnbase642Photo.Location = new System.Drawing.Point(416, 204);
            this.btnbase642Photo.Name = "btnbase642Photo";
            this.btnbase642Photo.Size = new System.Drawing.Size(119, 32);
            this.btnbase642Photo.TabIndex = 5;
            this.btnbase642Photo.Text = "<<base64转照片";
            this.btnbase642Photo.UseVisualStyleBackColor = true;
            this.btnbase642Photo.Click += new System.EventHandler(this.btnbase642Photo_Click);
            // 
            // txtPhotoName
            // 
            this.txtPhotoName.Location = new System.Drawing.Point(12, 418);
            this.txtPhotoName.Name = "txtPhotoName";
            this.txtPhotoName.ReadOnly = true;
            this.txtPhotoName.Size = new System.Drawing.Size(400, 21);
            this.txtPhotoName.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(868, 418);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClear2
            // 
            this.btnClear2.Location = new System.Drawing.Point(337, 446);
            this.btnClear2.Name = "btnClear2";
            this.btnClear2.Size = new System.Drawing.Size(75, 23);
            this.btnClear2.TabIndex = 8;
            this.btnClear2.Text = "清空";
            this.btnClear2.UseVisualStyleBackColor = true;
            this.btnClear2.Click += new System.EventHandler(this.btnClear2_Click);
            // 
            // btnSavePhoto
            // 
            this.btnSavePhoto.Location = new System.Drawing.Point(172, 445);
            this.btnSavePhoto.Name = "btnSavePhoto";
            this.btnSavePhoto.Size = new System.Drawing.Size(75, 32);
            this.btnSavePhoto.TabIndex = 9;
            this.btnSavePhoto.Text = "保存图片";
            this.btnSavePhoto.UseVisualStyleBackColor = true;
            this.btnSavePhoto.Click += new System.EventHandler(this.btnSavePhoto_Click);
            // 
            // btnSaveTXT
            // 
            this.btnSaveTXT.Location = new System.Drawing.Point(671, 418);
            this.btnSaveTXT.Name = "btnSaveTXT";
            this.btnSaveTXT.Size = new System.Drawing.Size(75, 32);
            this.btnSaveTXT.TabIndex = 10;
            this.btnSaveTXT.Text = "保存文件";
            this.btnSaveTXT.UseVisualStyleBackColor = true;
            this.btnSaveTXT.Click += new System.EventHandler(this.btnSaveTXT_Click);
            // 
            // txtLen
            // 
            this.txtLen.Location = new System.Drawing.Point(540, 418);
            this.txtLen.Name = "txtLen";
            this.txtLen.ReadOnly = true;
            this.txtLen.Size = new System.Drawing.Size(100, 21);
            this.txtLen.TabIndex = 11;
            // 
            // btnGetLength
            // 
            this.btnGetLength.Location = new System.Drawing.Point(540, 445);
            this.btnGetLength.Name = "btnGetLength";
            this.btnGetLength.Size = new System.Drawing.Size(100, 23);
            this.btnGetLength.TabIndex = 12;
            this.btnGetLength.Text = "获取字符长度";
            this.btnGetLength.UseVisualStyleBackColor = true;
            this.btnGetLength.Click += new System.EventHandler(this.btnGetLength_Click);
            // 
            // frmBase64
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 498);
            this.Controls.Add(this.btnGetLength);
            this.Controls.Add(this.txtLen);
            this.Controls.Add(this.btnSaveTXT);
            this.Controls.Add(this.btnSavePhoto);
            this.Controls.Add(this.btnClear2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtPhotoName);
            this.Controls.Add(this.btnbase642Photo);
            this.Controls.Add(this.btnPhoto2base64);
            this.Controls.Add(this.btnSelectPhoto);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "frmBase64";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "照片base64编码";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSelectPhoto;
        private System.Windows.Forms.Button btnPhoto2base64;
        private System.Windows.Forms.Button btnbase642Photo;
        private System.Windows.Forms.TextBox txtPhotoName;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClear2;
        private System.Windows.Forms.Button btnSavePhoto;
        private System.Windows.Forms.Button btnSaveTXT;
        private System.Windows.Forms.TextBox txtLen;
        private System.Windows.Forms.Button btnGetLength;
    }
}

