using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace lxw_Helper
{
   public class DbHelper
    {
        public DbHelper()
        {
            Database db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 读取数据库默认连接字符串
        /// </summary>
        /// <returns></returns>
        private string ReadDefaultConnectString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection dataConfigurationSection = config.Sections["dataConfiguration"];
            string defaultDatabase = (dataConfigurationSection as Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings).DefaultDatabase;

            ConfigurationSection connectionConfigurationSection = config.Sections["connectionStrings"];
            ConnectionStringsSection connectionStringsSection = connectionConfigurationSection as ConnectionStringsSection;
            return connectionStringsSection.ConnectionStrings[defaultDatabase].ConnectionString;
        }

        /// <summary>
        /// 保存数据库默认连接字符串
        /// </summary>
        /// <param name="connectString"></param>
        private void SaveDefaultConnectString(string connectString)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection dataConfigurationSection = config.Sections["dataConfiguration"];
            string defaultDatabase = (dataConfigurationSection as Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings).DefaultDatabase;

            ConfigurationSection connectionConfigurationSection = config.Sections["connectionStrings"];
            ConnectionStringsSection connectionStringsSection = connectionConfigurationSection as ConnectionStringsSection;
            connectionStringsSection.ConnectionStrings[defaultDatabase].ConnectionString = connectString;
            config.Save();
        }

        /// <summary>
        /// 刷新数据库默认连接字符串
        /// </summary>
        private void RefreshConfig()
        {
            ConfigurationManager.RefreshSection("dataConfiguration");
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
