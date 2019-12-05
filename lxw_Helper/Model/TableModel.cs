using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lxw_Helper.Model
{
    public class TableModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public String TABLE_NAME { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        public String COMMENTS { get; set; }

        /// <summary>
        /// 列信息
        /// </summary>
        public List<ColModel> ltCol { get; set; }

    }
}
