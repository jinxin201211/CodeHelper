namespace lxw_Helper
{
    partial class frmMD5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDecrypt = new System.Windows.Forms.RichTextBox();
            this.txtEncrypt = new System.Windows.Forms.RichTextBox();
            this.btnEncrypt32 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(12, 12);
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtDecrypt.Size = new System.Drawing.Size(804, 122);
            this.txtDecrypt.TabIndex = 10;
            this.txtDecrypt.Text = "";
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(12, 201);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEncrypt.Size = new System.Drawing.Size(804, 122);
            this.txtEncrypt.TabIndex = 9;
            this.txtEncrypt.Text = "";
            // 
            // btnEncrypt32
            // 
            this.btnEncrypt32.Location = new System.Drawing.Point(12, 152);
            this.btnEncrypt32.Name = "btnEncrypt32";
            this.btnEncrypt32.Size = new System.Drawing.Size(100, 30);
            this.btnEncrypt32.TabIndex = 8;
            this.btnEncrypt32.Text = "加密↓";
            this.btnEncrypt32.UseVisualStyleBackColor = true;
            this.btnEncrypt32.Click += new System.EventHandler(this.btnEncrypt32_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(139, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmMD5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 352);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtDecrypt);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.btnEncrypt32);
            this.Name = "frmMD5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MD5加密";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtDecrypt;
        private System.Windows.Forms.RichTextBox txtEncrypt;
        private System.Windows.Forms.Button btnEncrypt32;
        private System.Windows.Forms.Button btnClear;
    }
}