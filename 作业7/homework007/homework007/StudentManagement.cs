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
using System.Data.OleDb;

namespace homework007
{
    public partial class StudentManagement : Form
    {
        public ArrayList operateResult;
        public DataSet daSe;
        public OleDbDataAdapter ada;
        public DataView dv;

        public StudentManagement()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 方法：获取列名
        /// </summary>
        public ArrayList getColumnName(string mySQL)
        {
            ArrayList al = new ArrayList();
            //数据库名称
            string dbName = "Student.mdb";
            //定义连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\" + dbName;
            //连接数据库
            OleDbConnection myconn = new OleDbConnection(connString);
            myconn.Open();
            //新建command对象
            OleDbCommand mycmd = new OleDbCommand(mySQL, myconn);
            //新建DataReader对象
            OleDbDataReader myDataReader = mycmd.ExecuteReader();
            //循环获取列名称
            for (int i = 0; i < myDataReader.FieldCount; i++)
            {
                string columnName = myDataReader.GetName(i);
                al.Add(columnName);
            }
            //关闭DataReader对象
            myDataReader.Close();
            //关闭数据库
            myconn.Close();
            return al;
        }
        /// <summary>
        /// 窗体导入
        /// </summary>
        private void StudentManagement_Load(object sender, EventArgs e)
        {
            //定义SQL语句
            string mySQL1 = "select * from StuInfo";
            //获取列名
            ArrayList al = getColumnName(mySQL1);
            //初始化combobox1和4
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < al.Count; i++)
            {
                comboBox1.Items.Add(al[i]);         //设置检索条件字段
                comboBox4.Items.Add(al[i]);         //设置排序字段
            }
            //初始化单选按钮
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            //初始化combox2
            comboBox2.Items.Clear();
            comboBox2.Items.Add("=");
            comboBox2.Items.Add(">");
            comboBox2.Items.Add("<");
            comboBox2.Items.Add("like");
            //初始化textbox1
            textBox1.Text = "";

            //设置数据dataGridView控件数据源
            operateResult = Administrator.OperateDB(mySQL1);
            daSe = (DataSet)operateResult[1];
            ada = (OleDbDataAdapter)operateResult[0];
            
            //新建dataview对象
            dv = daSe.Tables["information"].DefaultView;

            dataGridView1.DataSource = dv;
        }
        /// <summary>
        /// 点击检索按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string filterStr;    //设置检索条件
            string tx = textBox1.Text;
            if (comboBox2.Text == "like")//模糊查询
            {
                tx = "%" + textBox1 + "%";
            }
            if (comboBox1.Text == "成绩")
            {
                filterStr = comboBox1.Text + comboBox2.Text + tx;
            }
            else
            {
                filterStr = comboBox1.Text + comboBox2.Text + "'" + tx + "'";
            }
            try
            {
                dv.RowFilter = filterStr;//筛选
            }
            catch
            {
                MessageBox.Show("检索错误，请设置正确的检索条件！");
            }
        }
        /// <summary>
        /// 点击排序按钮
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            string sortWay;
            string field = comboBox4.Text;
            if (radioButton1.Checked)
            {
                sortWay = " ASC";
                dv.Sort = field + sortWay;//升序
            }
            else if (radioButton2.Checked)
            {
                sortWay = " DESC";
                dv.Sort = field + sortWay;//降序
            }
            else
            {
                MessageBox.Show("请选择排序方式", "错误提示框", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 点击保存修改
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            //新建bCommandBuilder对象
            OleDbCommandBuilder myBuilder = new OleDbCommandBuilder(ada);
            //保存对数据库的更改
            if (daSe.HasChanges())
            {
                try
                {
                    ada.Update(daSe, "information");
                    MessageBox.Show("保存成功！", "保存提示框", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示框", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("信息未更改，无需保存！", "保存提示框", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 点击退出按钮
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
