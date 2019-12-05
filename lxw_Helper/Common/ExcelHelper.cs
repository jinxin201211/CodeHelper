using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using lxw_Helper.Model;
using System.Windows.Forms;
using NPOI.SS.Util;

namespace lxw_Helper.Common
{
    public class ExcelHelper
    {

        /// <summary>
        /// 获取Byte删除文件
        /// </summary>
        /// <param name="downloadPath"></param>
        /// <returns></returns>
        private static byte[] GetByteThenDelteFile(string downloadPath)
        {
            FileStream fs = new FileStream(downloadPath, FileMode.Open);
            long size = fs.Length;//获取文件大小
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);//将文件读到byte数组中
            fs.Close();
            //删除文件
            if (System.IO.File.Exists(downloadPath))
            {
                System.IO.File.Delete(downloadPath);//存在文件 删除
            }
            return array;
        }

        //参数说明
        //第一个:指定操作的Sheet。
        //第二个:指定在第几行指入（插入行的位置）
        //第三个:指定要插入多少行
        //第四个:源单元格格式的行，
        private static void MyInsertRow(HSSFSheet sheet, int insertRowIndex, int insertCount, HSSFRow sourceStyle)
        {
            #region 批量移动行
            sheet
            .ShiftRows
            (
            insertRowIndex,                                 //--开始行
            sheet
            .LastRowNum,                            //--结束行
            insertCount,                             //--移动大小(行数)--往下移动
            true,                                   //是否复制行高
            false,                                  //是否重置行高
            true                                    //是否移动批注
            );
            #endregion

            #region 对批量移动后空出的空行插，创建相应的行，并以插入行的上一行为格式源(即：插入行-1的那一行)
            for (int i = insertRowIndex; i < insertRowIndex + insertCount - 1; i++)
            {
                IRow targetRow = null;
                NPOI.SS.UserModel.ICell sourceCell = null;
                NPOI.SS.UserModel.ICell targetCell = null;

                targetRow = sheet.CreateRow(i + 1);

                for (int m = sourceStyle.FirstCellNum; m < sourceStyle.LastCellNum; m++)
                {
                    sourceCell = sourceStyle.GetCell(m);
                    if (sourceCell == null)
                    { continue; }
                    targetCell = targetRow.CreateCell(m);
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }

            IRow firstTargetRow = sheet.GetRow(insertRowIndex);
            ICell firstSourceCell = null;
            ICell firstTargetCell = null;

            for (int m = sourceStyle.FirstCellNum; m < sourceStyle.LastCellNum; m++)
            {
                firstSourceCell = sourceStyle.GetCell(m);
                if (firstSourceCell == null)
                { continue; }
                firstTargetCell = firstTargetRow.CreateCell(m);
                firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                firstTargetCell.SetCellType(firstSourceCell.CellType);
            }
            #endregion
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        #region 导出表结构

        public static string ExportTableInfo(List<TableModel> ltTableInfo, string fileName)
        {
            int tableConut = 0;

            string excelPath = Application.StartupPath + @"\Template\表结构.xls";
            HSSFWorkbook hssfworkbook;
            //读入复制的要导出的excel文件
            using (FileStream file = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
                file.Close();
            }
            HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.GetSheetAt(0);


            #region 生成表目录

            int index = 1;//从第1行开始写入
            for (int i = 0; i < ltTableInfo.Count; i++)
            {
                if (index >= 2)
                {
                    HSSFRow mySourceStyleRow = (HSSFRow)sheet1.GetRow(index - 1);//获取源格式行
                    MyInsertRow(sheet1, index, 1, mySourceStyleRow);//插入格式
                }
                sheet1.GetRow(index).GetCell(0).SetCellValue((i + 1).ToString());//序号
                sheet1.GetRow(index).GetCell(1).SetCellValue(ltTableInfo[i].TABLE_NAME);//英文表名
                sheet1.GetRow(index).GetCell(2).SetCellValue(ltTableInfo[i].COMMENTS);//说明
                index = index + 1;
            }



            #endregion

            #region 生成表结构

            index = 1;
            HSSFSheet sheet2 = (HSSFSheet)hssfworkbook.GetSheetAt(1);

            HSSFRow mySourceStyleRow0 = (HSSFRow)sheet2.GetRow(0);//获取源格式行
            HSSFRow mySourceStyleRow1 = (HSSFRow)sheet2.GetRow(1);//获取源格式行
            HSSFRow mySourceStyleRow2 = (HSSFRow)sheet2.GetRow(2);//获取源格式行
            HSSFRow mySourceStyleRow3 = (HSSFRow)sheet2.GetRow(3);//获取源格式行

            foreach (var item in ltTableInfo)
            {
                tableConut = tableConut + 1;
                //表信息
                if (index > 1)
                {
                    #region 表头
                    index = index + 1;
                    SetCellRangeAddress(sheet2, index, index, 0, 1);
                    SetCellRangeAddress(sheet2, index, index, 2, 6);
                    sheet2.GetRow(index).GetCell(0).SetCellValue("表名");//表名
                    sheet2.GetRow(index).GetCell(2).SetCellValue("表说明");//表说明
                    index = index + 1;
                    SetCellRangeAddress(sheet2, index, index, 0, 1);
                    SetCellRangeAddress(sheet2, index, index, 2, 6);
                    sheet2.GetRow(index).GetCell(0).SetCellValue(item.TABLE_NAME);//表名
                    sheet2.GetRow(index).GetCell(2).SetCellValue(item.COMMENTS);//表说明
                    index = index + 1;
                    sheet2.GetRow(index).GetCell(0).SetCellValue("列名");
                    sheet2.GetRow(index).GetCell(1).SetCellValue("类型");
                    sheet2.GetRow(index).GetCell(2).SetCellValue("是否可空");
                    sheet2.GetRow(index).GetCell(3).SetCellValue("默认值");
                    sheet2.GetRow(index).GetCell(4).SetCellValue("");
                    sheet2.GetRow(index).GetCell(5).SetCellValue("注释");
                    sheet2.GetRow(index).GetCell(6).SetCellValue("是否主键");
                    index = index + 1;
                    #endregion
                }
                else
                {
                    sheet2.GetRow(index).GetCell(0).SetCellValue(item.TABLE_NAME);//表名
                    sheet2.GetRow(index).GetCell(2).SetCellValue(item.COMMENTS);//表说明
                    index = index + 2;
                }

                if (item.ltCol != null && item.ltCol.Count > 0)
                {
                    //列信息
                    foreach (var col in item.ltCol)
                    {
                        if (index >= 4)
                        {
                            MyInsertRow(sheet2, index, 1, mySourceStyleRow3);//插入格式
                        }
                        #region 列
                        //列名
                        sheet2.GetRow(index).GetCell(0).SetCellValue(col.COLUMN_NAME);
                        //类型
                        sheet2.GetRow(index).GetCell(1).SetCellValue(col.DATA_TYPE);
                        //是否可空
                        sheet2.GetRow(index).GetCell(2).SetCellValue(col.NULLABLE);
                        //默认值
                        sheet2.GetRow(index).GetCell(3).SetCellValue(col.DATA_DEFAULT);
                        //空列
                        sheet2.GetRow(index).GetCell(4).SetCellValue("");
                        //注释");
                        sheet2.GetRow(index).GetCell(5).SetCellValue(col.COMMENTS);
                        //是否主键
                        if (col.ISPK == "1")
                        {
                            sheet2.GetRow(index).GetCell(6).SetCellValue("Y");
                        }
                        #endregion
                        index = index + 1;
                    }
                    MyInsertRow(sheet2, index, 1, mySourceStyleRow3);//插入格式
                    SetCellRangeAddress(sheet2, index, index, 0, 6);
                    index = index + 1;
                }

                if (tableConut < ltTableInfo.Count)
                {
                    //拷贝样式
                    MyInsertRow(sheet2, index, 1, mySourceStyleRow2);//插入格式
                    MyInsertRow(sheet2, index, 1, mySourceStyleRow1);//插入格式
                    MyInsertRow(sheet2, index, 1, mySourceStyleRow0);//插入格式
                    index = index - 1;
                }
             
               
            }
            #endregion

            //删除最后一行
            IRow row = sheet2.GetRow(index);
            sheet2.RemoveRow(row);

            string downloadPath = Application.StartupPath + @"\AutoCode" + "//" + fileName + ".xls"; ;
            //写入文件
            FileStream tempFile = new FileStream(downloadPath, FileMode.Create);
            hssfworkbook.Write(tempFile);

            tempFile.Close();
            return downloadPath;
        }





        #endregion
    }
}
