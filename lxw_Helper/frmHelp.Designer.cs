namespace lxw_Helper
{
    partial class frmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelp));
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInfo.Location = new System.Drawing.Point(0, 0);
            this.rtxtInfo.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.ReadOnly = true;
            this.rtxtInfo.Size = new System.Drawing.Size(786, 457);
            this.rtxtInfo.TabIndex = 1;
            this.rtxtInfo.Text = "";
            // 
            // frmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 457);
            this.Controls.Add(this.rtxtInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帮助信息";
            this.Load += new System.EventHandler(this.frmHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtInfo;
    }
}