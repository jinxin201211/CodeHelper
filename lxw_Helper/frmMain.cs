using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lxw_Helper.Common;
using System.IO;
using System.Configuration;
using lxw_Helper.Model;

namespace lxw_Helper
{
    public partial class frmMain : Form
    {
        string TableName = "";//表名称
        string TableComments = "";//表描述
        List<ColModel> ltCol = null;//列信息集合
        string strCode = "";
        int rowIndex = 0;
        DataTable dt = new DataTable();
        string fileType = "cs"; //cs  cshtml
        string author = "lxw";
        String path = Application.StartupPath + @"\AutoCode";

        List<TableModel> ltTable = null;//表信息集合

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowAnimate.AnimateWindow(this.Handle, 200, WindowAnimate.AW_CENTER | WindowAnimate.AW_HIDE);
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WindowAnimate.AnimateWindow(this.Handle, 200, WindowAnimate.AW_CENTER);
            try
            {
                txtPackage.Text = ConfigurationManager.AppSettings["package"] ?? "";
                txtAuthor.Text = ConfigurationManager.AppSettings["author"] ?? "";
                txtProPath.Text = ConfigurationManager.AppSettings["propath"] ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("从app.config配置文件中读取[package],[author]异常");
            }

        }

        private void btnSetConn_Click(object sender, EventArgs e)
        {
            frmParCfg frm = new frmParCfg();
            frm.isFirstRun = false;
            frm.ShowDialog();
        }

