using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using lxw_Helper.Model;

namespace lxw_Helper.Common
{
    public static class DataChange
    {
        /// <summary>
        /// DataTable转换成泛类型List扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    T s = System.Activator.CreateInstance<T>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        PropertyInfo info = plist.Find(p => p.Name.ToUpper() == dt.Columns[i].ColumnName);
                        if (info != null)
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                info.SetValue(s, item[i], null);
                            }
                        }
                    }
                    list.Add(s);
                }
            }
            return list;

        }

        /// <summary>
        /// 获取C#对应的类型
        /// </summary>
        /// <param name="lt"></param>
        /// <param name="dt"></param>
        public static void GetCSharpType(List<ColModel> lt, DataTable dt)
        {

            ColModel tempCol = null;
            string tempStr = "";
            if (dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    tempCol = lt.Find(o => o.COLUMN_NAME.ToUpper() == dt.Columns[i].ColumnName);
                    if (tempCol != null)
                    {
                        tempStr = dt.Columns[i].DataType.ToString();
                        tempCol.CSharpData_Type = tempStr.Substring(tempStr.LastIndexOf('.') + 1);
                        //tempCol.CSharpData_Type = tempStr;
                    }
                }
            }
        }
    }
}
