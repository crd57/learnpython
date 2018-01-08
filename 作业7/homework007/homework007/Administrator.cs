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
    public partial class Administrator : Form
    {
        public ArrayList operateResult;
        public DataSet daSe;
        public OleDbDataAdapter ada;

        public Administrator()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 方法：对数据库操作
        /// </summary>
        /// <param name="mySQL">SQL语句</param>
        /// <returns></returns>
        public static ArrayList OperateDB(string mySQL)
        {
            ArrayList al = new ArrayList();
            //数据库名称
            string dbName = "Student.mdb";
            //定义连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\" + dbName;
            //连接数据库
            OleDbConnection myconn = new OleDbConnection(connString);
            myconn.Open();
            //新建DataAdapter对象
            OleDbDataAdapter myAdapter = new OleDbDataAdapter(mySQL, myconn);
            //新建dataset对象
            DataSet ds = new DataSet();
            myAdapter.Fill(ds, "information");
            //关闭数据库连接
            myconn.Close();

            al.Add(myAdapter);
            al.Add(ds);
            //返回DataAdapter对象和dataset对象
            return al;
        }
        /// <summary>
        /// 方法，对数据库操作
        /// </summary>
        /// <param name="mySQL">SQL语句</param>
        public static void OperateDBbyCom(string mySQL)
        {
            //数据库名称
            string dbName = "Student.mdb";
            //定义连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\" + dbName;
            //连接数据库
            OleDbConnection myconn = new OleDbConnection(connString);
            myconn.Open();
            //新建command对象
            OleDbCommand mycmd = new OleDbCommand(mySQL, myconn);
            //实现数据的修改、插入、删除等
            mycmd.ExecuteNonQuery();
            //关闭数据库连接
            myconn.Close();
        }
        /// <summary>
        /// 窗体导入
        /// </summary>
        private void Administrator_Load(object sender, EventArgs e)
        {
            //初始化窗体
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 点击确定按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //获取用户名和密码
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            if (userName != "" && passWord != "")
            {
                //新建查询语句
                string mySQL1 = "select UserId from UserInfo where UserId = '" + userName + "'";
                string mySQL2 = "select UserPwd from UserInfo where UserId = '" + userName + "'";
                //获取查询后的数据
                operateResult = OperateDB(mySQL1);
                daSe = (DataSet)operateResult[1];
                if (daSe.Tables["information"].Rows.Count == 0)
                {
                    MessageBox.Show("用户不存在！", "错误提示", MessageBoxButtons.OK);
                    //初始化窗体
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    operateResult = OperateDB(mySQL2);
                    daSe = (DataSet)operateResult[1];
                    if (daSe.Tables["information"].Rows[0][0].ToString() != passWord)
                    {
                        MessageBox.Show("密码错误，请重新输入！", "错误提示", MessageBoxButtons.OK);
                        //初始化窗体
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                    else
                    {
                        MessageBox.Show("登录成功！", "提示", MessageBoxButtons.OK);
                        StudentManagement stm = new StudentManagement();
                        stm.Show();
                        this.Visible = false;
                    }
                }
            }
            else
            {
                if (userName == "")
                {
                    MessageBox.Show("用户名为空，请重新输入！", "错误提示", MessageBoxButtons.OK);
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("密码为空，请重新输入！", "错误提示", MessageBoxButtons.OK);
                    textBox2.Focus();
                }
            }
        }
        /// <summary>
        /// 点击重试按钮
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            //初始化窗体
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 点击退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        /// <summary>
        /// 点击用户注册按钮
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            //reg.ShowDialog();
            reg.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 点击修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            ResetPassword rs = new ResetPassword();
            rs.Show();
            this.Visible = false;
        }
    }
}
