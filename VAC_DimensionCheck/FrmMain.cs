using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAC_DimensionCheck.Helpers;
using VAC_DimensionCheck.Views;

namespace VAC_DimensionCheck
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            LoadProductGroup();
        }

        private void txtMachine_KeyDown(object sender, KeyEventArgs e)
        {
            string machine = txtMachine.Text.Trim();
            if (e.KeyCode == Keys.Enter)
            {
                string query = $"select * from Machine where Machine = '{machine}'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if (dt.Rows.Count > 0)
                {
                    txtMachineName.Text = dt.Rows[0]["MachineName"].ToString();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Invalid machine code!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void txtEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        void Login(string employee, string password)
        {
            string query = $"select * from Employee where Employee = '{employee}' and EmployeePWD = '{password}' and EmpGroup = '9Q01'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                txtEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
                grProductInfo.Enabled = true;
                LoadProductGroup();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string employee = txtEmployee.Text.Trim();
                string password = txtPassword.Text.Trim();
                Login(employee, password);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string employee = txtEmployee.Text.Trim();
            string password = txtPassword.Text.Trim();
            Login(employee, password);
        }

        private void cbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productGroup = (cbProductGroup.SelectedItem as DataRowView).Row["Code"] as string;
            string query = $"select Product from Product where ProductGroup = '{productGroup}'";
            cbProduct.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cbProduct.DisplayMember = "Product";
            cbProduct.ValueMember = "Product";
        }

        void LoadProductGroup()
        {
            string query = "select Code, [Name] from ProductGroup";
            cbProductGroup.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cbProductGroup.DisplayMember = "Name";
            cbProductGroup.ValueMember = "Code";
        }

        private void currentTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string productGroup = (cbProductGroup.SelectedItem as DataRowView).Row["Code"] as string;
            if (productGroup.Trim() == "01")
            {
                FrmAirblade frm = new FrmAirblade();
                frm.Show();
            }
            else
            {
                MessageBox.Show("This product is not supported yet", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
