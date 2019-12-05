namespace lxw_Helper
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCol = new System.Windows.Forms.DataGridView();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出表结构ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetTable = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkSort = new System.Windows.Forms.CheckBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置连接字符串ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dES加密解密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mD5加密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uRL编码解码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.base64编码解码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.btnCreateAdd = new System.Windows.Forms.Button();
            this.btnCreateEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnCreatAll = new System.Windows.Forms.Button();
            this.notifyHelper = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPackage = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPojo = new System.Windows.Forms.Button();
            this.btnMapper = new System.Windows.Forms.Button();
            this.btnMapperXML = new System.Windows.Forms.Button();
            this.btnService = new System.Windows.Forms.Button();
            this.btnServiceImpl = new System.Windows.Forms.Button();
            this.btnController = new System.Windows.Forms.Button();
            this.btnCreateProDir = new System.Windows.Forms.Button();
            this.btnCreateCodeToPro = new System.Windows.Forms.Button();
            this.txtProPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnCreateHtml = new System.Windows.Forms.Button();
            this.btnExportTable2Excel = new System.Windows.Forms.Button();
            this.chkPascal = new System.Windows.Forms.CheckBox();
            this.bgwExport = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnInfo = new System.Windows.Forms.Button();
            this.txtTName_C = new System.Windows.Forms.TextBox();
            this.txtTName_P = new System.Windows.Forms.TextBox();
            this.lblTableName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(668, 532);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 34);
            this.btnCopy.TabIndex = 32;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "列信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "表信息";
            // 
            // dgvCol
            // 
            this.dgvCol.AllowUserToAddRows = false;
            this.dgvCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCol.Location = new System.Drawing.Point(275, 135);
            this.dgvCol.Name = "dgvCol";
            this.dgvCol.RowHeadersWidth = 21;
            this.dgvCol.RowTemplate.Height = 23;
            this.dgvCol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCol.Size = new System.Drawing.Size(642, 393);
            this.dgvCol.TabIndex = 25;
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTable.Location = new System.Drawing.Point(18, 135);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersWidth = 21;
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.Size = new System.Drawing.Size(240, 393);
            this.dgvTable.TabIndex = 24;
            this.dgvTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看数据ToolStripMenuItem,
            this.导出表结构ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // 查看数据ToolStripMenuItem
            // 
            this.查看数据ToolStripMenuItem.Name = "查看数据ToolStripMenuItem";
            this.查看数据ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.查看数据ToolStripMenuItem.Text = "查看数据";
            this.查看数据ToolStripMenuItem.Click += new System.EventHandler(this.查看数据ToolStripMenuItem_Click);
            // 
            // 导出表结构ToolStripMenuItem
            // 
            this.导出表结构ToolStripMenuItem.Name = "导出表结构ToolStripMenuItem";
            this.导出表结构ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.导出表结构ToolStripMenuItem.Text = "导出选中表结构";
            this.导出表结构ToolStripMenuItem.Click += new System.EventHandler(this.导出表结构ToolStripMenuItem_Click);
            // 
            // btnGetTable
            // 
            this.btnGetTable.Location = new System.Drawing.Point(18, 39);
            this.btnGetTable.Name = "btnGetTable";
            this.btnGetTable.Size = new System.Drawing.Size(110, 46);
            this.btnGetTable.TabIndex = 23;
            this.btnGetTable.Text = "获取所有表";
            this.btnGetTable.UseVisualStyleBackColor = true;
            this.btnGetTable.Click += new System.EventHandler(this.btnGetTable_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(753, 532);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.Location = new System.Drawing.Point(363, 113);
            this.chkSort.Name = "chkSort";
            this.chkSort.Size = new System.Drawing.Size(72, 16);
            this.chkSort.TabIndex = 40;
            this.chkSort.Text = "是否排序";
            this.chkSort.UseVisualStyleBackColor = true;
            this.chkSort.CheckedChanged += new System.EventHandler(this.chkSort_CheckedChanged);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("宋体", 12F);
            this.txtFind.Location = new System.Drawing.Point(18, 91);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(240, 26);
            this.txtFind.TabIndex = 41;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(966, 25);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建查询ToolStripMenuItem,
            this.设置连接字符串ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 新建查询ToolStripMenuItem
            // 
            this.新建查询ToolStripMenuItem.Name = "新建查询ToolStripMenuItem";
            this.新建查询ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.新建查询ToolStripMenuItem.Text = "新建查询";
            this.新建查询ToolStripMenuItem.Click += new System.EventHandler(this.新建查询ToolStripMenuItem_Click);
            // 
            // 设置连接字符串ToolStripMenuItem
            // 
            this.设置连接字符串ToolStripMenuItem.Name = "设置连接字符串ToolStripMenuItem";
            this.设置连接字符串ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.设置连接字符串ToolStripMenuItem.Text = "设置连接字符串";
            this.设置连接字符串ToolStripMenuItem.Click += new System.EventHandler(this.设置连接字符串ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dES加密解密ToolStripMenuItem,
            this.mD5加密ToolStripMenuItem,
            this.uRL编码解码ToolStripMenuItem,
            this.base64编码解码ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // dES加密解密ToolStripMenuItem
            // 
            this.dES加密解密ToolStripMenuItem.Name = "dES加密解密ToolStripMenuItem";
            this.dES加密解密ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.dES加密解密ToolStripMenuItem.Text = "DES加密解密";
            this.dES加密解密ToolStripMenuItem.Click += new System.EventHandler(this.dES加密解密ToolStripMenuItem_Click);
            // 
            // mD5加密ToolStripMenuItem
            // 
            this.mD5加密ToolStripMenuItem.Name = "mD5加密ToolStripMenuItem";
            this.mD5加密ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.mD5加密ToolStripMenuItem.Text = "MD5加密";
            this.mD5加密ToolStripMenuItem.Click += new System.EventHandler(this.mD5加密ToolStripMenuItem_Click);
            // 
            // uRL编码解码ToolStripMenuItem
            // 
            this.uRL编码解码ToolStripMenuItem.Name = "uRL编码解码ToolStripMenuItem";
            this.uRL编码解码ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.uRL编码解码ToolStripMenuItem.Text = "URL编码解码";
            this.uRL编码解码ToolStripMenuItem.Click += new System.EventHandler(this.uRL编码解码ToolStripMenuItem_Click);
            // 
            // base64编码解码ToolStripMenuItem
            // 
            this.base64编码解码ToolStripMenuItem.Name = "base64编码解码ToolStripMenuItem";
            this.base64编码解码ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.base64编码解码ToolStripMenuItem.Text = "Base64编码解码";
            this.base64编码解码ToolStripMenuItem.Click += new System.EventHandler(this.base64编码解码ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem,
            this.帮助ToolStripMenuItem1});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem1
            // 
            this.帮助ToolStripMenuItem1.Name = "帮助ToolStripMenuItem1";
            this.帮助ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.帮助ToolStripMenuItem1.Text = "帮助";
            this.帮助ToolStripMenuItem1.Click += new System.EventHandler(this.帮助ToolStripMenuItem1_Click);
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(580, 534);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(75, 31);
            this.btnCreateIndex.TabIndex = 45;
            this.btnCreateIndex.Text = "生成Index";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // btnCreateAdd
            // 
            this.btnCreateAdd.Location = new System.Drawing.Point(580, 566);
            this.btnCreateAdd.Name = "btnCreateAdd";
            this.btnCreateAdd.Size = new System.Drawing.Size(75, 31);
            this.btnCreateAdd.TabIndex = 46;
            this.btnCreateAdd.Text = "生成Add";
            this.btnCreateAdd.UseVisualStyleBackColor = true;
            this.btnCreateAdd.Click += new System.EventHandler(this.btnCreateAdd_Click);
            // 
            // btnCreateEdit
            // 
            this.btnCreateEdit.Location = new System.Drawing.Point(580, 601);
            this.btnCreateEdit.Name = "btnCreateEdit";
            this.btnCreateEdit.Size = new System.Drawing.Size(75, 31);
            this.btnCreateEdit.TabIndex = 47;
            this.btnCreateEdit.Text = "生成Edit";
            this.btnCreateEdit.UseVisualStyleBackColor = true;
            this.btnCreateEdit.Click += new System.EventHandler(this.btnCreateEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(668, 570);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 34);
            this.btnView.TabIndex = 51;
            this.btnView.Text = "查看";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCreatAll
            // 
            this.btnCreatAll.Location = new System.Drawing.Point(842, 607);
            this.btnCreatAll.Name = "btnCreatAll";
            this.btnCreatAll.Size = new System.Drawing.Size(75, 34);
            this.btnCreatAll.TabIndex = 52;
            this.btnCreatAll.Text = "生成所有";
            this.btnCreatAll.UseVisualStyleBackColor = true;
            this.btnCreatAll.Click += new System.EventHandler(this.btnCreatAll_Click);
            // 
            // notifyHelper
            // 
            this.notifyHelper.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyHelper.Icon")));
            this.notifyHelper.Text = "lxw_Helper";
            this.notifyHelper.Visible = true;
            this.notifyHelper.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyHelper_MouseDoubleClick);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(923, 313);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(31, 49);
            this.btnDown.TabIndex = 56;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(923, 233);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(31, 49);
            this.btnUp.TabIndex = 55;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(614, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 57;
            this.label3.Text = "包前缀：";
            // 
            // txtPackage
            // 
            this.txtPackage.Location = new System.Drawing.Point(673, 57);
            this.txtPackage.Name = "txtPackage";
            this.txtPackage.Size = new System.Drawing.Size(243, 21);
            this.txtPackage.TabIndex = 58;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(753, 87);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(163, 21);
            this.txtAuthor.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(706, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 59;
            this.label4.Text = "作者：";
            // 
            // btnPojo
            // 
            this.btnPojo.Location = new System.Drawing.Point(274, 534);
            this.btnPojo.Name = "btnPojo";
            this.btnPojo.Size = new System.Drawing.Size(75, 31);
            this.btnPojo.TabIndex = 61;
            this.btnPojo.Text = "生成Pojo";
            this.btnPojo.UseVisualStyleBackColor = true;
            this.btnPojo.Click += new System.EventHandler(this.btnPojo_Click);
            // 
            // btnMapper
            // 
            this.btnMapper.Location = new System.Drawing.Point(359, 534);
            this.btnMapper.Name = "btnMapper";
            this.btnMapper.Size = new System.Drawing.Size(75, 31);
            this.btnMapper.TabIndex = 62;
            this.btnMapper.Text = "生成Mapper";
            this.btnMapper.UseVisualStyleBackColor = true;
            this.btnMapper.Click += new System.EventHandler(this.btnMapper_Click);
            // 
            // btnMapperXML
            // 
            this.btnMapperXML.Location = new System.Drawing.Point(359, 566);
            this.btnMapperXML.Name = "btnMapperXML";
            this.btnMapperXML.Size = new System.Drawing.Size(101, 31);
            this.btnMapperXML.TabIndex = 63;
            this.btnMapperXML.Text = "生成Mapper.xml";
            this.btnMapperXML.UseVisualStyleBackColor = true;
            this.btnMapperXML.Click += new System.EventHandler(this.btnMapperXML_Click);
            // 
            // btnService
            // 
            this.btnService.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnService.Location = new System.Drawing.Point(468, 534);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(86, 31);
            this.btnService.TabIndex = 64;
            this.btnService.Text = "生成IService";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // btnServiceImpl
            // 
            this.btnServiceImpl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnServiceImpl.Location = new System.Drawing.Point(468, 566);
            this.btnServiceImpl.Name = "btnServiceImpl";
            this.btnServiceImpl.Size = new System.Drawing.Size(106, 31);
            this.btnServiceImpl.TabIndex = 65;
            this.btnServiceImpl.Text = "生成ServiceImpl";
            this.btnServiceImpl.UseVisualStyleBackColor = true;
            this.btnServiceImpl.Click += new System.EventHandler(this.btnServiceImpl_Click);
            // 
            // btnController
            // 
            this.btnController.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnController.Location = new System.Drawing.Point(468, 601);
            this.btnController.Name = "btnController";
            this.btnController.Size = new System.Drawing.Size(99, 31);
            this.btnController.TabIndex = 66;
            this.btnController.Text = "生成Controller";
            this.btnController.UseVisualStyleBackColor = true;
            this.btnController.Click += new System.EventHandler(this.btnController_Click);
            // 
            // btnCreateProDir
            // 
            this.btnCreateProDir.Location = new System.Drawing.Point(18, 602);
            this.btnCreateProDir.Name = "btnCreateProDir";
            this.btnCreateProDir.Size = new System.Drawing.Size(108, 31);
            this.btnCreateProDir.TabIndex = 67;
            this.btnCreateProDir.Text = "创建项目结构";
            this.btnCreateProDir.UseVisualStyleBackColor = true;
            this.btnCreateProDir.Click += new System.EventHandler(this.btnCreateProDir_Click);
            // 
            // btnCreateCodeToPro
            // 
            this.btnCreateCodeToPro.Location = new System.Drawing.Point(841, 532);
            this.btnCreateCodeToPro.Name = "btnCreateCodeToPro";
            this.btnCreateCodeToPro.Size = new System.Drawing.Size(75, 34);
            this.btnCreateCodeToPro.TabIndex = 68;
            this.btnCreateCodeToPro.Text = "后台代码生成到项目";
            this.btnCreateCodeToPro.UseVisualStyleBackColor = true;
            this.btnCreateCodeToPro.Click += new System.EventHandler(this.btnCreateCodeToPro_Click);
            // 
            // txtProPath
            // 
            this.txtProPath.Location = new System.Drawing.Point(312, 28);
            this.txtProPath.Name = "txtProPath";
            this.txtProPath.Size = new System.Drawing.Size(562, 21);
            this.txtProPath.TabIndex = 70;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 69;
            this.label5.Text = "项目路径：";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(885, 26);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(32, 23);
            this.btnSelectPath.TabIndex = 71;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // btnCreateHtml
            // 
            this.btnCreateHtml.Location = new System.Drawing.Point(842, 570);
            this.btnCreateHtml.Name = "btnCreateHtml";
            this.btnCreateHtml.Size = new System.Drawing.Size(75, 34);
            this.btnCreateHtml.TabIndex = 72;
            this.btnCreateHtml.Text = "前台代码生成";
            this.btnCreateHtml.UseVisualStyleBackColor = true;
            this.btnCreateHtml.Click += new System.EventHandler(this.btnCreateHtml_Click);
            // 
            // btnExportTable2Excel
            // 
            this.btnExportTable2Excel.Location = new System.Drawing.Point(150, 535);
            this.btnExportTable2Excel.Name = "btnExportTable2Excel";
            this.btnExportTable2Excel.Size = new System.Drawing.Size(108, 31);
            this.btnExportTable2Excel.TabIndex = 73;
            this.btnExportTable2Excel.Text = "导出所有表结构";
            this.btnExportTable2Excel.UseVisualStyleBackColor = true;
            this.btnExportTable2Excel.Click += new System.EventHandler(this.btnExportTable2Excel_Click);
            // 
            // chkPascal
            // 
            this.chkPascal.AutoSize = true;
            this.chkPascal.Checked = true;
            this.chkPascal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPascal.Location = new System.Drawing.Point(19, 543);
            this.chkPascal.Name = "chkPascal";
            this.chkPascal.Size = new System.Drawing.Size(84, 16);
            this.chkPascal.TabIndex = 74;
            this.chkPascal.Text = "Pascal风格";
            this.chkPascal.UseVisualStyleBackColor = true;
            // 
            // bgwExport
            // 
            this.bgwExport.WorkerReportsProgress = true;
            this.bgwExport.WorkerSupportsCancellation = true;
            this.bgwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExport_DoWork);
            this.bgwExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExport_ProgressChanged);
            this.bgwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExport_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 572);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(240, 21);
            this.progressBar1.TabIndex = 75;
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(580, 636);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 31);
            this.btnInfo.TabIndex = 76;
            this.btnInfo.Text = "生成Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // txtTName_C
            // 
            this.txtTName_C.Location = new System.Drawing.Point(470, 111);
            this.txtTName_C.Name = "txtTName_C";
            this.txtTName_C.ReadOnly = true;
            this.txtTName_C.Size = new System.Drawing.Size(220, 21);
            this.txtTName_C.TabIndex = 78;
            // 
            // txtTName_P
            // 
            this.txtTName_P.Location = new System.Drawing.Point(696, 111);
            this.txtTName_P.Name = "txtTName_P";
            this.txtTName_P.ReadOnly = true;
            this.txtTName_P.Size = new System.Drawing.Size(220, 21);
            this.txtTName_P.TabIndex = 79;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(18, 645);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(11, 12);
            this.lblTableName.TabIndex = 80;
            this.lblTableName.Text = "-";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 672);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.txtTName_P);
            this.Controls.Add(this.txtTName_C);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkPascal);
            this.Controls.Add(this.btnExportTable2Excel);
            this.Controls.Add(this.btnCreateHtml);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txtProPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCreateCodeToPro);
            this.Controls.Add(this.btnCreateProDir);
            this.Controls.Add(this.btnController);
            this.Controls.Add(this.btnServiceImpl);
            this.Controls.Add(this.btnService);
            this.Controls.Add(this.btnMapperXML);
            this.Controls.Add(this.btnMapper);
            this.Controls.Add(this.btnPojo);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPackage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnCreatAll);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnCreateEdit);
            this.Controls.Add(this.btnCreateAdd);
            this.Controls.Add(this.btnCreateIndex);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.chkSort);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCol);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.btnGetTable);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "lxw_Helper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCol;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Button btnGetTable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkSort;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置连接字符串ToolStripMenuItem;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.Button btnCreateAdd;
        private System.Windows.Forms.Button btnCreateEdit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnCreatAll;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 新建查询ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyHelper;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dES加密解密ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mD5加密ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uRL编码解码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem base64编码解码ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPackage;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPojo;
        private System.Windows.Forms.Button btnMapper;
        private System.Windows.Forms.Button btnMapperXML;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.Button btnServiceImpl;
        private System.Windows.Forms.Button btnController;
        private System.Windows.Forms.Button btnCreateProDir;
        private System.Windows.Forms.Button btnCreateCodeToPro;
        private System.Windows.Forms.TextBox txtProPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnCreateHtml;
        private System.Windows.Forms.Button btnExportTable2Excel;
        private System.Windows.Forms.CheckBox chkPascal;
        private System.ComponentModel.BackgroundWorker bgwExport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导出表结构ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看数据ToolStripMenuItem;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TextBox txtTName_C;
        private System.Windows.Forms.TextBox txtTName_P;
        private System.Windows.Forms.Label lblTableName;
    }
}

