namespace lxw_Helper
{
    partial class frmURLDecodeEncode
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
            this.btnClear = new System.Windows.Forms.Button();
            this.txtDecrypt = new System.Windows.Forms.RichTextBox();
            this.txtEncrypt = new System.Windows.Forms.RichTextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(722, 192);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(12, 12);
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtDecrypt.Size = new System.Drawing.Size(816, 164);
            this.txtDecrypt.TabIndex = 10;
            this.txtDecrypt.Text = "";
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(12, 243);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEncrypt.Size = new System.Drawing.Size(816, 150);
            this.txtEncrypt.TabIndex = 9;
            this.txtEncrypt.Text = "";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(604, 192);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(100, 30);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "解码↑";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(486, 192);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(100, 30);
            this.btnEncrypt.TabIndex = 7;
            this.btnEncrypt.Text = "编码↓";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // frmURLDecodeEncode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 418);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtDecrypt);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Name = "frmURLDecodeEncode";
            this.Text = "URL编码解码";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox txtDecrypt;
        private System.Windows.Forms.RichTextBox txtEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
    }
}