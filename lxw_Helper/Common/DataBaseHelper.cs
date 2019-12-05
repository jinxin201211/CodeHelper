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

        public static string GetTableSql { get; set; }
        public DataBaseHelper()
        {
        }

        public static void SelectDataBase(string provider, string connstring)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConfigurationSection connectionConfigurationSection = config.Sections["connectionStrings"];
            ConnectionStringsSection connectionStringsSection = connectionConfigurationSection as ConnectionStringsSection;
            connectionStringsSection.ConnectionStrings["DataSource"].ConnectionString = connstring;
            connectionStringsSection.ConnectionStrings["DataSource"].ProviderName = provider;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            db = DatabaseFactory.CreateDatabase("DataSource");
        }

        public static DataTable GetTable()
        {
            DbCommand command = db.GetSqlStringCommand(GetTableSql);
            return db.ExecuteDataSet(command).Tables[0];
        }
    }
}
