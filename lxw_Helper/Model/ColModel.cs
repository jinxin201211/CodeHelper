using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lxw_Helper.Model
{
    /// <summary>
    /// 列实体
    /// </summary>
    public class ColModel
    {
        public bool List { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool qCondition { get; set; }

        string _COLUMN_NAME;
        /// <summary>
        /// 列名
        /// </summary>
        public string COLUMN_NAME
        {
            get { return _COLUMN_NAME; }
            set { _COLUMN_NAME = value; }
        }

        string _DATA_TYPE;

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DATA_TYPE
        {
            get { return _DATA_TYPE; }
            set { _DATA_TYPE = value; }
        }
        string _COMMENTS;
        /// <summary>
        /// 注释
        /// </summary>
        public string COMMENTS
        {
            get { return _COMMENTS; }
            set { _COMMENTS = value; }
        }
        string _NULLABLE;
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public string NULLABLE
        {
            get { return _NULLABLE; }
            set { _NULLABLE = value; }
        }
        string _ISPK;
        /// <summary>
        /// 是否为主键
        /// </summary>
        public string ISPK
        {
            get { return _ISPK; }
            set { _ISPK = value; }
        }
        string _CONSTRAINT_NAME;
        /// <summary>
        /// 主键名称
        /// </summary>
        public string CONSTRAINT_NAME
        {
            get { return _CONSTRAINT_NAME; }
            set { _CONSTRAINT_NAME = value; }
        }

        private string _CSharpData_Type;
        /// <summary>
        /// C#对应的类型
        /// </summary>
        public string CSharpData_Type
        {
            get { return _CSharpData_Type; }
            set { _CSharpData_Type = value; }
        }

        decimal _DATA_LENGTH;
        /// <summary>
        /// 数据长度
        /// </summary>
        public decimal DATA_LENGTH
        {
            get { return _DATA_LENGTH; }
            set { _DATA_LENGTH = value; }
        }



        public string codeType { get; set; }
        public string codeTypeComments { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public String DATA_DEFAULT { get; set; }


        public decimal CHAR_LENGTH { get; set; }
        public decimal? DATA_PRECISION { get; set; }
        public decimal? DATA_SCALE { get; set; }

        /// <summary>
        /// 字段小写形式
        /// </summary>
        public string ColumnName_Lower { get; set; }

        /// <summary>
        /// 字段Camel形式
        /// </summary>
        public string ColumnName_Camel { get; set; }

        /// <summary>
        /// 字段Pascal形式
        /// </summary>
        public string ColumnName_Pascal { get; set; }

        /// <summary>
        /// 数据长度扩展 （主要处理decimal类型长度）
        /// </summary>
        public decimal DATA_LENGTH_Ex { get; set; }



    }
}
