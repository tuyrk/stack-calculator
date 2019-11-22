using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Numerics;

namespace 作业
{
    public partial class Form1 : Form
    {
        String expression = string.Empty;     // 记录 box_show.Text
        Boolean isIs = false;       // 记录是否被计算.是则为true,否则为false

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            box_show.SelectionAlignment = HorizontalAlignment.Right; // 靠右显示
            box_result.SelectionAlignment = HorizontalAlignment.Right; // 靠右显示
        }

        #region 菜单栏 状态栏
        private void 进制转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();
        }
        private void 位运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.Show();
            this.Hide();
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 fm = new Form5();
            fm.Show();
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 fm = new Form6();
            fm.Show();
        }
        private void 进制转换ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            进制转换ToolStripMenuItem_Click(sender, e);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString() + DateTime.Now.ToLongTimeString();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 0123456789+-*/%^()
        private void AddNumber(string numStr) // 0123456789
        {
            box_result.Text = string.Empty;

            // 当box_show为"输入错误","0"(数的第一位不能为0),或者已经被计算过(此时box_show内为计算结果,此时是下次输入,即为第二次输入计算)
            if (box_show.Text == "输入有误" || box_show.Text == "0" || isIs == true)
            {
                box_show.Text = string.Empty;
                expression = string.Empty;
                //ClearBox("box_show.Text", "expression");
            }

            // 括号后直接跟数字,在数字前添加一个"*"
            if (expression.Length > 0 && expression[expression.Length - 1] == ')')
            {
                box_show.Text += "*";
                expression += "*";
            }

            box_show.Text += numStr;
            expression += numStr;
        }

        private void AddSign(string signStr) // +-*/%^
        {
            isIs = false;// 设置为未计算.
            box_result.Text = string.Empty;

            if (box_show.Text == "输入有误")
            {
                box_show.Text = string.Empty;
                expression = string.Empty;
            }

            #region if (signStr.Equals("-"))
            bool condition = false;

            if (signStr.Equals("-")) // -
            {
                if ((expression.Length == 0) // 如果 box_show 为空,直接添加"-"
                    || (expression[expression.Length - 1] != '-'
                    || (expression.Length >= 2 && expression[expression.Length - 2] != '-')) // --
                    && expression[expression.Length - 1] != '.'// box_show 最后一个符号不为"."
               )
                {
                    condition = true;
                }
            }
            #endregion
            #region else
            else // +*/%^
            {
                if (expression.Length > 0 // "+"不能在第一个位置.
                && expression[expression.Length - 1] != '/' // 前一个不能为"/"
                && expression[expression.Length - 1] != '*'
                && expression[expression.Length - 1] != '-'
                && expression[expression.Length - 1] != '+'
                && expression[expression.Length - 1] != '%'
                && expression[expression.Length - 1] != '^'
                && expression[expression.Length - 1] != '.')
                {
                    condition = true;
                }
            }
            #endregion

            if (condition == true)
            {
                box_show.Text += signStr;
                expression += signStr;
            }
        }

        private void AddDot(string dotStr) // .
        {
            box_result.Text = string.Empty;

            #region "0"的问题
            // box_show 中为"输入错误" || 已经被计算过(此时box_show内为计算结果,此时是下次输入,即为第二次输入计算)
            if (isIs == true || box_show.Text == "输入有误")
            {
                box_show.Text = "0";
                expression = "0";
            }

            if (box_show.Text == string.Empty // box_show中为空.直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '+' // box_show中最后一个符号为"+".直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '-' // box_show中最后一个符号为"-".直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '*' // box_show中最后一个符号为"*".直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '/' // box_show中最后一个符号为"/".直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '%' // box_show中最后一个符号为"%".直接点击"."时在前面加一个"0"
                || expression[expression.Length - 1] == '^') // box_show中最后一个符号为"^".直接点击"."时在前面加一个"0"
            {
                box_show.Text += "0";
                expression += "0";
            }
            #endregion

            #region "."的问题
            Boolean isDot = false; // 标记这个数此时没有"."
            // 从末尾循环遍历express,直到遇到"+" "-" "*" "/" "%" "^"退出循环(只求最近的一个数中有没有".")
            // 防止数据出现 0.0.0 的情况
            for (int i = 1; i <= expression.Length; i++)
            {
                // 如果在这个数中遍历到了".", 标记这个数已经有"."
                if (expression[expression.Length - i] == '.')
                {
                    isDot = true;
                    break;
                }
                // 遇到"+" "-" "*" "/" "%" "^"退出循环,这个是与前一个数的分隔点
                if (expression[expression.Length - i] == '+'
                    || expression[expression.Length - i] == '-'
                    || expression[expression.Length - i] == '*'
                    || expression[expression.Length - i] == '/'
                    || expression[expression.Length - i] == '%'
                    || expression[expression.Length - i] == '^')
                {
                    break;
                }
            }
            #endregion

            // 括号后边直接加".",在之前添加"*",然后再添加"0"
            if (expression.Length > 0 && expression[expression.Length - 1] == ')')
            {
                box_show.Text += "*0";
                expression += "*0";
            }

            // 遍历结束,这个数中没有".",添加"."
            if (isDot == false)
            {
                box_show.Text += ".";
                expression += ".";
            }
        }

        private void AddBrackts(string brackts) // ()
        {
            box_result.Text = string.Empty;

            #region if (brackts.Equals("("))
            if (brackts.Equals("("))
            {
                if (box_show.Text == "输入有误")
                {
                    box_show.Text = string.Empty;
                    expression = string.Empty;
                }

                // "."后面直接添加左括号,在"."后面添加一个"0",然后下一个if判断会自动添加一个"*"
                if (expression.Length > 0 && expression[expression.Length - 1] == '.')
                {
                    box_show.Text += "0";
                    expression += "0";
                }

                // 数字后直接加左括号,在数字后自动添加一个"*"
                if (expression.Length > 0 && Char.IsNumber(expression[expression.Length - 1]))
                {
                    box_show.Text += "*";
                    expression += "*";
                }
                box_show.Text += "(";
                expression += "(";
                isIs = false;
            }
            #endregion

            #region else
            else
            {
                if (expression.Length > 0)
                {
                    box_show.Text += ")";
                    expression += ")";
                }
            }
            #endregion
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
        private void button_dot_Click(object sender, EventArgs e)
        {
            AddDot(".");
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            AddSign("+");
        }
        private void button_sub_Click(object sender, EventArgs e)
        {
            AddSign("-");
        }
        private void button_mul_Click(object sender, EventArgs e)
        {
            AddSign("*");
        }
        private void button_div_Click(object sender, EventArgs e)
        {
            AddSign("/");
        }
        private void button_remainder_Click(object sender, EventArgs e)
        {
            AddSign("%");
        }
        private void button_pow_Click(object sender, EventArgs e)
        {
            AddSign("^");
        }
        private void button_left_Click(object sender, EventArgs e)
        {
            AddBrackts("(");
        }
        private void button_right_Click(object sender, EventArgs e)
        {
            AddBrackts(")");
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

        // 核心代码
        #region 求解表达式
        private String evaluateExpression(String expression)
        {
            Stack<double> operandStack = new Stack<double>();
            Stack<char> operatorStack = new Stack<char>();
            List<String> tokens = split(expression);

            for (int i = 0; i < tokens.Count; i++)
            {
                #region if("-"的问题)
                if (tokens[i][0] == '-' && tokens[i].Length > 1)    // tokens[i]为'-'&&tokens[i].Length>1 如果这是一个负数
                {
                    if (operandStack.Count != 0)    // 如果数字栈中已经有了元素
                    {
                        if (operatorStack.Peek() == '-') //如果符号栈最后一个元素为'-',例如 "3 - -6" 的情况
                        {
                            // 将 3 - -6 改写为 3 + 6
                            operatorStack.Pop();                                    // 弹出符号栈中的最后一个元素'-'
                            operatorStack.Push('+');                                // 向符号栈压入'+'
                            operandStack.Push(-1 * Convert.ToDouble(tokens[i]));    // 将数字 -6 作 6 压入数字栈
                        }
                        else if (operatorStack.Peek() == '(' || operatorStack.Peek() == '+'     // 如果符号栈中的最后一个元素是'(',例如 3 - (-6),直接将 -6 压入数字栈
                            || operatorStack.Peek() == '*' || operatorStack.Peek() == '/')  // 例如 3 * -6 、 3 / -6 、 3 + -6
                        {
                            // 将tokens[i]压入数字栈
                            operandStack.Push(Convert.ToDouble(tokens[i]));
                        }
                    }
                    else //数字栈中没有元素
                    {
                        // 将tokens[i]压入数字栈
                        operandStack.Push(Convert.ToDouble(tokens[i]));
                    }
                }
                #endregion
                #region else
                // 核心
                else
                {
                    if (tokens[i][0] == '+' || tokens[i][0] == '-')
                    {
                        while (operatorStack.Count != 0
                            && ('+' == Convert.ToChar(operatorStack.Peek())
                            || '-' == Convert.ToChar(operatorStack.Peek())
                            || '*' == Convert.ToChar(operatorStack.Peek())
                            || '/' == Convert.ToChar(operatorStack.Peek())
                            || '%' == Convert.ToChar(operatorStack.Peek())
                            || '^' == Convert.ToChar(operatorStack.Peek())))
                        {
                            processAnOperator(operandStack, operatorStack);
                        }

                        operatorStack.Push(tokens[i][0]);
                    }
                    else if (tokens[i][0] == '*' || tokens[i][0] == '/' || tokens[i][0] == '%' || tokens[i][0] == '^')
                    {
                        while (operatorStack.Count != 0
                            && ('*' == Convert.ToChar(operatorStack.Peek())
                            || '/' == Convert.ToChar(operatorStack.Peek())
                            || '%' == Convert.ToChar(operatorStack.Peek())
                            || '^' == Convert.ToChar(operatorStack.Peek())))
                        {
                            processAnOperator(operandStack, operatorStack);
                        }

                        operatorStack.Push(tokens[i][0]);
                    }
                    else if (tokens[i][0] == '(')
                    {
                        operatorStack.Push('(');
                    }
                    else if (tokens[i][0] == ')')
                    {
                        while (Convert.ToChar(operatorStack.Peek()) != '(')
                        {
                            processAnOperator(operandStack, operatorStack);
                        }

                        operatorStack.Pop();
                    }
                    else
                    {
                        operandStack.Push(Convert.ToDouble(tokens[i]));
                    }
                }
                #endregion
            }

            while (operatorStack.Count != 0)
            {
                processAnOperator(operandStack, operatorStack);
            }

            return operandStack.Pop().ToString();
        }

        private List<String> split(String Text)
        {
            List<String> v = new List<string>();
            String numberString = "";

            for (int i = 0; i < Text.Length; i++)
            {
                if (Char.IsNumber(Text[i])
                    || Text[i] == '.'
                    || (Text[i] == '-'
                    && Char.IsNumber(Text[i + 1])
                    && numberString.Length == 0))
                {
                    numberString += Text[i];
                }
                else
                {
                    if (numberString.Length > 0)
                    {
                        v.Add(numberString);
                        numberString = "";
                    }

                    if (Text[i] != ' ')
                    {
                        String s = "";
                        s += Text[i];
                        v.Add(s);
                    }
                }
            }

            if (numberString.Length > 0)
            {
                v.Add(numberString);
            }
            /*
            string s0 = "";
            foreach (string i in v)
            {
                s0 += i + "&";
            }
            MessageBox.Show(s0.ToString());
            */
            return v;
        }

        private void processAnOperator(Stack<double> operandStack, Stack<char> operatorStack)
        {
            char op = operatorStack.Pop();
            double op1 = operandStack.Pop();
            double op2 = operandStack.Pop();
            if (op == '+')
                operandStack.Push(op2 + op1);
            else if (op == '-')
                operandStack.Push(op2 - op1);
            else if (op == '*')
                operandStack.Push(op2 * op1);
            else if (op == '/')
                operandStack.Push(op2 / op1);
            else if (op == '%')
                operandStack.Push(op2 % op1);
            else if (op == '^')
                operandStack.Push(Math.Pow(op2, op1));

        }

        #endregion

        #region Function
        private void button_is_Click(object sender, EventArgs e) // =
        {
            try
            {
                if (expression.Length > 0)
                {
                    expression = evaluateExpression(box_show.Text);
                    ShowResult(expression);
                    isIs = true;
                }
            }
            catch
            {
                ShowError();
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

        private void ClearBox(params string[] boxName)
        {
            for (int i= 0;i < boxName.Length; i++)
            {
                this.GetType().GetField(boxName[i]).SetValue(this, "123123");
                boxName[i] = string.Empty;
            }
        }

        private void Function(string fun) // 功能接口
        {
            try
            {
                Boolean isNumber = true;
                foreach (char exp in expression)
                {
                    if (!(Char.IsDigit(exp) || exp == '.' || exp == '-'))
                    {
                        isNumber = false;
                    }
                }
                if (isNumber == true)
                {
                    #region Switch
                    switch (fun)
                    {
                        case "reciprocal": reciprocal(); break;
                        case "sum":sum();break;
                        case "sign":sign();break;
                        case "square":square();break;
                        case "PI":PI();break;
                        case "exp":exp();break;
                        case "_10_n":_10_n();break;
                        case "fac":fac();break;
                        case "pow_3": pow_3();break;
                        case "pwo_2": pwo_2();break;
                        case "sin": sin(); break;
                        case "cos": cos(); break;
                        case "tan": tan(); break;
                        case "lg": lg(); break;
                        case "ln": ln(); break;
                        default:
                            break;
                    }
                    #endregion
                    ShowResult(expression);
                }
            }
            catch
            {
                ShowError();
            }
        }

        #region Function
        private void reciprocal() // 1/X
        {
            expression = (1.0 / Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void sum() // ∑
        {
            UInt64 number = Convert.ToUInt64(box_show.Text);
            UInt64 index = 1;
            BigInteger result = 0;
            while (index <= number)
            {
                result += index++;
            }
            expression = result > Int64.MaxValue ? result.ToString("E8") : result.ToString();
        }
        private void sign() // ±
        {
            expression = (-1 * Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void square() // square
        {
            expression = Math.Sqrt(Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void PI() // ∏
        {
            if (box_show.Text[box_show.Text.Length - 1] != '*')
            {
                box_show.Text += "*";
                expression = box_show.Text;
            }
            box_show.Text += "∏";
            expression += Math.PI.ToString();
        }
        private void exp() // exp
        {
            expression = Math.Exp(Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void _10_n() // 10_n
        {
            expression = Math.Pow(10, Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void fac() // fac
        {
            UInt64 number = Convert.ToUInt64(box_show.Text);
            UInt64 index = 1;
            BigInteger result = 1;
            while (index <= number)
            {
                result *= index++;
            }
            expression = result > Int64.MaxValue ? result.ToString("E8") : result.ToString();
        }
        private void pow_3() // pow_3
        {
            expression = Math.Pow(Convert.ToDouble(evaluateExpression(box_show.Text)), 3).ToString();
        }
        private void pwo_2() // pow_2
        {
            expression = Math.Pow(Convert.ToDouble(evaluateExpression(box_show.Text)), 2).ToString();
        }
        private void sin() // sin
        {
            expression = Math.Sin(Math.PI * Convert.ToDouble(evaluateExpression(box_show.Text)) / 180).ToString();
        }
        private void cos() // cos
        {
            expression = Math.Cos(Math.PI * Convert.ToDouble(evaluateExpression(box_show.Text)) / 180).ToString();
        }
        private void tan() // tan
        {
            expression = Math.Tan(Math.PI * Convert.ToDouble(evaluateExpression(box_show.Text)) / 180).ToString();
        }
        private void lg() // lg
        {
            expression = Math.Log10(Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        private void ln() // ln
        {
            expression = Math.Log(Convert.ToDouble(evaluateExpression(box_show.Text))).ToString();
        }
        #endregion

        #region Button_fun_Click
        private void button_reciprocal_Click(object sender, EventArgs e) // 1/X
        {
            Function("reciprocal");
        }
        private void button_sum_Click(object sender, EventArgs e) // ∑
        {
            Function("sum");
        }
        private void button_sign_Click(object sender, EventArgs e) // ±
        {
            Function("sign");
        }
        private void button_square_Click(object sender, EventArgs e) // square
        {
            Function("square");
        }
        private void button_PI_Click(object sender, EventArgs e) // ∏
        {
            Function("PI");
        }
        private void button_exp_Click(object sender, EventArgs e) // exp
        {
            Function("exp");
        }
        private void button_10_n_Click(object sender, EventArgs e) // 10_n
        {
            Function("_10_n");
        }
        private void button_fac_Click(object sender, EventArgs e) // fac
        {
            Function("fac");
        }
        private void button_pow_3_Click(object sender, EventArgs e) // pow_3
        {
            Function("pow_3");
        }
        private void button_pow_2_Click(object sender, EventArgs e) // pow_2
        {
            Function("pwo_2");
        }
        private void button_sin_Click(object sender, EventArgs e) // sin
        {
            Function("sin");
        }
        private void button_cos_Click(object sender, EventArgs e) //cos
        {
            Function("cos");
        }
        private void button_tan_Click(object sender, EventArgs e) // tan
        {
            Function("tan");
        }
        private void button_log_Click(object sender, EventArgs e) // lg
        {
            Function("lg");
        }
        private void button_ln_Click(object sender, EventArgs e) // ln
        {
            Function("ln");
        }
        #endregion

        #endregion

        
    }
}
