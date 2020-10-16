using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAC_DimensionCheck.Helpers
{
    public class Config
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Encryption encryption = new Encryption();

        public void SaveConfig(string server, string database, string username, string password)
        {
            string connectionString = String.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3};", server, database, username, password);
            if (isConnected(connectionString))
            {
                config.AppSettings.Settings["server"].Value = server;
                config.AppSettings.Settings["database"].Value = database;
                config.AppSettings.Settings["username"].Value = username;
                config.AppSettings.Settings["password"].Value = encryption.Encrypt(password);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Config saved successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Can't connect database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string[] LoadConfig()
        {
            string[] res = new string[4];
            res[0] = config.AppSettings.Settings["server"].Value;
            res[1] = config.AppSettings.Settings["database"].Value;
            res[2] = config.AppSettings.Settings["username"].Value;
            res[3] = config.AppSettings.Settings["password"].Value;
            return res;
        }

        public string GetConnectionString()
        {
            string connectionString = "";
            string sServer = config.AppSettings.Settings["server"].Value;
            string sDatabase = config.AppSettings.Settings["database"].Value;
            string sUsername = config.AppSettings.Settings["username"].Value;
            string sPassword = config.AppSettings.Settings["password"].Value;
            if (sServer == "" || sDatabase == "" || sUsername == "" || sPassword == "")
            {
                connectionString = "";
            }
            else
            {
                connectionString = String.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3};", sServer, sDatabase, sUsername, encryption.Decrypt(sPassword));
            }
            return connectionString;
        }

        private static bool isConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
