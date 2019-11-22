using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 作业;

namespace 课堂
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            account.Text = "766564616";
            password.Text = "TUYUANKUN";
        }

        private void login_Click(object sender, EventArgs e)
        {
            string ID_1 = "766564616";
            string KEY_1 = "TUYUANKUN";

            double num;
            string ID = account.Text;
            string KEY = password.Text;
            if(!Double.TryParse(ID, out num))
            {
                MessageBox.Show("账号必须是数字串", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                account.Text = "";
                account.Focus();
            }
            else if(ID.Equals(ID_1) && KEY.Equals(KEY_1))
            {
                this.Hide();
                Form1 fm = new Form1();
                fm.Show();
                
            }
            else if (ID == "")
            {
                MessageBox.Show("请输入账号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                account.Text = "";
                password.Text = "";
                account.Focus();
            }
            else if(KEY == "")
            {
                MessageBox.Show("请输入密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                password.Focus();
            }
            else
            {
                MessageBox.Show("密码错误,请重试!", "登陆失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
                account.Text = "";
                password.Text = "";
                account.Focus();
            }
        }
    }
}
