using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 作业
{
    public partial class Form3 : Form
    {
        String expression = "";     // 记录 box_show.Text
        Boolean isIs = false;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 使得 box_show 在右侧显示
            box_show.SelectionAlignment = HorizontalAlignment.Right;
            box_result.SelectionAlignment = HorizontalAlignment.Right;
        }

        #region 菜单栏 状态栏
        private void 主页面ToolStripMenuItem_Click(object sender, EventArgs e) // 打开主页面
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();    // 隐藏这个界面
        }
        private void 进制转换ToolStripMenuItem_Click(object sender, EventArgs e) // 打开进制转换
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();    // 隐藏这个界面
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) // 退出程序
        {
            Application.Exit();
        }
        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e) // 打开说明
        {
            Form5 fm = new Form5();
            fm.Show();
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e) // 打开关于
        {
            Form6 fm = new Form6();
            fm.Show();
        }
        
        private void timer1_Tick(object sender, EventArgs e) // 状态栏
        {
            toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString() + DateTime.Now.ToLongTimeString();
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e) // 窗口关闭时退出程序
        {
            Application.Exit();
        }
        #endregion

        #region 单选框
        private void l_2_CheckedChanged(object sender, EventArgs e) // 左二进制
        {
            box_result.Text = string.Empty;
            if (l_2.Checked == true)
            {
                // 二进制只有0 1可用,其余不可用
                button_0.Enabled = true;
                button_1.Enabled = true;
                button_2.Enabled = false;
                button_3.Enabled = false;
                button_4.Enabled = false;
                button_5.Enabled = false;
                button_6.Enabled = false;
                button_7.Enabled = false;
                button_8.Enabled = false;
                button_9.Enabled = false;
                //button_dot.Enabled = true;
                button_A.Enabled = false;
                button_B.Enabled = false;
                button_C.Enabled = false;
                button_D.Enabled = false;
                button_E.Enabled = false;
                button_F.Enabled = false;

            }
        }
        private void l_8_CheckedChanged(object sender, EventArgs e) // 左八进制
        {
            box_result.Text = string.Empty;
            if (l_8.Checked == true)
            {
                // 二进制中只有0 1 2 3 4 5 6 7可用, 其余不可用
                button_0.Enabled = true;
                button_1.Enabled = true;
                button_2.Enabled = true;
                button_3.Enabled = true;
                button_4.Enabled = true;
                button_5.Enabled = true;
                button_6.Enabled = true;
                button_7.Enabled = true;
                button_8.Enabled = false;
                button_9.Enabled = false;
                //button_dot.Enabled = true;
                button_A.Enabled = false;
                button_B.Enabled = false;
                button_C.Enabled = false;
                button_D.Enabled = false;
                button_E.Enabled = false;
                button_F.Enabled = false;
            }
        }
        private void l_10_CheckedChanged(object sender, EventArgs e) // 左十进制
        {
            box_result.Text = string.Empty;
            if (l_10.Checked == true)
            {
                // 十进制中只有0 1 2 3 4 5 6 7 8 9可用, 其余不可用
                button_0.Enabled = true;
                button_1.Enabled = true;
                button_2.Enabled = true;
                button_3.Enabled = true;
                button_4.Enabled = true;
                button_5.Enabled = true;
                button_6.Enabled = true;
                button_7.Enabled = true;
                button_8.Enabled = true;
                button_9.Enabled = true;
                //button_dot.Enabled = true;
                button_A.Enabled = false;
                button_B.Enabled = false;
                button_C.Enabled = false;
                button_D.Enabled = false;
                button_E.Enabled = false;
                button_F.Enabled = false;
            }
        }
        private void l_16_CheckedChanged(object sender, EventArgs e) // 左十六进制
        {
            box_result.Text = string.Empty;
            if (l_16.Checked == true)
            {
                // 十六进制全部可用
                button_0.Enabled = true;
                button_1.Enabled = true;
                button_2.Enabled = true;
                button_3.Enabled = true;
                button_4.Enabled = true;
                button_5.Enabled = true;
                button_6.Enabled = true;
                button_7.Enabled = true;
                button_8.Enabled = true;
                button_9.Enabled = true;
                //button_dot.Enabled = true;
                button_A.Enabled = true;
                button_B.Enabled = true;
                button_C.Enabled = true;
                button_D.Enabled = true;
                button_E.Enabled = true;
                button_F.Enabled = true;
            }
        }
        #endregion

        #region 0123456789.ABCDEF

        /// <summary>
        /// 向显示框添加数字
        /// </summary>
        /// <param name="numStr">需要添加进显示框的数字</param>
        private void AddNumber(string numStr) // 0123456789ABCDEF
        {
            box_result.Text = string.Empty;

            // 当box_show为"输入错误","0"(数的第一位不能为0),或者已经被计算过(此时box_show内为计算结果,此时是下次输入,即为第二次输入计算)
            if (box_show.Text == "输入有误" || box_show.Text == "0" || isIs == true)
            {
                // 清空
                box_show.Text = string.Empty;
                expression = string.Empty;
                // 设置为未计算.
                isIs = false;
            }

            box_show.Text += numStr;
            expression += numStr;
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            AddNumber("0");
        }
        private void button_1_Click(object sender, EventArgs e)
        {
            AddNumber("1");
        }
        private void button_2_Click(object sender, EventArgs e)
        {
            AddNumber("2");
        }
        private void button_3_Click(object sender, EventArgs e)
        {
            AddNumber("3");
        }
        private void button_4_Click(object sender, EventArgs e)
        {
            AddNumber("4");
        }
        private void button_5_Click(object sender, EventArgs e)
        {
            AddNumber("5");
        }
        private void button_6_Click(object sender, EventArgs e)
        {
            AddNumber("6");
        }
        private void button_7_Click(object sender, EventArgs e)
        {
            AddNumber("7");
        }
        private void button_8_Click(object sender, EventArgs e)
        {
            AddNumber("8");
        }
        private void button_9_Click(object sender, EventArgs e)
        {
            AddNumber("9");
        }
        private void button_A_Click(object sender, EventArgs e)
        {
            AddNumber("A");
        }
        private void button_B_Click(object sender, EventArgs e)
        {
            AddNumber("B");
        }
        private void button_C_Click(object sender, EventArgs e)
        {
            AddNumber("C");
        }
        private void button_D_Click(object sender, EventArgs e)
        {
            AddNumber("D");
        }
        private void button_E_Click(object sender, EventArgs e)
        {
            AddNumber("E");
        }
        private void button_F_Click(object sender, EventArgs e)
        {
            AddNumber("F");
        }

        private void button_dot_Click(object sender, EventArgs e)
        {
            box_result.Text = string.Empty;

            #region "0"的问题
            if (box_show.Text == "输入有误" || box_show.Text == string.Empty || isIs == true)
            {
                box_show.Text = "0";
                isIs = false;
            }
            #endregion

            #region "."的问题
            Boolean isDot = false; // 标记这个数此时没有"."
            for (int i = 1; i <= expression.Length; i++)
            {
                // 如果在这个数中遍历到了".", 标记这个数已经有"."
                if (expression[expression.Length - i] == '.')
                {
                    isDot = true;
                    break;
                }
            }
            #endregion

            // 遍历结束,这个数中没有".",添加"."
            if (isDot == false)
            {
                box_show.Text += ".";
                expression += ".";
            }
        }
        #endregion

        #region C←
        private void button_clean_Click(object sender, EventArgs e)
        {
            box_show.Text = string.Empty;
            expression = box_show.Text;
            box_result.Text = string.Empty;
            isIs = false;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            if (box_show.Text.Length > 0)
            {
                box_show.Text = box_show.Text.Remove(box_show.Text.Length - 1);
                expression = box_show.Text;
                box_result.Text = string.Empty;
                isIs = false;
            }
        }
        #endregion

        #region Function

        /// <summary>
        /// 检测需要转换的进制是什么位
        /// </summary>
        /// <param name="lift">转换前的进制</param>
        /// <param name="right">转换后的进制</param>
        private void Checked(out int left)
        {
            int l = 0;  // 数据转换前的进制
            // 判断转换前的数据的进制
            // 遍历 panel 找出被选中的 radioButton
            foreach (Control c in panel1.Controls)
            {
                if (c is RadioButton)
                {
                    if ((c as RadioButton).Checked)
                    {
                        l = Convert.ToInt32(c.Text);
                        break;
                    }
                }
            }
            left = l;
        }
        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="left">转换前的进制</param>
        /// <param name="expression">转换前的表达式</param>
        /// <param name="result">转换后的表达式</param>
        private void Change(int left, int right, string expression, out string result)
        {
            // 如果有负号,去掉计算的时候负号
            if (expression[0] == '-')
            {
                expression = expression.Remove(0, 1);
            }

            if (expression.Contains(".")) // 小数
            {
                string[] array = expression.Split('.');
                // 整数部分
                string _integer = array[0];
                int temp = Convert.ToInt32(_integer, left);
                result = Convert.ToString(temp, right);

                // 小数部分
                string _float = array[1];
                temp = Convert.ToInt32(_float, left);

                char[] c_str = Convert.ToString(temp, right).ToCharArray();
                Array.Reverse(c_str);
                string str = new string(c_str);

                result += ".";
                result += str;
            }
            else // 整数
            {
                int temp = Convert.ToInt32(expression, left);
                result = Convert.ToString(temp, right);
            }
            // 如果计算的时候去掉了负号,最后要加上负号
            if (expression.Length != box_show.Text.Length)
            {
                result = result.Insert(0, "-");
            }
        }
        
        private void ShowError() // 显示错误
        {
            box_show.Text = "输入有误";
        }
        private void ShowResult(string result) // 显示结果
        {
            box_result.Text = result;
        }

        private void button_is_Click(object sender, EventArgs e) // =
        {
            try
            {
                int l;  // 数据转换前的进制

                Checked(out l);
                //Change(l, box_show.Text, out expression);

                ShowResult(expression);
                isIs = true;
            }
            catch
            {
                ShowError();
            }
        }
        #endregion

        private void button_and_Click(object sender, EventArgs e)
        {
            AddNumber("&");
        }
        private void button_or_Click(object sender, EventArgs e)
        {
            AddNumber("|");
        }
        private void button_xor_Click(object sender, EventArgs e)
        {
            AddNumber("^");
        }
        private void button_left_Click(object sender, EventArgs e)
        {
            AddNumber("<<");
        }
        private void button_right_Click(object sender, EventArgs e)
        {
            AddNumber(">>");
        }
        private void button_invert_Click(object sender, EventArgs e) // ~
        {
            try
            {
                box_show.Text = box_show.Text.Insert(0, "~");
                //expression = (~Convert.ToInt32(expression)).ToString();
                ShowResult(expression);
            }
            catch
            {
                ShowError();
            }
        }
        private void button_rightZore_Click(object sender, EventArgs e)
        {
            AddNumber(">>>");
        }
    }
}
