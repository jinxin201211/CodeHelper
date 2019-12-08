using lxw_Helper.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace lxw_Helper.Common
{
    public class DataBaseHelper
    {
        private static Database db { get; set; }

        public static int DbType { get; set; } = 1;

        public static string IP { get; set; }
        public static string Port { get; set; }
        public static string Instance { get; set; }
        public static string User { get; set; }
        public static string Pwd { get; set; }

        public static string ConnectionString
        {
            get
            {
                if (DbType == 0)
                {
                    return $"Data Source={IP}:{Port}/{Instance}; user={User}; password={Pwd}; persist security info=false;";
                }
                else if (DbType == 1)
                {
                    return $"Data Source={IP};Initial Catalog={Instance};Persist Security Info=True;User ID={User};Password={Pwd};";
                }
                else
                {
                    return $"Data Source={IP}; Port={Port}; Database={Instance}; User ID={User}; Password={Pwd};";
                }
            }
        }

        public static string[] Provider
        {
            get
            {
                return new string[] { "System.Data.OracleClient", "System.Data.SqlClient", "Mysql.Data.MysqlClient" };
            }
        }
        public static string[] GetTableSql
        {
            get
            {
                return new string[] {
                    @"
SELECT A.TABLE_NAME, B.COMMENTS
  FROM USER_TABLES A
  LEFT JOIN USER_TAB_COMMENTS B
    ON A.TABLE_NAME = B.TABLE_NAME
 ORDER BY TABLE_NAME",
                    @"
SELECT A.NAME AS TABLE_NAME, ISNULL(G.[VALUE],'-') AS COMMENTS
  FROM SYS.TABLES A
  LEFT JOIN SYS.EXTENDED_PROPERTIES G
    ON (A.OBJECT_ID = G.MAJOR_ID AND G.MINOR_ID = 0 AND G.NAME='MS_Description')
 ORDER BY A.NAME",
                    $@"
SELECT TABLE_NAME,TABLE_COMMENT AS COMMENTS
  FROM INFORMATION_SCHEMA.`TABLES`
 WHERE TABLE_SCHEMA = '{Instance}' AND TABLE_TYPE='BASE TABLE'
 ORDER BY TABLE_NAME"
                };
            }
        }

        public static void ChangeDataBase()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection connectionConfigurationSection = config.Sections["connectionStrings"];
            ConnectionStringsSection connectionStringsSection = connectionConfigurationSection as ConnectionStringsSection;
            connectionStringsSection.ConnectionStrings["DataSource"].ConnectionString = ConnectionString;
            connectionStringsSection.ConnectionStrings["DataSource"].ProviderName = Provider[DbType];
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            db = DatabaseFactory.CreateDatabase("DataSource");
        }

        public static bool TestConnection(out string msg)
        {
            bool R = false;
            msg = string.Empty;
            DbConnection conn = db.CreateConnection();
            using (DbConnection connection = db.CreateConnection())
            {
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        R = true;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    msg = ex.Message;
                }
            }
            return R;
        }

        public static DataSet Query(string sql)
        {
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(command);
        }

        public static DataTable GetTable()
        {
            DbCommand command = db.GetSqlStringCommand(GetTableSql[DbType]);
            return db.ExecuteDataSet(command).Tables[0];
        }

        public static List<ColModel> GetColInfoByTable(string tableName, bool sort)
        {
            string strSQL = string.Empty;
            if (DbType == 0)
            {
                strSQL = $@" SELECT A.COLUMN_NAME,
                                   A.DATA_TYPE,
                                   B.COMMENTS,
                                   A.NULLABLE,
                                   A.DATA_LENGTH,
                                   CASE
                                     WHEN C.CONSTRAINT_NAME IS NULL THEN
                                      '0'
                                     ELSE
                                      '1'
                                   END AS ISPK,
                                   C.CONSTRAINT_NAME,
                                   A.DATA_DEFAULT,
                                   A.CHAR_LENGTH, A.DATA_PRECISION,A.DATA_SCALE
                              FROM USER_TAB_COLUMNS A
                              LEFT JOIN USER_COL_COMMENTS B
                                ON A.TABLE_NAME = B.TABLE_NAME
                               AND A.COLUMN_NAME = B.COLUMN_NAME
                              LEFT JOIN (SELECT A.TABLE_NAME, A.COLUMN_NAME, A.CONSTRAINT_NAME
                                           FROM USER_CONS_COLUMNS A, USER_CONSTRAINTS B
                                          WHERE A.CONSTRAINT_NAME = B.CONSTRAINT_NAME
                                            AND B.CONSTRAINT_TYPE = 'P'
                                    AND B.TABLE_NAME = '{tableName}') C
                                     ON A.TABLE_NAME = C.TABLE_NAME
                                    AND A.COLUMN_NAME = C.COLUMN_NAME
                                    WHERE A.TABLE_NAME = '{tableName}'";

                if (sort)
                {
                    strSQL += " ORDER BY A.COLUMN_NAME";
                }
            }
            else if (DbType == 1)
            {
                strSQL = $@"
SELECT 
--bm=  d.name ,
--表说明=case when a.colorder=1 then isnull(f.value,'') else '' end,
--字段序号=a.colorder,
COLUMN_NAME=a.name,
DATA_TYPE=b.name,
COMMENTS=isnull(g.[value],''),
NULLABLE=case when a.isnullable=1 then 'Y'else 'N' end,
DATA_LENGTH=cast(COLUMNPROPERTY(a.id,a.name,'PRECISION') as decimal),
ISPK=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (
  SELECT name FROM sysindexes WHERE indid in(
   SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid
   ))) then '1' else '0' end,
CONSTRAINT_NAME=h.CONSTRAINT_NAME,
DATA_DEFAULT=isnull(e.text,''),
--标识=case when COLUMNPROPERTY(a.id,a.name,'IsIdentity')=1 then '√'else '' end
CHAR_LENGTH=cast(a.length as decimal),
DATA_PRECISION=cast(isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as decimal),
DATA_SCALE=null
FROM syscolumns a
left join systypes b on a.xtype=b.xusertype
inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
left join syscomments e on a.cdefault=e.id
left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id
left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE h on h.TABLE_NAME=d.name and h.COLUMN_NAME=a.name
where d.name='{tableName}' --如果只查询指定表,加上此条件 
";

                if (sort)
                {
                    strSQL += " ORDER BY A.NAME";
                }
            }
            else
            {
                strSQL = $@"
select a.COLUMN_NAME,
a.DATA_TYPE,
a.COLUMN_COMMENT,
case when a.is_nullable='NO' then 'N' else 'Y' end as NULLABLE,
cast(a.character_maximum_length as decimal) as DATA_LENGTH,
case when a.column_key='PRI' then '1' else '0' end as ISPK,
'' as CONSTRAINT_NAME,
a.column_default as DATA_DEFAULT,
cast(a.character_octet_length as decimal) as CHAR_LENGTH, 
null as DATA_PRECISION, 
null as DATA_SCALE
 from information_schema.columns a
where table_schema = 'sys' and table_name = '{tableName}'
";

                if (sort)
                {
                    strSQL += " ORDER BY A.column_name";
                }
            }
            List<ColModel> ltCol = DataBaseHelper.Query(strSQL).Tables[0].ToList<ColModel>();

            foreach (var item in ltCol)
            {
                if (item.DATA_DEFAULT == null)
                {
                    item.DATA_DEFAULT = "";
                }
                else
                {
                    item.DATA_DEFAULT = item.DATA_DEFAULT.Replace("\n", "").Trim();
                    if (item.DATA_DEFAULT == "null")
                    {
                        item.DATA_DEFAULT = "";
                    }
                }
            }
            return ltCol;
        }
    }
}
