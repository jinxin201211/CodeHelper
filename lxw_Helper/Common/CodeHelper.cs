using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using lxw_Helper.Model;

namespace lxw_Helper.Common
{
    public class CodeHelper
    {

        /// <summary>
        /// 生成pojo
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="ltCol">列集合</param>
        /// <param name="strPackage">包</param>
        /// <param name="strNameSpace">命名空间</param>
        /// <returns></returns>
        public static string GetModel(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author)
        {
            StringBuilder sb = new StringBuilder();

            StringBuilder sbGetSet = new StringBuilder();

            sb.AppendLine("package " + strPackage + ".pojo;");
            sb.AppendLine("");
            sb.AppendLine("import org.springframework.format.annotation.DateTimeFormat;");
            sb.AppendLine("import java.util.Date;");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" * " + TableComments);
            sb.AppendLine(" * " + author);
            sb.AppendLine(" * " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine(" */");

            sb.AppendLine("public class " + ConvertToPascal(className, "_") + " {");


            foreach (var item in ltCol)
            {

                sb.AppendLine("   /**");
                sb.AppendLine("     * " + item.COMMENTS);
                sb.AppendLine("     */");
                if (item.DATA_TYPE == "DATE")
                {
                    sb.AppendLine("    @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                }
                sb.AppendLine("    private " + GetJavaType(item) + " " + item.ColumnName_Camel + ";");
                sb.AppendLine(" ");

                //get  
                sbGetSet.AppendLine("    public " + GetJavaType(item) + " " + "get" + item.ColumnName_Pascal + "() {");
                sbGetSet.AppendLine("        return " + item.ColumnName_Camel + ";");
                sbGetSet.AppendLine("    }");
                sbGetSet.AppendLine(" ");
                //set
                sbGetSet.AppendLine("    public void " + "set" + item.ColumnName_Pascal + "(" + GetJavaType(item) + " " + item.ColumnName_Camel + ") {");
                sbGetSet.AppendLine("        this." + item.ColumnName_Camel + " = " + item.ColumnName_Camel + ";");
                sbGetSet.AppendLine("    }");
                sbGetSet.AppendLine(" ");
            }

            sb.Append(sbGetSet.ToString());
            sb.AppendLine("}");

            return sb.ToString();

        }

        public static string GetDao(string key, string TableName, string TableComments, string author)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Dao.txt");
            sb.AppendFormat(template
                   , TableComments + " add by " + author + " " + DateTime.Now.ToString("yyyy-MM-dd")
                   , TableName + "Dao"
                   , TableName + "Dto"
                   , TableName
                   , key
                );
            return sb.ToString();
        }

        public static string GetController(string TableComments, string TableName, string ControllerName, ColModel keyModel, string author, List<ColModel> ltQueryCon)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Controller.txt");
            StringBuilder sbConvert = new StringBuilder();

            if (ltQueryCon != null && ltQueryCon.Count > 0)
            {
                sbConvert.AppendLine("            LtDto.ForEach(o =>");
                sbConvert.AppendLine("            {");
                //代码转换处理
                foreach (var item in ltQueryCon)
                {
                    if (item.codeType != "")
                    {
                        if (item.CSharpData_Type == "String")
                        {
                            sbConvert.AppendLine("                o." + item.COLUMN_NAME + " = WebCommon.GetNameByValue(" + item.codeType + ", o." + item.COLUMN_NAME + ");");

                        }
                        else
                        {
                            sbConvert.AppendLine("                 //o." + item.COLUMN_NAME + " = WebCommon.GetNameByValue(" + item.codeType + ", o." + item.COLUMN_NAME + ");");

                        }
                    }
                }
                sbConvert.AppendLine("            });");
            }


            sb.AppendFormat(template
                   , TableComments + " add by " + author + " " + DateTime.Now.ToString("yyyy-MM-dd")
                   , TableName + "Dto"
                   , TableName + "Dao"
                   , ControllerName
                   , keyModel.COLUMN_NAME
                   , sbConvert.ToString()
                );
            return sb.ToString();
        }

        public static string GetIndex(string TableComments, string TableName, List<ColModel> ltCol, ColModel keyModel, List<ColModel> ltQueryCon)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Index.txt");
            StringBuilder timeToolbar = new StringBuilder();

            int addHeight = 120;
            int editHeight = 120;
            int infoHeight = 120;

            infoHeight = infoHeight + 33 * (ltCol.Count - 1);

            List<ColModel> ltTemp = ltCol.FindAll(o => o.Add == true);
            if (ltTemp != null)
            {
                addHeight = addHeight + 40 * (ltTemp.Count - 1);
            }

            ltTemp = ltCol.FindAll(o => o.Edit == true);
            if (ltTemp != null)
            {
                editHeight = editHeight + 40 * (ltTemp.Count - 1);
            }

            #region

            StringBuilder qCondition = new StringBuilder();//查询条件
            StringBuilder selectInit = new StringBuilder();//初始化下拉列表

