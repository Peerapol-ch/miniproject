using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex13
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("SELECT* FROM TBLAdmin WHERE Username='{0}' AND Password='{1}'", TBUser.Text, TBpass.Text);
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                string name = dt.Rows[0]["Username"].ToString();
                MessageBox.Show("Login ถูกต้อง\n" + name);
                FormMain.loginstat = "1";
                this.Close();   
            }
            else
            {
                MessageBox.Show("UserName หรือ Password ไม่ถูกต้อง");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TBpass.PasswordChar = '\0';
            }
            else
            {
                TBpass.PasswordChar = '*';
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
