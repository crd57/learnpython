using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace homework007
{
    public partial class ResetPassword : Form
    {
        public ArrayList operateResult;
        public DataSet daSe;
        public OleDbDataAdapter ada;
        public ResetPassword()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体导入
        /// </summary>
        private void ResetPassword_Load(object sender, EventArgs e)
        {
            //重置窗体
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 点击重置密码按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //获取更改的账户信息
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            string newPasWrd = textBox3.Text;
            string reNewPasWrd = textBox4.Text;
            //SQL语句，账户查询
            string mySQL1 = "select UserId from UserInfo where UserId = '" + userName + "'";
            //SQL语句，账户密码查询
            string mySQL2 = "select UserPwd from UserInfo where UserId = '" + userName + "'";

            if (userName != "" && passWord != "" && newPasWrd != "" && reNewPasWrd != "")
            {
                //获取查询后的数据
                operateResult = Administrator.OperateDB(mySQL1);
                daSe = (DataSet)operateResult[1];
                
                if (daSe.Tables["information"].Rows.Count == 0)
                {
                    MessageBox.Show("用户不存在！", "错误提示", MessageBoxButtons.OK);
                    //重置窗体
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox1.Focus();
                }
                else if (textBox3.Text.Length > 10)
                {
                    MessageBox.Show("密码长度超过10位，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox3.Focus();
                }
                else if (textBox3.Text.Length < 6)
                {
                    MessageBox.Show("密码长度不足6位，请重新输入！", "错误提示框", MessageBoxButtons.OK);
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox3.Focus();
                }
                else if (newPasWrd != reNewPasWrd)
                {
                    MessageBox.Show("两次输入的重设密码不一致！", "错误提示", MessageBoxButtons.OK);
                    //初始化窗体
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox3.Focus();
                }
                else
                {
                    operateResult = Administrator.OperateDB(mySQL2);
                    daSe = (DataSet)operateResult[1];
                    if (daSe.Tables["information"].Rows[0][0].ToString() != passWord)
                    {
                        MessageBox.Show("原始密码错误，请重新输入！", "错误提示", MessageBoxButtons.OK);
                        //初始化窗体
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                    else
                    {
                        //SQL语句，更新密码
                        string mySQL3 = "update UserInfo set UserPwd = '" + textBox3.Text + "' where UserId = '" + userName + "'";
                        Administrator.OperateDBbyCom(mySQL3);
                        MessageBox.Show("密码更改成功！", "提示", MessageBoxButtons.OK);
                        //重置窗体
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox1.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("请填写完整的信息！", "错误提示", MessageBoxButtons.OK);
            }

        }
        /// <summary>
        /// 点击重新输入按钮
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            //重置窗体
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 点击立即登录按钮
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            Administrator adm = new Administrator();
            this.Close();
            adm.Show();
        }
        /// <summary>
        /// 点击退出应用按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