        private void btnGetTable_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(DbHelperOra.ConnectionString))
            //{
            //    MessageBox.Show("请先设置连接字符串");
            //    return;
            //}
            try
            {
                dt = DataBaseHelper.GetTable();
                //dt = GetTable();
                ltTable = dt.ToList<TableModel>();

                dgvTable.DataSource = null;
                dgvTable.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //private DataTable GetTable()
        //{
        //    try
        //    {
        //        string strSQL = @"SELECT A.TABLE_NAME, B.COMMENTS
        //                      FROM USER_TABLES A
        //                      LEFT JOIN USER_TAB_COMMENTS B
        //                      ON A.TABLE_NAME = B.TABLE_NAME ORDER BY TABLE_NAME";

        //        return DbHelperOra.Query(strSQL).Tables[0];
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //        return null;
        //    }

        //}

        private void dgvTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTable.Rows.Count > 0)
            {
                int rowIndex = dgvTable.SelectedRows[0].Index;  //得到当前选中行的索引
                if (rowIndex < 0)
                {
                    return;
                }

                TableName = dgvTable.SelectedRows[0].Cells[0].Value.ToString();
                TableComments = dgvTable.SelectedRows[0].Cells[1].Value.ToString();

                lblTableName.Text = TableName.ToLower() + "(" + TableComments + ")";
                txtTName_C.Text = CodeHelper.ConvertToCamel(TableName, "_");
                txtTName_P.Text = CodeHelper.ConvertToPascal(TableName, "_");
                //ltCol = GetColInfoByTable(TableName);
                ltCol = DataBaseHelper.GetColInfoByTable(TableName, chkSort.Checked);

                ltCol.ForEach(o =>
                {
                    o.List = true;
                    o.Add = true;
                    o.Edit = true;
                });


                //判断字段说明是否包含冒号，默认填充codeTypeComments
                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.codeTypeComments = o.COMMENTS.Replace("：", ":").Split(':')[0];
                        }
                    });
                }


                //下拉列表处理
                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }

                //字段形式,Number类型长度处理 
                foreach (var item in ltCol)
                {
                    item.ColumnName_Camel = CodeHelper.ConvertToCamel(item.COLUMN_NAME, "_");
                    item.ColumnName_Pascal = CodeHelper.ConvertToPascal(item.COLUMN_NAME, "_");
                    item.ColumnName_Lower = item.COLUMN_NAME.ToLower();
                    item.DATA_LENGTH_Ex = GetNumberTypeLength(item);
                }

                string strSQL2 = " select * from " + TableName + " where 1=0";
                //DataChange.GetCSharpType(ltCol, DbHelperOra.Query(strSQL2).Tables[0]);
                DataChange.GetCSharpType(ltCol, DataBaseHelper.Query(strSQL2).Tables[0]);

                BindData();
            }
        }

        //private List<ColModel> GetColInfoByTable(string tableName)
        //{
        //    string strSQL = @" SELECT A.COLUMN_NAME,
        //                               A.DATA_TYPE,
        //                               B.COMMENTS,
        //                               A.NULLABLE,
        //                               A.DATA_LENGTH,
        //                               CASE
        //                                 WHEN C.CONSTRAINT_NAME IS NULL THEN
        //                                  '0'
        //                                 ELSE
        //                                  '1'
        //                               END AS ISPK,
        //                               C.CONSTRAINT_NAME,
        //                               A.DATA_DEFAULT,
        //                               A.CHAR_LENGTH, A.DATA_PRECISION,A.DATA_SCALE
        //                          FROM USER_TAB_COLUMNS A
        //                          LEFT JOIN USER_COL_COMMENTS B
        //                            ON A.TABLE_NAME = B.TABLE_NAME
        //                           AND A.COLUMN_NAME = B.COLUMN_NAME
        //                          LEFT JOIN (SELECT A.TABLE_NAME, A.COLUMN_NAME, A.CONSTRAINT_NAME
        //                                       FROM USER_CONS_COLUMNS A, USER_CONSTRAINTS B
        //                                      WHERE A.CONSTRAINT_NAME = B.CONSTRAINT_NAME
        //                                        AND B.CONSTRAINT_TYPE = 'P'"
        //                            + " AND B.TABLE_NAME = '" + tableName + "') C"
        //                            + "  ON A.TABLE_NAME = C.TABLE_NAME"
        //                            + " AND A.COLUMN_NAME = C.COLUMN_NAME"
        //                            + " WHERE A.TABLE_NAME = '" + tableName + "'";

        //    if (chkSort.Checked)
        //    {
        //        strSQL += " ORDER BY A.COLUMN_NAME";
        //    }
        //    List<ColModel> ltCol = DbHelperOra.Query(strSQL).Tables[0].ToList<ColModel>();

        //    foreach (var item in ltCol)
        //    {
        //        if (item.DATA_DEFAULT == null)
        //        {
        //            item.DATA_DEFAULT = "";
        //        }
        //        else
        //        {
        //            item.DATA_DEFAULT = item.DATA_DEFAULT.Replace("\n", "").Trim();
        //            if (item.DATA_DEFAULT == "null")
        //            {
        //                item.DATA_DEFAULT = "";
        //            }
        //        }
        //    }
        //    return ltCol;
        //}

        private void BindData()
        {
            dgvCol.DataSource = null;
            dgvCol.DataSource = ltCol;

            dgvCol.Columns[0].Width = 40;
            dgvCol.Columns[1].Width = 40;
            dgvCol.Columns[2].Width = 40;
            dgvCol.Columns[3].Width = 40;

            dgvCol.Columns[7].Width = 40;
            dgvCol.Columns[8].Width = 40;
            dgvCol.Columns[9].Width = 40;
            dgvCol.Columns[10].Width = 60;
            dgvCol.Columns[11].Width = 40;
            dgvCol.Columns[12].Width = 40;
            dgvCol.Columns[13].Width = 80;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strCode))
            {
                MessageBox.Show("无代码，请先生成！");
            }
            else
            {
                Clipboard.SetDataObject(strCode);
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFind.Text))
                {
                    dgvTable.DataSource = null;
                    dgvTable.DataSource = dt;
                }


                DataRow[] datarows = dt.Select(" TABLE_NAME like '%" + txtFind.Text.Trim().ToUpper() + "%'");
                dgvTable.DataSource = null;
                dgvTable.DataSource = ToDataTable(datarows);

            }
            catch (Exception ex)
            {


            }
        }

        private DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone(); // 复制DataRow的表结构
            foreach (DataRow row in rows)
            {
                tmp.ImportRow(row); // 将DataRow添加到DataTable中
            }
            return tmp;
        }

        private void chkSort_CheckedChanged(object sender, EventArgs e)
        {

            if (dgvCol == null)
            {
                dgvTable_CellDoubleClick(null, null);
            }
            dgvCol.DataSource = null;
            if (chkSort.Checked)
            {
                dgvCol.DataSource = ltCol.OrderBy(o => o.COLUMN_NAME).ToList();
            }
            else
            {
                dgvCol.DataSource = ltCol;
            }
            dgvCol.Columns[0].Width = 40;
            dgvCol.Columns[1].Width = 40;
            dgvCol.Columns[2].Width = 40;

        }

        #region 菜单

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1();
            frm.Show();
        }

        private void 设置连接字符串ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParCfg frm = new frmParCfg();
            frm.isFirstRun = false;
            frm.ShowDialog();
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmHelp frm = new frmHelp();
            string info = File.ReadAllText(Application.StartupPath + @"\Template\Help.txt");

            frm.info = info;
            frm.Show();
        }

        private void 新建查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSQLWindow frm = new frmSQLWindow();
            frm.Show();
        }

        private void dES加密解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDES frm = new frmDES();
            frm.Show();
        }

        private void mD5加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMD5 frm = new frmMD5();
            frm.Show();
        }

        private void uRL编码解码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmURLDecodeEncode frm = new frmURLDecodeEncode();
            frm.Show();
        }

        private void base64编码解码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBase64 frm = new frmBase64();
            frm.Show();
        }

        #endregion

        void UpdateInfo()
        {


            for (int row = dgvCol.Rows.Count - 1; row >= 0; row--)
            {
                if (dgvCol.Rows[row].Cells[0].Value.ToString() == "True")
                {
                    ltCol[row].List = true;
                }
                else
                {
                    ltCol[row].List = false;
                }

                if (dgvCol.Rows[row].Cells[1].Value.ToString() == "True")
                {
                    ltCol[row].Add = true;
                }
                else
                {
                    ltCol[row].Add = false;
                }


                if (dgvCol.Rows[row].Cells[2].Value.ToString() == "True")
                {
                    ltCol[row].Edit = true;
                }
                else
                {
                    ltCol[row].Edit = false;
                }

                if (dgvCol.Rows[row].Cells[3].Value.ToString() == "True")
                {
                    ltCol[row].qCondition = true;
                }
                else
                {
                    ltCol[row].qCondition = false;
                }

                //是否下拉类别相关
                if (dgvCol.Rows[row].Cells[12].Value == null)
                {
                    ltCol[row].codeType = "";
                    ltCol[row].codeTypeComments = "";
                }
                else
                {
                    ltCol[row].codeType = dgvCol.Rows[row].Cells[12].Value.ToString().Trim();
                    if (dgvCol.Rows[row].Cells[13].Value == null)
                    {
                        ltCol[row].codeTypeComments = "";
                    }
                    else
                    {
                        ltCol[row].codeTypeComments = dgvCol.Rows[row].Cells[13].Value.ToString().Trim();
                    }

                }

                //主键特殊处理
                if (dgvCol.Rows[row].Cells[8].Value.ToString() == "1")
                {
                    ltCol[row].List = true;
                    ltCol[row].Add = true;
                    ltCol[row].Edit = true;
                }

            }



        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();
                fileType = "html";
                ColModel col = ltCol.Find(o => o.ISPK == "1");
                if (col == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                List<ColModel> ltTemp = ltCol.FindAll(o => o.List == true);

                List<ColModel> ltQueryCon = ltCol.FindAll(o => o.qCondition == true);

                strCode = CodeHelper.GetIndex(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col, ltQueryCon);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }

        }

        private void btnCreateAdd_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();
                fileType = "html";

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }

                List<ColModel> ltTemp = ltCol.FindAll(o => o.Add == true);
                strCode = CodeHelper.GetAdd(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnCreateEdit_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();
                fileType = "html";

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }
                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {

                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }

                List<ColModel> ltTemp = ltCol.FindAll(o => o.Edit == true);
                strCode = CodeHelper.GetEdit(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strCode))
            {
                MessageBox.Show("无代码，请先生成！");
            }
            else
            {
                frmView frm = new frmView();
                frm.strCode = strCode;
                frm.Show();
            }
        }

        private void btnCreatAll_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath + @"\AutoCode";
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }


            if (dgvCol.Rows.Count > 0)
            {

                ColModel col = ltCol.Find(o => o.ISPK == "1");
                if (col == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                UpdateInfo();

                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {
                        //o.COLUMN_NAME = CodeHelper.ConvertToCamel(o.COLUMN_NAME, "_");
                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }



                //Index
                List<ColModel> ltTemp = ltCol.FindAll(o => o.List == true);
                List<ColModel> ltQueryCon = ltCol.FindAll(o => o.qCondition == true);
                strCode = CodeHelper.GetIndex(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col, ltQueryCon);
                File.WriteAllText(path + "//" + "index.html", strCode);

                //Add
                ltTemp = ltCol.FindAll(o => o.Add == true);
                strCode = CodeHelper.GetAdd(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col);
                File.WriteAllText(path + "//" + "add.html", strCode);

                //Edit
                ltTemp = ltCol.FindAll(o => o.Edit == true);
                strCode = CodeHelper.GetEdit(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col);
                File.WriteAllText(path + "//" + "edit.html", strCode);

                //info
                //ltTemp = ltCol.FindAll(o => o.Edit == true);
                strCode = CodeHelper.GetInfo(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltCol, col);
                File.WriteAllText(path + "//" + "info.html", strCode);


                //pojo
                strCode = CodeHelper.GetModel(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text);
                File.WriteAllText(path + "//" + CodeHelper.ConvertToPascal(TableName, "_") + ".java", strCode);
                //mapper
                strCode = CodeHelper.GetMapper(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                File.WriteAllText(path + "//" + CodeHelper.ConvertToPascal(TableName, "_") + "Mapper.java", strCode);
                //mapper.xml
                strCode = CodeHelper.GetMapperXML(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                File.WriteAllText(path + "//" + CodeHelper.ConvertToPascal(TableName, "_") + "Mapper.xml", strCode);
                //IService
                strCode = CodeHelper.GetIService(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                File.WriteAllText(path + "//I" + CodeHelper.ConvertToPascal(TableName, "_") + "Service.java", strCode);

                //ServiceImpl
                strCode = CodeHelper.GetServiceImpl(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                File.WriteAllText(path + "//" + CodeHelper.ConvertToPascal(TableName, "_") + "Impl.java", strCode);

                //Controller
                strCode = CodeHelper.GetController(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                File.WriteAllText(path + "//" + CodeHelper.ConvertToPascal(TableName, "_") + "Controller.java", strCode);

                //打开文件夹
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show("无信息！");
            }

        }

        #region 托盘


        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyHelper.Visible = true;
            }
        }

        private void notifyHelper_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            //{
            //    this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            //}

            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyHelper.Visible = false;
        }
        #endregion

        private void btnUp_Click(object sender, EventArgs e)
        {

            if (dgvCol.Rows.Count > 0)
            {
                try
                {
                    int rowIndex = dgvCol.SelectedRows[0].Index;  //得到当前选中行的索引
                    if (rowIndex == 0)
                    {
                        MessageBox.Show("已经是第一行了!");
                        return;
                    }


                    //调整List里数据顺序
                    ColModel temp = ltCol[rowIndex];
                    ltCol.RemoveAt(rowIndex);
                    ltCol.Insert(rowIndex - 1, temp);


                    BindData();

                    foreach (DataGridViewRow row in dgvCol.Rows)
                    {
                        row.Selected = false;
                    }
                    dgvCol.Rows[rowIndex - 1].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                try
                {
                    int rowIndex = dgvCol.SelectedRows[0].Index;  //得到当前选中行的索引
                    if (rowIndex == dgvCol.Rows.Count - 1)
                    {
                        MessageBox.Show("已经是最后一行了!");
                        return;
                    }

                    //调整List里数据顺序
                    ColModel temp = ltCol[rowIndex];
                    ltCol.RemoveAt(rowIndex);
                    ltCol.Insert(rowIndex + 1, temp);

                    BindData();

                    foreach (DataGridViewRow row in dgvCol.Rows)
                    {
                        row.Selected = false;
                    }
                    dgvCol.Rows[rowIndex + 1].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strCode))
            {
                MessageBox.Show("无代码，请先生成！");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (fileType == "html")
            {
                saveFileDialog1.DefaultExt = "html";
                saveFileDialog1.Filter = "html files (*.html)|*.html";
            }
            else if (fileType == "xml")
            {
                saveFileDialog1.DefaultExt = "xml";
                saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            }
            else
            {
                saveFileDialog1.DefaultExt = "java";
                saveFileDialog1.Filter = "java files (*.java)|*.java";
            }


            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                File.WriteAllText(saveFileDialog1.FileName, strCode);
                MessageBox.Show("成功保存");
            }
        }

        private void btnPojo_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();
                fileType = "java";
                strCode = CodeHelper.GetModel(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnMapper_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                fileType = "java";
                strCode = CodeHelper.GetMapper(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnMapperXML_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                fileType = "xml";
                strCode = CodeHelper.GetMapperXML(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                fileType = "java";
                strCode = CodeHelper.GetIService(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnServiceImpl_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                fileType = "java";
                strCode = CodeHelper.GetServiceImpl(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnController_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                fileType = "java";
                strCode = CodeHelper.GetController(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }

        private void btnCreateProDir_Click(object sender, EventArgs e)
        {
            string basepath = "";
            if (string.IsNullOrEmpty(txtProPath.Text))
            {
                FolderBrowserDialog path = new FolderBrowserDialog();
                path.ShowDialog();
                basepath = path.SelectedPath;
                if (string.IsNullOrEmpty(basepath))
                {
                    MessageBox.Show("请先选择目录");
                    return;
                }
            }
            else
            {
                basepath = txtProPath.Text;
            }


            string rootpath = txtPackage.Text;
            if (string.IsNullOrEmpty(rootpath))
            {
                MessageBox.Show("请先填写包前缀");
                return;
            }

            rootpath = "/src/main/java/" + rootpath.Replace(".", "/");

            try
            {
                //comm
                CreateDir(basepath + rootpath + "/comm");

                //Response
                string package = "package " + txtPackage.Text + ".comm;";
                string template = package + "\r\n" + File.ReadAllText(Application.StartupPath + @"\Template\Response.txt");
                File.WriteAllText(basepath + rootpath + "/comm/" + "Response.java", template);
                //ResultCodeEnum
                template = package + "\r\n" + File.ReadAllText(Application.StartupPath + @"\Template\ResultCodeEnum.txt");
                File.WriteAllText(basepath + rootpath + "/comm/" + "ResultCodeEnum.java", template);
                //controller
                CreateDir(basepath + rootpath + "/controller");
                //mapper
                CreateDir(basepath + rootpath + "/mapper");
                //pojo
                CreateDir(basepath + rootpath + "/pojo");
                //service
                CreateDir(basepath + rootpath + "/service");
                // service/impl
                CreateDir(basepath + rootpath + "/service/impl");
                //viewpojo
                CreateDir(basepath + rootpath + "/viewpojo");
                //mapper
                CreateDir(basepath + "/src/main/resources/mapper");

                //properties
                template = File.ReadAllText(Application.StartupPath + @"\Template\properties.txt", Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(template, Config.IP, Config.Name, Config.Pwd, txtPackage.Text);
                File.WriteAllText(basepath + "/src/main/resources/" + "application.properties", sb.ToString(), Encoding.UTF8);

                MessageBox.Show("创建成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建失败：" + ex.Message);
            }

        }

        void CreateDir(string path)
        {
            //检查是否存在文件夹
            if (false == System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        private void btnCreateCodeToPro_Click(object sender, EventArgs e)
        {
            string basepath = "";
            if (txtProPath.Text == "")
            {
                FolderBrowserDialog path = new FolderBrowserDialog();
                path.ShowDialog();
                basepath = path.SelectedPath;
                if (string.IsNullOrEmpty(basepath))
                {
                    MessageBox.Show("请先选择目录");
                    return;
                }
            }
            else
            {
                basepath = txtProPath.Text;
            }


            try
            {
                string rootpath = txtPackage.Text;
                if (string.IsNullOrEmpty(rootpath))
                {
                    MessageBox.Show("请先填写包前缀");
                    return;
                }
                rootpath = "/src/main/java/" + rootpath.Replace(".", "/");

                //comm
                CreateDir(basepath + rootpath + "/comm");
                //controller
                CreateDir(basepath + rootpath + "/controller");
                //mapper
                CreateDir(basepath + rootpath + "/mapper");
                //pojo
                CreateDir(basepath + rootpath + "/pojo");
                //service
                CreateDir(basepath + rootpath + "/service");
                // service/impl
                CreateDir(basepath + rootpath + "/service/impl");
                //viewpojo
                CreateDir(basepath + rootpath + "/viewpojo");
                //mapper
                CreateDir(basepath + "/main/resources/mapper");

                if (dgvCol.Rows.Count > 0)
                {
                    #region 列检测、处理

                    ColModel col = ltCol.Find(o => o.ISPK == "1");
                    if (col == null)
                    {
                        MessageBox.Show(TableName + "缺少主键!");
                        return;
                    }

                    UpdateInfo();

                    if (ltCol != null && ltCol.Count > 0)
                    {
                        ltCol.ForEach(o =>
                        {
                            // o.COLUMN_NAME = CodeHelper.ConvertToCamel(o.COLUMN_NAME, "_");
                            if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                            {
                                o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                                if (string.IsNullOrEmpty(o.codeTypeComments))
                                {
                                    o.codeTypeComments = o.COMMENTS;
                                }
                            }
                        });
                    }
                    #endregion

                    //pojo
                    strCode = CodeHelper.GetModel(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text);
                    File.WriteAllText(basepath + rootpath + "/pojo/" + CodeHelper.ConvertToPascal(TableName, "_") + ".java", strCode);

                    //mapper
                    strCode = CodeHelper.GetMapper(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                    File.WriteAllText(basepath + rootpath + "/mapper/" + CodeHelper.ConvertToPascal(TableName, "_") + "Mapper.java", strCode);

                    //mapper.xml
                    strCode = CodeHelper.GetMapperXML(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                    File.WriteAllText(basepath + "/src/main/resources/mapper/" + CodeHelper.ConvertToPascal(TableName, "_") + "Mapper.xml", strCode);

                    //IService
                    strCode = CodeHelper.GetIService(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                    File.WriteAllText(basepath + rootpath + "/service/I" + CodeHelper.ConvertToPascal(TableName, "_") + "Service.java", strCode);

                    //ServiceImpl
                    strCode = CodeHelper.GetServiceImpl(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                    File.WriteAllText(basepath + rootpath + "/service/impl/" + CodeHelper.ConvertToPascal(TableName, "_") + "ServiceImpl.java", strCode);

                    //Controller
                    strCode = CodeHelper.GetController(TableName, ltCol, TableComments, txtPackage.Text, txtAuthor.Text, col);
                    File.WriteAllText(basepath + rootpath + "/controller/" + CodeHelper.ConvertToPascal(TableName, "_") + "Controller.java", strCode);

                    MessageBox.Show("创建成功!");
                }
                else
                {
                    MessageBox.Show("无信息！");
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("创建失败：" + ex.Message);
            }



        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            txtProPath.Text = path.SelectedPath;
        }

        private void btnCreateHtml_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\AutoCode";
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            if (dgvCol.Rows.Count > 0)
            {

                ColModel col = ltCol.Find(o => o.ISPK == "1");
                if (col == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }

                UpdateInfo();

                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }


                //Index
                List<ColModel> ltTemp = ltCol.FindAll(o => o.List == true);
                List<ColModel> ltQueryCon = ltCol.FindAll(o => o.qCondition == true);
                strCode = CodeHelper.GetIndex(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col, ltQueryCon);
                File.WriteAllText(path + "//" + "index.html", strCode);

                //Add
                ltTemp = ltCol.FindAll(o => o.Add == true);
                strCode = CodeHelper.GetAdd(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col);
                File.WriteAllText(path + "//" + "add.html", strCode);

                //Edit
                ltTemp = ltCol.FindAll(o => o.Edit == true);
                strCode = CodeHelper.GetEdit(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, col);
                File.WriteAllText(path + "//" + "edit.html", strCode);

                //Info
                strCode = CodeHelper.GetInfo(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltCol, col);
                File.WriteAllText(path + "//" + "info.html", strCode);

                //打开文件夹
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show("无信息！");
            }

        }

        #region 导出表结构
        private void btnExportTable2Excel_Click(object sender, EventArgs e)
        {
            ltTable = DataBaseHelper.GetTable().ToList<TableModel>();
            //ltTable = GetTable().ToList<TableModel>();

            if (ltTable == null || ltTable.Count == 0)
            {
                MessageBox.Show("无信息");
                return;
            }

            if (!bgwExport.IsBusy)
            {
                this.bgwExport.RunWorkerAsync();
                bgwExport.InitializeLifetimeService();
            }
        }

        void CombineTypeAndLength(ColModel col)
        {
            if (col.DATA_TYPE.Contains("CHAR"))
            {
                col.DATA_TYPE = string.Format("{0}({1})", col.DATA_TYPE, col.CHAR_LENGTH);
            }
            else if (col.DATA_TYPE == "NUMBER")
            {
                if (col.DATA_PRECISION != null && col.DATA_SCALE != null)
                {
                    if (col.DATA_SCALE.Value == 0)
                    {
                        col.DATA_TYPE = string.Format("{0}({1})", col.DATA_TYPE, col.DATA_PRECISION);
                    }
                    else
                    {
                        col.DATA_TYPE = string.Format("{0}({1},{2})", col.DATA_TYPE, col.DATA_PRECISION, col.DATA_SCALE);
                    }
                }
            }
        }

        /// <summary>
        /// 获取Number类型实际长度
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        decimal GetNumberTypeLength(ColModel col)
        {
            if (col.DATA_TYPE == "NUMBER")
            {
                if (col.DATA_PRECISION != null && col.DATA_SCALE != null)
                {
                    if (col.DATA_SCALE.Value == 0)
                    {
                        return col.DATA_PRECISION.Value;
                    }
                    else
                    {
                        return col.DATA_PRECISION.Value + col.DATA_SCALE.Value + 1;
                    }
                }
                else
                {
                    return col.DATA_LENGTH;
                }
            }
            else
            {
                return col.DATA_LENGTH;
            }
        }

        private void bgwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            int barPrecent = 0;
            int total = ltTable.Count;

            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            //查询表中的列信息
            for (int i = 0; i < ltTable.Count; i++)
            {
                //ltTable[i].ltCol = GetColInfoByTable(ltTable[i].TABLE_NAME);
                ltTable[i].ltCol = DataBaseHelper.GetColInfoByTable(ltTable[i].TABLE_NAME, chkSort.Checked);
                barPrecent = (int)(((double)i / (double)total) * 100);
                bgwExport.ReportProgress(barPrecent, i);
            }

            #region  Pascal风格处理 类型和长度拼接
            if (chkPascal.Checked == true)
            {
                foreach (var item in ltTable)
                {
                    item.TABLE_NAME = CodeHelper.ConvertToPascal2(item.TABLE_NAME, '_');

                    if (item.ltCol != null)
                    {
                        foreach (var col in item.ltCol)
                        {
                            col.COLUMN_NAME = CodeHelper.ConvertToPascal(col.COLUMN_NAME, "_");

                            CombineTypeAndLength(col);
                        }
                    }
                }
            }

            #endregion

            ExcelHelper.ExportTableInfo(ltTable, Config.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));

            bgwExport.ReportProgress(100, total);
        }

        private void bgwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void bgwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //打开文件夹
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        #endregion

        private void 导出表结构ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTable.Rows != null && dgvTable.Rows.Count != 0)
            {
                DataGridViewSelectedRowCollection selRows = dgvTable.SelectedRows;
                if (selRows.Count != 0)
                {
                    ltTable.Clear();
                    TableModel table = null;
                    foreach (DataGridViewRow item in selRows)
                    {
                        table = new TableModel();
                        table.TABLE_NAME = item.Cells[0].Value.ToString();
                        table.COMMENTS = item.Cells[1].Value.ToString();
                        ltTable.Add(table);
                    }
                    //导出
                    if (!bgwExport.IsBusy)
                    {
                        this.bgwExport.RunWorkerAsync();
                        bgwExport.InitializeLifetimeService();
                    }
                }
                else
                {
                    MessageBox.Show("先选选择需要导出的数据!");
                }
            }
            else
            {
                MessageBox.Show("无数据!");
            }
        }

        private void 查看数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTable.Rows != null && dgvTable.Rows.Count != 0)
            {
                DataGridViewSelectedRowCollection selRows = dgvTable.SelectedRows;
                if (selRows.Count != 0)
                {
                    frmSQLWindow frm = new frmSQLWindow();
                    frm.strSQL = string.Format("SELECT * FROM {0} WHERE ROWNUM < 10", selRows[0].Cells[0].Value);
                    frm.Show();
                }
            }
            else
            {
                MessageBox.Show("无数据!");
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (dgvCol.Rows.Count > 0)
            {
                UpdateInfo();
                fileType = "html";

                ColModel keyCol = ltCol.Find(o => o.ISPK == "1");
                if (keyCol == null)
                {
                    MessageBox.Show(TableName + "缺少主键!");
                    return;
                }
                if (ltCol != null && ltCol.Count > 0)
                {
                    ltCol.ForEach(o =>
                    {

                        if (!string.IsNullOrEmpty(o.COMMENTS) && (o.COMMENTS.Contains(":") || o.COMMENTS.Contains("：")))
                        {
                            o.COMMENTS = o.COMMENTS.Replace("：", ":").Split(':')[0];
                            if (string.IsNullOrEmpty(o.codeTypeComments))
                            {
                                o.codeTypeComments = o.COMMENTS;
                            }
                        }
                    });
                }

                List<ColModel> ltTemp = ltCol.FindAll(o => o.Edit == true);
                strCode = CodeHelper.GetInfo(TableComments, CodeHelper.ConvertToCamel(TableName, "_"), ltTemp, keyCol);
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("无信息！");
            }
        }


    }


}
