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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (username == "")
            {
                MessageBox.Show("Please fill in user account!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password == "")
            {
                MessageBox.Show("Please fill in password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Login(username, password))
                {
                    this.Hide();
                    FrmMain frm = new FrmMain();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Wrong user or password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        bool Login(string username, string password)
        {
            string query = $"select * from UserMaster where UserID = '{username}' and [Password] = '{password}'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmConfig frm = new FrmConfig();
            frm.Show();
        }
    }
}
