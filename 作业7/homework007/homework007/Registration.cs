using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace homework007
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体导入
        /// </summary>
        private void Registration_Load(object sender, EventArgs e)
        {
            //窗体重置
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Focus();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        /// <summary>
        /// 点击确定按钮
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;          //账号
            string name = textBox2.Text;        //姓名
            string password = textBox4.Text;    //用户密码
            string rePassword = textBox5.Text;  //确认密码
            string sex = "";                    //性别
            if(radioButton1.Checked)
                sex="男";
            else
                sex="女";
            if (id != "" && name != "" && password != "" && rePassword != "" && sex != "")
            {
                if (textBox1.Text.Length > 6)
                {
                    MessageBox.Show("账号名称过长，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else if (textBox1.Text.Length < 4)
                {
                    MessageBox.Show("账号名称过短，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else if (textBox4.Text.Length > 10)
                {
                    MessageBox.Show("密码过长，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox4.Focus();
                }
                else if (textBox4.Text.Length < 6)
                {
                    MessageBox.Show("密码过短，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox4.Focus();
                }
                else if (password != rePassword)
                {
                    MessageBox.Show("两次输入的密码不一致，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox5.Text = "";
                    textBox5.Focus();
                }
                else
                {
                    string date = DateTime.Now.ToShortDateString().ToString();
                    //新建SQL语句
                    string mySQL = "Insert into UserInfo values('" + id + "','" + password + "','" + name + "','" + sex + "','" + date + "')";
                    //操作数据库
                    try
                    {
                        Administrator.OperateDBbyCom(mySQL);
                        MessageBox.Show("恭喜，注册成功！", "注册提示框", MessageBoxButtons.OK);
                    }
                    catch
                    {
                        MessageBox.Show("账户已存在，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("请完整填写注册信息！", "错误提示框", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 点击重新输入按钮
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            //窗体重置
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Focus();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        /// <summary>
        /// 点击登录按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Administrator adm = new Administrator();
            this.Close();
            adm.Show();
        }
        /// <summary>
        /// 点击退出按钮
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