            foreach (var item in ltQueryCon)
            {
                //下拉列表处理
                if (item.codeType != "")
                {
                    //<span>参数类别：
                    //    <select  class="select_search w160" id="TYPE_ID"  onchange="GetData()">
                    //    </select>
                    //</span>
                    qCondition.AppendLine("        <span>" + item.COMMENTS + "：");
                    qCondition.AppendLine("            <select data-lw-query=\"" + item.ColumnName_Camel + "\" class=\"select_search w160\" id=\"" + item.COLUMN_NAME + "\" onchange=\"GetData()\"  ></select>");
                    qCondition.AppendLine("        </span>");

                    //lw_GetSelect('/common/getAllSysType', "", "TYPE_ID", "--请选择--", "参数类别", "");
                    selectInit.AppendLine("            lw_GetSelect('/common/getSysCodeByTid', { tid: " + item.codeType + " }, '" + item.COLUMN_NAME + "', '--请选择--', '" + item.codeTypeComments + "', '');");
                }
                else if (item.CSharpData_Type == "DateTime")
                {
                    qCondition.AppendLine("        <span>" + item.COMMENTS + "：");
                    qCondition.AppendLine("              <input data-lw-query=\"sdate_" + item.COLUMN_NAME + "\" id=\"txtsdate_" + item.COLUMN_NAME + "\" class=\"Wdate inputtext w160\"  style=\"width: 90px;\" type=\"text\" onclick=\"WdatePicker({dateFmt: 'yyyy-MM-dd',maxDate:'#F{$(\\'#txtedate_" + item.COLUMN_NAME + "\\').val()}',maxDate:new Date() });\" value=\"\" />");
                    qCondition.AppendLine("        ~");
                    qCondition.AppendLine("              <input data-lw-query=\"edate_" + item.COLUMN_NAME + "\" id=\"txtedate_" + item.COLUMN_NAME + "\" class=\"Wdate inputtext w160\"  style=\"width: 90px;\" type=\"text\" onclick=\"WdatePicker({dateFmt: 'yyyy-MM-dd',minDate:'#F{$(\\'#txtsdate_" + item.COLUMN_NAME + "\\').val()}',maxDate: new Date() });\" value=\"\" />");
                    qCondition.AppendLine("        </span>");

                }
                else
                {
                    //<span>参数名称：
                    //<input class="inputtext w160" id="CNAME" name="CNAME" type="text" />
                    //</span>
                    qCondition.Append("        <span>" + item.COMMENTS + "：");
                    qCondition.Append("            <input  data-lw-query=\"" + item.COLUMN_NAME + "\" class=\"inputtext w160\" id=\"" + item.COLUMN_NAME + "\" type=\"text\" maxlength=\"" + (item.DATA_LENGTH + 1) + "\" />");
                    qCondition.Append("        </span>");

                }
            }

            #endregion

            StringBuilder cols = new StringBuilder();
            foreach (var item in ltCol)
            {

                item.COLUMN_NAME = item.ColumnName_Camel;

                if (item.ISPK == "1")
                {
                    continue;
                }
                if (item.codeType != "")
                {
                    cols.AppendLine("                           {field: '" + item.COLUMN_NAME + "', align: 'center', title: '" + item.COMMENTS + "', templet: function (d) { return getValueByType(" + item.codeType + ", d." + item.COLUMN_NAME + "); }  },");
                }
                else
                {
                    cols.AppendLine("                           {field: '" + item.COLUMN_NAME + "', align: 'center', title: '" + item.COMMENTS + "' },");
                }
            }

            sb.AppendFormat(template
                   , qCondition.ToString()//查询条件元素
                   , TableComments//表名
                   , cols.ToString()// 表格显示列名
                   , selectInit.ToString()//查询初始化处理的 下拉列表的数据源
                   , keyModel.COLUMN_NAME//主键名字
                   , TableName
                   , addHeight
                   , editHeight
                   , infoHeight
                );

            return sb.ToString();
        }

        public static string GetAdd(string TableComments, string TableName, List<ColModel> ltCol, ColModel keyModel)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Add.txt");

            StringBuilder tableTR = new StringBuilder();
            StringBuilder rules = new StringBuilder();
            StringBuilder rulesMsg = new StringBuilder();
            StringBuilder selectInit = new StringBuilder();//初始化下拉列表
            string temp = "";

            foreach (var item in ltCol)
            {
                item.COLUMN_NAME = item.ColumnName_Camel;

                if (item.ISPK == "1")//不生成主键字段
                {
                    continue;
                }

                tableTR.AppendLine("           <tr>");
                if (item.NULLABLE == "Y")
                {
                    tableTR.AppendLine("                <td class=\"right w116\">" + item.COMMENTS + "：</td>");
                }
                else
                {
                    tableTR.AppendLine("                <td class=\"right w116\">" + item.COMMENTS + "：<span class=\"color_red\">*</span></td>");
                }

                //下拉类别
                if (item.codeType != "")
                {
                    tableTR.AppendLine("                <td><select id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" class=\"select_w160\"></select></td>");
                    selectInit.AppendLine("        lw_GetSelect('/common/getSysCodeByTid', { tid: " + item.codeType + " }, '" + item.COLUMN_NAME + "', '--请选择--', '" + item.codeTypeComments + "', '');");
                }
                else
                {

                    //时间字段处理
                    if (item.CSharpData_Type == "DateTime")
                    {
                        tableTR.AppendLine("                <td><input class=\"inputtext w160\" id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" type=\"text\" maxlength=\"11\"   onclick=\"WdatePicker({dateFmt: 'yyyy-MM-dd'});\" value=\"\" /></td>");
                    }
                    else
                    {
                        tableTR.AppendLine("                <td><input class=\"inputtext w160\" id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" type=\"text\" maxlength=\"" + (item.DATA_LENGTH + 1) + "\" /></td>");
                    }
                }

                tableTR.AppendLine("           </tr>");

                rules.AppendLine("                " + item.COLUMN_NAME + ": {");
                rulesMsg.AppendLine("                " + item.COLUMN_NAME + ": {");
                //必填规则
                if (item.NULLABLE == "N")
                {
                    rules.AppendLine("                    required: true,");
                    rulesMsg.AppendLine("                    required: \"请输入" + item.COMMENTS + "\",");
                }

                //时间字段处理
                if (item.CSharpData_Type == "DateTime")
                {
                    //长度限制规则
                    rules.AppendLine("                    maxlength: " + 10 + ",");
                    rulesMsg.AppendLine("                    maxlength: \"最大长度10\",");
                }
                else
                {
                    //长度限制规则
                    rules.AppendLine("                    maxlength: " + item.DATA_LENGTH + ",");
                    rulesMsg.AppendLine("                    maxlength: \"最大长度" + item.DATA_LENGTH + "\",");
                }

                rules.AppendLine("                },");
                rulesMsg.AppendLine("                },");

                //去除末尾逗号
                temp = rules.ToString();
                rules.Clear();
                rules.Append(temp.Replace(",\r\n                },\r\n", "\r\n                },\r\n"));

                temp = rulesMsg.ToString();
                rulesMsg.Clear();
                rulesMsg.Append(temp.Replace(",\r\n                },\r\n", "\r\n                },\r\n"));

            }

