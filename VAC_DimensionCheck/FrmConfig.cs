using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAC_DimensionCheck.Helpers;

namespace VAC_DimensionCheck
{
    public partial class FrmConfig : Form
    {
        Config config = new Config();
        public FrmConfig()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            string sServer = txtServer.Text.Trim();
            string sDatabase = txtDatabase.Text.Trim();
            string sUsername = txtUsername.Text.Trim();
            string sPassword = txtPassword.Text.Trim();

            config.SaveConfig(sServer, sDatabase, sUsername, sPassword);
        }

        private void txtExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void LoadConfig()
        {
            string[] sConfig = config.LoadConfig();
            txtServer.Text = sConfig[0];
            txtDatabase.Text = sConfig[1];
            txtUsername.Text = sConfig[2];
            txtPassword.Text = sConfig[3];
        }
    }
}