            //去除末尾逗号
            temp = rules.ToString();
            rules.Clear();
            rules.Append(temp.Remove(temp.Length - 3));

            temp = rulesMsg.ToString();
            rulesMsg.Clear();
            rulesMsg.Append(temp.Remove(temp.Length - 3));


            sb.AppendFormat(template
                   , TableComments
                   , TableName
                   , tableTR.ToString()
                   , rules.ToString()
                   , rulesMsg.ToString()
                   , selectInit.ToString()
                );
            return sb.ToString();
        }

        public static string GetEdit(string TableComments, string TableName, List<ColModel> ltCol, ColModel keyModel)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Edit.txt");

            StringBuilder tableTR = new StringBuilder();
            StringBuilder rules = new StringBuilder();
            StringBuilder rulesMsg = new StringBuilder();
            StringBuilder selectInit = new StringBuilder();//初始化下拉列表
            string temp = "";

            foreach (var item in ltCol)
            {
                item.COLUMN_NAME = item.ColumnName_Camel;

                if (item.ISPK == "1")
                {
                    continue;
                }

                tableTR.AppendLine("           <tr>");

                if (item.NULLABLE == "Y")
                {
                    tableTR.AppendLine("                <td class=\"right w116\">" + item.COMMENTS + "：</td>");
                }
                else
                {
                    tableTR.AppendLine("                <td class=\"right w116\">" + item.COMMENTS + "：<span class=\"color_red\">*</span></td>");
                }

                //下拉类别
                if (item.codeType != "")
                {
                    tableTR.AppendLine("                <td><select id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" class=\"select_w160\"></select>");
                    tableTR.AppendLine("                    <input type=\"hidden\" id=\"hid" + item.COLUMN_NAME + "\"  value=\"\"/></td>");

                    selectInit.AppendLine("                else if (key == \"" + item.COLUMN_NAME + "\") {");
                    selectInit.AppendLine("                    document.forms[0][\"hid\" + key ].value = n;");
                    selectInit.AppendLine("                    lw_GetSelect('/common/getSysCodeByTid', { tid: " + item.codeType + " }, '" + item.COLUMN_NAME + "', '', '" + item.codeTypeComments + "', $(\"#hid" + item.COLUMN_NAME + "\").val());");
                    selectInit.AppendLine("                }");
                }
                else
                {
                    //时间字段处理
                    if (item.CSharpData_Type == "DateTime")
                    {
                        tableTR.AppendLine("                <td><input class=\"inputtext w160\" id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" value=\"\" onclick=\"WdatePicker({dateFmt: 'yyyy-MM-dd'});\" type=\"text\" maxlength=\"11\" /></td>");
                    }
                    else
                    {
                        tableTR.AppendLine("                <td><input class=\"inputtext w160\" id=\"" + item.COLUMN_NAME + "\" name=\"" + item.COLUMN_NAME + "\" value=\"\" type=\"text\" maxlength=\"" + (item.DATA_LENGTH + 1) + "\" /></td>");
                    }
                }
                tableTR.AppendLine("           </tr>");


                rules.AppendLine("                " + item.COLUMN_NAME + ": {");
                rulesMsg.AppendLine("                " + item.COLUMN_NAME + ": {");
                //必填规则
                if (item.NULLABLE == "N")
                {
                    rules.AppendLine("                    required: true,");
                    rulesMsg.AppendLine("                    required: \"请输入" + item.COMMENTS + "\",");
                }
                //时间字段处理
                if (item.CSharpData_Type == "DateTime")
                {
                    //长度限制规则
                    rules.AppendLine("                    maxlength: " + 10 + ",");
                    rulesMsg.AppendLine("                    maxlength: \"最大长度10\",");
                }
                else
                {
                    //长度限制规则
                    rules.AppendLine("                    maxlength: " + item.DATA_LENGTH + ",");
                    rulesMsg.AppendLine("                    maxlength: \"最大长度" + item.DATA_LENGTH + "\",");
                }

                rules.AppendLine("                },");
                rulesMsg.AppendLine("                },");

                //去除末尾逗号
                temp = rules.ToString();
                rules.Clear();
                rules.Append(temp.Replace(",\r\n                },\r\n", "\r\n                },\r\n"));

                temp = rulesMsg.ToString();
                rulesMsg.Clear();
                rulesMsg.Append(temp.Replace(",\r\n                },\r\n", "\r\n                },\r\n"));

            }

            //去除末尾逗号
            temp = rules.ToString();
            rules.Clear();
            rules.Append(temp.Remove(temp.Length - 3));

            temp = rulesMsg.ToString();
            rulesMsg.Clear();
            rulesMsg.Append(temp.Remove(temp.Length - 3));

            //去除第一个else
            temp = selectInit.ToString().Trim();
            selectInit.Clear();
            if (!string.IsNullOrEmpty(temp))
            {
                selectInit.Append("              " + temp.Remove(0, 5));
                selectInit.Append(@"                   else {
                    if (document.forms[0][key]) {
                        document.forms[0][key].value = n;
                    }
                }");
            }
            else
            {
                selectInit.Append(@"                if (document.forms[0][key]) {
                    document.forms[0][key].value = n;
                } ");
            }

            sb.AppendFormat(template
                   , TableComments
                   , TableName
                   , keyModel.COLUMN_NAME
                   , tableTR.ToString()
                   , rules.ToString()
                   , rulesMsg.ToString()
                   , selectInit.ToString()
                );
            return sb.ToString();
        }

        public static string GetInfo(string TableComments, string TableName, List<ColModel> ltCol, ColModel keyModel)
        {
            StringBuilder sb = new StringBuilder();
            string template = File.ReadAllText(Application.StartupPath + @"\Template\Info.txt");

            StringBuilder tableTR = new StringBuilder();
            StringBuilder setValue = new StringBuilder();
            string temp = "";

            List<ColModel> ltNeedConvertCol = ltCol.FindAll(o => o.codeType != "");

            foreach (var item in ltCol)
            {
                item.COLUMN_NAME = item.ColumnName_Camel;
                if (item.ISPK == "1") continue;
                tableTR.AppendLine("           <tr>");
                tableTR.AppendLine("                <td class=\"right w116\">" + item.COMMENTS + "：</td>");
                tableTR.AppendLine("                <td id=\"td_" + item.COLUMN_NAME + "\"></td>");
                tableTR.AppendLine("           </tr>");
            }

            if (ltNeedConvertCol != null && ltNeedConvertCol.Count > 0)
            {
                foreach (var item in ltNeedConvertCol)
                {
                    setValue.AppendLine("                else if(key == \"" + item.COLUMN_NAME + "\"){");
                    setValue.AppendLine("                   $(\"#td_" + item.COLUMN_NAME + "\").html(getValueByType(" + item.codeType + ",n));");
                    setValue.AppendLine("                }");
                }

                setValue.AppendLine("else{");
                setValue.AppendLine("   $(\"#td_\"+key).html(n);");
                setValue.AppendLine("}");

            }
            else
            {
                setValue.AppendLine("$(\"#td_\"+key).html(n);");
            }


            temp = setValue.ToString().Trim();
            if (!string.IsNullOrEmpty(temp) && temp.StartsWith("else"))
            {
                temp = temp.Remove(0, 5);
            }

            sb.AppendFormat(template
                   , TableComments
                   , TableName
                   , keyModel.COLUMN_NAME
                   , tableTR.ToString()
                   , temp
                );
            return sb.ToString();
        }

        /// <summary>
        /// 生成Mapper
        /// </summary>
        public static string GetMapper(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author, ColModel keyCol)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("package " + strPackage + ".mapper;");
            sb.AppendLine("");
            sb.AppendLine("import " + strPackage + ".pojo." + ConvertToPascal(className, "_") + ";");
            sb.AppendLine("import java.util.List;");
            sb.AppendLine("import java.util.Map;");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" * " + TableComments + "Mapper");
            sb.AppendLine(" * " + author);
            sb.AppendLine(" * " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine(" */");

            className = ConvertToPascal(className, "_");

            sb.AppendLine("public interface " + className + "Mapper {");


            sb.AppendFormat(@"    /**
     *全字段插入
     */
    int insert({0} record);

    /**
     *对非空字段插入
     */
    int insertSelective({0} record);

    /**
     *全字段更新
     */
    int updateByPrimaryKeySelective({0} record);

    /**
     *对非空字段更新
     */
    int updateByPrimaryKey({0} record);

    /**
     *通过主键字段删除
     */
    int deleteByPrimaryKey({1} {2});

    /**
     *通过主键字段查询
     */
    {0} selectByPrimaryKey({1} {2});

    /**
     *获取数据
     */
    List<{0}> selectAllByMap(Map map);"
                , className
                , GetJavaType(keyCol)
                , keyCol.ColumnName_Camel
                );

            sb.AppendLine("}");

            return sb.ToString();

        }

        /// <summary>
        /// 生成Mapper.xml
        /// </summary>
        public static string GetMapperXML(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author, ColModel keyCol)
        {

            List<ColModel> ltQueryCon = ltCol.FindAll(o => o.qCondition == true);

            StringBuilder sb = new StringBuilder();
            string classNamePascal = ConvertToPascal(className, "_");

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.AppendLine("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            sb.AppendLine("<mapper namespace=\"" + strPackage + ".mapper." + classNamePascal + "Mapper\" >");


            #region BaseResultMap
            sb.AppendLine("<resultMap id=\"BaseResultMap\" type=\"" + strPackage + ".pojo." + classNamePascal + "\" >");



            foreach (var item in ltCol)
            {
                if (item.ISPK == "1")
                {
                    sb.AppendLine(string.Format("   <id column=\"{0}\" property=\"{1}\" jdbcType=\"{2}\" />"
                                                            , item.COLUMN_NAME
                                                            , item.ColumnName_Camel
                                                            , GetJDBCType(item)
                                                            ));
                }
                else
                {
                    sb.AppendLine(string.Format("   <result column=\"{0}\" property=\"{1}\" jdbcType=\"{2}\" />"
                                        , item.COLUMN_NAME
                                        , item.ColumnName_Camel
                                        , GetJDBCType(item)
                                        ));
                }


            }

            sb.AppendLine("</resultMap>");
            #endregion

            #region Base_Column_List
            sb.AppendLine("<sql id=\"Base_Column_List\" >");
            string Base_Column_List = "    ";
            foreach (var item in ltCol)
            {
                Base_Column_List += item.COLUMN_NAME + ",";
            }
            sb.AppendLine(Base_Column_List.TrimEnd(','));
            sb.AppendLine("</sql>");
            #endregion

            #region selectByPrimaryKey
            sb.AppendLine("<select id=\"selectByPrimaryKey\" resultMap=\"BaseResultMap\" parameterType=\"java.lang." + GetJavaType(keyCol) + "\" > ");
            sb.AppendLine("    select  ");
            sb.AppendLine("    <include refid=\"Base_Column_List\" /> ");
            sb.AppendLine("    from  " + className);
            sb.AppendLine("    where " + keyCol.COLUMN_NAME + " = #{" + keyCol.ColumnName_Camel + ",jdbcType=" + GetJDBCType(keyCol) + "} ");
            sb.AppendLine("</select> ");
            #endregion

            #region selectAllByMap
            sb.AppendLine("<select id=\"selectAllByMap\" resultMap=\"BaseResultMap\" parameterType=\"java.util.Map\">");
            sb.AppendLine("    select");
            sb.AppendLine("    <include refid=\"Base_Column_List\" />");
            sb.AppendLine("    from " + className);

            if (ltQueryCon != null && ltQueryCon.Count > 0)
            {
                sb.AppendLine("    <where>");
                foreach (var item in ltQueryCon)
                {
                    if (item.DATA_TYPE == "DATE")//时间类型处理
                    {   // sdate   edate
                        sb.AppendLine(string.Format("      <if test=\"edate_{0} != null and edate_{0} !=''\">AND {1} &lt;= to_date(#{{edate_{0}}}||' 23:59:59','yyyy-mm-dd hh24:mi:ss')</if>", item.ColumnName_Camel, item.COLUMN_NAME));
                        sb.AppendLine(string.Format("      <if test=\"sdate_{0} != null and sdate_{0} !=''\">AND {1} &gt;= to_date(#{{sdate_{0}}}||' 00:00:00','yyyy-mm-dd hh24:mi:ss')</if>", item.ColumnName_Camel, item.COLUMN_NAME));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("      <if test=\"{0} != null and {0} !=''\">AND {1} = #{{{0}}}</if>", item.ColumnName_Camel, item.COLUMN_NAME));
                    }
                }
                sb.AppendLine("    </where>");
            }


            sb.AppendLine("</select>");
            #endregion

            #region deleteByPrimaryKey
            sb.AppendLine("<delete id=\"deleteByPrimaryKey\"  parameterType=\"java.lang." + GetJavaType(keyCol) + "\" > ");
            sb.AppendLine("    delete  ");
            sb.AppendLine("    from  " + className);
            sb.AppendLine("    where " + keyCol.COLUMN_NAME + " = #{" + keyCol.ColumnName_Camel + ",jdbcType=" + GetJDBCType(keyCol) + "} ");
            sb.AppendLine("</delete> ");
            #endregion

            #region insert
            sb.AppendLine("<insert id=\"insert\" parameterType=\"" + strPackage + ".pojo." + classNamePascal + "\" >");
            string cols = keyCol.COLUMN_NAME + ",";
            string values = "SEQ_" + className + ".NEXTVAL,";
            foreach (var item in ltCol)
            {
                if (item.ISPK == "1")
                {
                    continue;
                }
                cols += item.COLUMN_NAME + ",";
                values += string.Format("#{{{0},jdbcType={1}}},", item.ColumnName_Camel, GetJDBCType(item));
            }
            sb.AppendLine("    insert into " + className + " ( " + cols.TrimEnd(',') + " ) \r\n    values ( " + values.TrimEnd(',') + " )");
            sb.AppendLine("</insert>");
            #endregion

            #region insertSelective

            sb.AppendLine("<insert id=\"insertSelective\" parameterType=\"" + strPackage + ".pojo." + classNamePascal + "\" >");
            string cols2 = "      " + keyCol.COLUMN_NAME + ",";
            string values2 = "      SEQ_" + className + ".NEXTVAL,";

            foreach (var item in ltCol)
            {
                if (item.ISPK == "1")
                {
                    continue;
                }
                cols2 += string.Format("\r\n      <if test=\"{0} != null\" >{1},</if>", item.ColumnName_Camel, item.COLUMN_NAME);
                values2 += string.Format("\r\n      <if test=\"{0} != null\" >#{{{0},jdbcType={1}}},</if>", item.ColumnName_Camel, GetJDBCType(item));
            }
            sb.AppendLine("    insert into " + className);
            sb.AppendLine("    <trim prefix=\"(\" suffix=\")\" suffixOverrides=\",\" >");
            sb.AppendLine(cols2.TrimEnd(','));
            sb.AppendLine("    </trim>");
            sb.AppendLine("    <trim prefix=\"values (\" suffix=\")\" suffixOverrides=\",\" >");
            sb.AppendLine(values2.TrimEnd(','));
            sb.AppendLine("    </trim>");
            sb.AppendLine("</insert>");

            #endregion

            #region updateByPrimaryKey

            sb.AppendLine("<update id=\"updateByPrimaryKey\" parameterType=\"" + strPackage + ".pojo." + classNamePascal + "\" >");
            sb.AppendLine("     update " + className + " set");
            string sets = "";
            foreach (var item in ltCol)
            {
                if (item.ISPK == "1")
                {
                    continue;
                }
                if (sets == "")
                {
                    sets += string.Format("     {0} = #{{{1},jdbcType={2}}},", item.COLUMN_NAME, item.ColumnName_Camel, GetJDBCType(item));
                }
                else
                {
                    sets += string.Format(" \r\n     {0} = #{{{1},jdbcType={2}}},", item.COLUMN_NAME, item.ColumnName_Camel, GetJDBCType(item));
                }
            }


            sb.Append(sets.TrimEnd(','));
            sb.AppendLine(" \r\n     where " + keyCol.COLUMN_NAME + " = #{" + keyCol.ColumnName_Camel + ",jdbcType=" + GetJDBCType(keyCol) + "}");
            sb.AppendLine("</update>");

            #endregion

            #region updateByPrimaryKeySelective
            sb.AppendLine("<update id=\"updateByPrimaryKeySelective\" parameterType=\"" + strPackage + ".pojo." + classNamePascal + "\" >");
            sb.AppendLine("     update " + className);
            sb.AppendLine("     <set >");
            foreach (var item in ltCol)
            {
                if (item.ISPK == "1")
                {
                    continue;
                }
                sb.AppendLine(string.Format("       <if test=\"{0} != null\" > {2} = #{{{0},jdbcType={1}}},</if>", item.ColumnName_Camel, GetJDBCType(item), item.COLUMN_NAME));
            }
            sb.AppendLine("     </set>");
            sb.AppendLine("     where " + keyCol.COLUMN_NAME + " = #{" + keyCol.ColumnName_Camel + ",jdbcType=" + GetJDBCType(keyCol) + "}");
            sb.AppendLine("</update>");
            #endregion

            sb.Append("</mapper>");

            return sb.ToString();

        }

        /// <summary>
        /// 生成IService
        /// </summary>
        public static string GetIService(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author, ColModel keyCol)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("package " + strPackage + ".service;");
            sb.AppendLine("");
            sb.AppendLine("import " + strPackage + ".pojo." + ConvertToPascal(className, "_") + ";");
            sb.AppendLine("import com.github.pagehelper.PageInfo;");
            sb.AppendLine("import java.util.Map;");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" * " + TableComments + "Service");
            sb.AppendLine(" * " + author);
            sb.AppendLine(" * " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine(" */");

            className = ConvertToPascal(className, "_");

            sb.AppendLine("public interface I" + className + "Service {");


            sb.AppendFormat(@"
    /**
     * 新增
     */
    void add({0} record) throws Exception;

    /**
     * 修改
     */
    void update({0} record) throws Exception;

    /**
     * 删除
     */
    void delete({1} {2}) throws Exception;

    /**
     * 获取分页数据
     */
    PageInfo getList(Map map) throws Exception;

    /**
     * 获取实体
     */
    {0} getModel({1} {2}) throws Exception;
    
"
                , className
                , GetJavaType(keyCol)
                , keyCol.ColumnName_Camel
                );

            sb.AppendLine("}");

            return sb.ToString();

        }

        /// <summary>
        /// 生成ServiceImpl
        /// </summary>
        public static string GetServiceImpl(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author, ColModel keyCol)
        {
            StringBuilder sb = new StringBuilder();
            className = ConvertToPascal(className, "_");

            sb.AppendLine("package " + strPackage + ".service.impl;");
            sb.AppendLine("");
            sb.AppendLine("import " + strPackage + ".mapper." + className + "Mapper;");
            sb.AppendLine("import " + strPackage + ".pojo." + className + ";");
            sb.AppendLine("import " + strPackage + ".service.I" + className + "Service;");
            sb.AppendLine("");
            sb.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            sb.AppendLine("import org.springframework.stereotype.Service;");
            sb.AppendLine("import org.springframework.util.StringUtils;");
            sb.AppendLine("import com.github.pagehelper.PageHelper;");
            sb.AppendLine("import com.github.pagehelper.PageInfo;");
            sb.AppendLine("");
            sb.AppendLine("import java.util.List;");
            sb.AppendLine("import java.util.Map;");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine(" * " + TableComments + "ServiceImpl");
            sb.AppendLine(" * " + author);
            sb.AppendLine(" * " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine(" */");

            sb.AppendLine(" @Service");
            sb.AppendLine("public  class " + className + "ServiceImpl implements I" + className + "Service {");

            sb.AppendFormat(@"
    @Autowired
    private {0}Mapper {1}Mapper;

    /**
     *新增
     */
    @Override
    public void add({0} record) throws Exception {{
        {1}Mapper.insertSelective(record);
    }}
    
    /**
     *修改
     */
    @Override
    public void update({0} record) throws Exception {{
        {1}Mapper.updateByPrimaryKeySelective(record);
    }}
    
    /**
     *删除
     */
    @Override
    public void delete({2} {3}) throws Exception {{
        {1}Mapper.deleteByPrimaryKey({3});
    }}
    
    /**
     *获取实体
     */
    @Override
    public {0} getModel({2} {3}) throws Exception {{
        return {1}Mapper.selectByPrimaryKey({3});
    }}
    
"
                , className
                , FirstLetterToLower(className)
                , GetJavaType(keyCol)
                , keyCol.ColumnName_Camel
                );


            //getList
            sb.AppendLine("    /**");
            sb.AppendLine("     *获取分页数据");
            sb.AppendLine("     */");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public PageInfo getList(Map map) throws Exception {");
            sb.AppendLine("        int pageNum = 1;");
            sb.AppendLine("        int pageSize = 10;");
            sb.AppendLine("        if (!StringUtils.isEmpty(map.get(\"page\"))) {");
            sb.AppendLine("            pageNum = Integer.parseInt((String) map.get(\"page\"));");
            sb.AppendLine("        }");
            sb.AppendLine("        if (!StringUtils.isEmpty(map.get(\"limit\"))) {");
            sb.AppendLine("            pageSize = Integer.parseInt((String) map.get(\"limit\"));");
            sb.AppendLine("        }");
            sb.AppendLine("        PageHelper.startPage(pageNum, pageSize);");
            sb.AppendLine("        List<" + className + "> list = " + FirstLetterToLower(className) + "Mapper.selectAllByMap(map);");

            //值转化 前台实现

            //List<ColModel> ltTemp = ltCol.FindAll(o => o.codeType != "");
            //if (ltTemp != null && ltTemp.Count > 0)
            //{
            //    sb.AppendLine("        //值转化");
            //    sb.AppendLine("        /*for (" + className + " item : list) {");
            //    foreach (var item in ltTemp)
            //    {
            //        sb.AppendLine("            item." + ConvertToCamel("SET_" + item.COLUMN_NAME, "_") + "(CommonCache.getSysCode(" + item.codeType + "L, item." + ConvertToCamel("GET_" + item.COLUMN_NAME, "_") + "()));");
            //    }
            //    sb.AppendLine("        }*/");
            //}

            sb.AppendLine("        PageInfo pageInfo = new PageInfo(list);");
            sb.AppendLine("        return pageInfo;");
            sb.AppendLine("    }");

            sb.AppendLine("}");

            return sb.ToString();

        }

        /// <summary>
        /// 生成Controller
        /// </summary>
        public static string GetController(string className, List<ColModel> ltCol, string TableComments, string strPackage, string author, ColModel keyCol)
        {
            StringBuilder sb = new StringBuilder();
            className = ConvertToPascal(className, "_");

            sb.AppendLine("package " + strPackage + ".controller;");
            sb.AppendLine("");
            sb.AppendLine("import " + strPackage + ".comm.Response;");
            sb.AppendLine("import " + strPackage + ".pojo." + className + ";");
            sb.AppendLine("import " + strPackage + ".service.I" + className + "Service;");
            sb.AppendLine("");
            sb.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            sb.AppendLine("import org.springframework.web.bind.annotation.RequestMapping;");
            sb.AppendLine("import org.springframework.web.bind.annotation.RequestParam;");
            sb.AppendLine("import org.springframework.web.bind.annotation.RestController;");
            sb.AppendLine("import com.github.pagehelper.PageInfo;");
            sb.AppendLine("");
            sb.AppendLine("import java.util.Map;");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine(" * " + TableComments + "Controller");
            sb.AppendLine(" * " + author);
            sb.AppendLine(" * " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine(" */");

            string classNameFLTL = FirstLetterToLower(className);

            sb.AppendLine(" @RestController");
            sb.AppendLine(" @RequestMapping(\"/" + classNameFLTL + "\")");
            sb.AppendLine("public  class " + className + "Controller {");
            sb.AppendLine("");
            sb.AppendLine("@Autowired");
            sb.AppendLine("private I" + className + "Service " + classNameFLTL + "Service;");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" *获取分页数据");
            sb.AppendLine(" */");
            sb.AppendLine("@RequestMapping(\"/getData\")");
            sb.AppendLine("public Response list(@RequestParam Map map) throws Exception {");
            sb.AppendLine("    PageInfo pageInfo = " + classNameFLTL + "Service.getList(map);");
            sb.AppendLine("    return new Response().success(pageInfo.getList(), pageInfo.getTotal());");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" *获取实体");
            sb.AppendLine(" */");
            sb.AppendLine("@RequestMapping(\"/getOne\")");
            sb.AppendLine("public Response list(" + GetJavaType(keyCol) + " " + keyCol.ColumnName_Camel + ") throws Exception {");
            sb.AppendLine("    return new Response().success(" + classNameFLTL + "Service.getModel(" + keyCol.ColumnName_Camel + "));");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" *保存");
            sb.AppendLine(" */");
            sb.AppendLine("@RequestMapping(\"/add\")");
            sb.AppendLine("public Response Save(" + className + " record) throws Exception {");
            sb.AppendLine("    " + classNameFLTL + "Service.add(record);");
            sb.AppendLine("    return new Response().success();");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" *修改");
            sb.AppendLine(" */");
            sb.AppendLine("@RequestMapping(\"/edit\")");
            sb.AppendLine("public Response Edit(" + className + " record) throws Exception {");
            sb.AppendLine("    " + classNameFLTL + "Service.update(record);");
            sb.AppendLine("    return new Response().success();");
            sb.AppendLine("}");
            sb.AppendLine("");

            sb.AppendLine("/**");
            sb.AppendLine(" *删除");
            sb.AppendLine(" */");
            sb.AppendLine("@RequestMapping(\"/delete\")");
            sb.AppendLine("public Response remove(" + GetJavaType(keyCol) + " " + keyCol.ColumnName_Camel + ") throws Exception {");
            sb.AppendLine("    " + classNameFLTL + "Service.delete(" + keyCol.ColumnName_Camel + ");");
            sb.AppendLine("    return new Response().success();");
            sb.AppendLine("}");


            sb.AppendLine("}");

            return sb.ToString();

        }

        #region 代码规范风格化

        /// <summary>
        /// 转换为Camel风格-第一个单词小写，其后每个单词首字母大写
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldDelimiter">分隔符</param>
        /// <returns></returns>
        public static string ConvertToCamel(string fieldName, string fieldDelimiter)
        {
            string result = string.Empty;
            //全部小写
            string[] array = fieldName.ToLower().Split(fieldDelimiter.ToCharArray());
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {   //首单词小写
                    result += array[i].ToLower();
                }
                else
                {//首字母大写
                    result += array[i].Substring(0, 1).ToUpper() + array[i].Substring(1);
                }
            }

            return result;
        }

        /// <summary>
        /// 转换为Pascal风格-每个单词首字母大写
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldDelimiter">分隔符</param>
        /// <returns></returns>
        public static string ConvertToPascal(string fieldName, string fieldDelimiter)
        {
            string result = string.Empty;
            //全部小写
            string[] array = fieldName.ToLower().Split(fieldDelimiter.ToCharArray());
            for (int i = 0; i < array.Length; i++)
            {
                //首字母大写
                result += array[i].Substring(0, 1).ToUpper() + array[i].Substring(1);
            }
            return result;
        }

        public static string ConvertToPascal2(string fieldName, char fieldDelimiter)
        {
            string result = string.Empty;
            //全部小写
            string[] array = fieldName.ToLower().Split(fieldDelimiter);
            for (int i = 0; i < array.Length; i++)
            {
                //首字母大写
                result += array[i].Substring(0, 1).ToUpper() + array[i].Substring(1) + fieldDelimiter.ToString();
            }
            return result.TrimEnd(fieldDelimiter);
        }

        public static string FirstLetterToLower(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return "";

            }
            else
            {
                return fieldName.Substring(0, 1).ToLower() + fieldName.Substring(1);
            }
        }




        #endregion

        #region 类型转化
        public static string GetJavaType(ColModel col)
        {
            string R = "String";

            if (DataBaseHelper.DbType == 0)
            {
                if (col.DATA_TYPE == "DATE")
                {
                    R = "Date";
                }
                else if (col.DATA_TYPE == "NUMBER")
                {
                    R = "Long";
                }
                else if (col.DATA_TYPE == "BLOB")
                {
                    R = "byte[]";
                }
            }
            else if (DataBaseHelper.DbType == 1)
            {
                switch (col.DATA_TYPE.ToLower())
                {
                    case "int":
                    case "tinyint":
                    case "smallint":
                        R = "Integer";
                        break;
                    case "varchar":
                    case "char":
                    case "nchar":
                    case "nvarchar":
                    case "text":
                    case "ntext":
                    case "uniqueidentifier":
                    case "sql_variant":
                        R = "String";
                        break;
                    case "bit":
                        R = "Boolean";
                        break;
                    case "bigint":
                        R = "Long";
                        break;
                    case "float":
                        R = "Double";
                        break;
                    case "real":
                        R = "Float";
                        break;
                    case "decimal":
                    case "money":
                    case "smallmoney":
                    case "numeric":
                        R = "BigDecimal";
                        break;
                    case "smalldatetime":
                    case "datetime":
                        R = "Timestamp";
                        break;
                    case "timestamp":
                    case "binary":
                    case "varbinary":
                    case "image":
                        R = "byte[]";
                        break;
                    default:
                        R = " String";
                        break;
                }
            }
            else if (DataBaseHelper.DbType == 2)
            {
                switch (col.DATA_TYPE.ToUpper())
                {
                    case "VARCHAR":
                    case "CHAR":
                    case "TEXT":
                        R = "String";
                        break;
                    case "BLOB":
                        R = "byte[]";
                        break;
                    case "INTEGER":
                    case "ID":
                        R = "Long";
                        break;
                    case "TINYINT":
                    case "SMALLINT":
                    case "MEDIUMINT":
                    case "BOOLEAN":
                        R = "Integer";
                        break;
                    case "BIT":
                        R = "Boolean";
                        break;
                    case "BIGINT":
                        R = "BigInteger";
                        break;
                    case "FLOAT":
                        R = "Float";
                        break;
                    case "DOUBLE":
                        R = "Double";
                        break;
                    case "DECIMAL":
                        R = "BigDecimal";
                        break;
                    case "DATE":
                    case "YEAR":
                        R = "Date";
                        break;
                    case "TIME":
                        R = "Time";
                        break;
                    case "DATETIME":
                    case "TIMESTAMP":
                        R = "Timestamp";
                        break;
                    default:
                        R = "String";
                        break;
                }
            }

            return R;
        }

        public static string GetJDBCType(ColModel col)
        {
            string R = col.DATA_TYPE;

            if (DataBaseHelper.DbType == 0)
            {
                if (col.DATA_TYPE == "VARCHAR2")
                {
                    R = "VARCHAR";
                }
                else if (col.DATA_TYPE == "NVARCHAR2")
                {
                    R = "NVARCHAR";
                }
                else if (col.DATA_TYPE == "NUMBER")
                {
                    R = "DECIMAL";
                }
                else if (col.DATA_TYPE == "DATE")
                {
                    R = "TIMESTAMP";
                }
            }
            else if (DataBaseHelper.DbType == 1)
            {
                switch (col.DATA_TYPE.ToLower())
                {
                    case "timestamp":
                        R = "BINARY";
                        break;
                    case "money":
                    case "smallmoney":
                        R = "DECIMAL";
                        break;
                    case "float":
                        R = "DOUBLE";
                        break;
                    case "int":
                        R = "INTEGER";
                        break;
                    case "image":
                    case "varbinary(max)":
                        R = "LONGVARBINARY";
                        break;
                    case "text":
                    case "varchar(max)":
                        R = "LONGVARCHAR";
                        break;
                    case "nchar":
                        R = "CHAR";
                        break;
                    case "nvarchar":
                        R = "VARCHAR";
                        break;
                    case "nvarchar(max)":
                    case "ntext":
                        R = "LONGVARCHAR";
                        break;
                    case "datetime":
                    case "smalldatetime":
                        R = "TIMESTAMP";
                        break;
                    case "udt":
                        R = "VARBINARY";
                        break;
                    case "uniqueidentifier":
                        R = "CHAR";
                        break;
                    case "xml":
                        R = "LONGVARCHAR";
                        break;
                    default:
                        R = col.DATA_TYPE.ToUpper();
                        break;
                }
            }
            else if (DataBaseHelper.DbType == 2)
            {
                switch (col.DATA_TYPE.ToUpper())
                {
                    case "VARCHAR":
                        R = "VARCHAR";
                        break;
                    case "LONGTEXT":
                        R = "LONGVARCHAR";
                        break;
                    case "INT":
                        R = "INTEGER";
                        break;
                    case "DATETIME":
                        R = "TIMESTAMP";
                        break;
                    case "BIT":
                        R = "BOOLEAN";
                        break;
                    case "TEXT":
                        R = "CLOB";
                        break;
                    case "BIGINT":
                        R = "BIGINT";
                        break;
                    case "TINYINT":
                        R = "TINYINT";
                        break;
                    case "SMALLINT":
                        R = "SMALLINT";
                        break;
                    case "DOUBLE":
                        R = "DOUBLE";
                        break;
                    case "DECIMAL":
                        R = "DECIMAL";
                        break;
                    default:
                        R = col.DATA_TYPE.ToUpper();
                        break;
                }
            }
            return R;
        }

        #endregion
    }
}