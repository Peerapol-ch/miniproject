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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();

        int eindex;

        string indexE;

        private void UpdateData()
        {
            String sql = "SELECT* FROM TBLAdmin ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "Admin");
            LBID.Text = "";
            TBU.Text = "";
            TBP.Text = "";
        }

        private void UpdateViwe()
        {
            ds.Tables.Remove("ViewAdmin");
            string sql = "SELECT* FROM ViewAdmin ";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, FormMain.DTb);
            da2.Fill(ds, "ViewAdmin");
            DTGCT.DataSource = ds.Tables["ViewAdmin"];
        }
        private void FormAdmin_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string sql = "SELECT* FROM ViewAdmin ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "ViewAdmin");
            DTGCT.DataSource = ds.Tables["ViewAdmin"];

            string sqlFood = "SELECT* FROM TBLAdmin ";
            da = new SqlDataAdapter(sqlFood, FormMain.DTb);
            da.Fill(ds, "Admin");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM TBLAdmin ORDER BY idkey DESC ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(dt);
            int Topid = Convert.ToInt32(dt.Rows[0]["idkey"]) + 1;
            LBID.Text = Topid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "SELECT* FROM TBLAdmin ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Admin");
            //เช็คว่าข้อมูล id ไม่ซ้ำ
            DataRow[] drs = ds.Tables["Admin"].Select("idkey ='" + LBID.Text + "'");
            if (drs.Length == 0)
            {
                DataRow dr = ds.Tables["Admin"].NewRow();
                dr["idkey"] = LBID.Text;
                dr["Username"] = TBU.Text;
                dr["Password"] = TBP.Text;
                ds.Tables["Admin"].Rows.Add(dr);
                UpdateData();
            }
            else
            {
                MessageBox.Show("เลขID ซ้ำ", "ERORR");
            }
            UpdateViwe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการลบข้อมูล Yes / No ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                if (ds.Tables.Contains("SR"))
                {
                    DataRow dr = ds.Tables["SR"].Rows[eindex];
                    DataRow[] dr2 = ds.Tables["Admin"].Select("idkey ='" + dr["idkey"] + "'");
                    dr2[0].Delete();
                }
                else
                {
                    DataRow dr = ds.Tables["Admin"].Rows[eindex];
                    dr.Delete();
                }
                UpdateData();
                ds.Tables["Admin"].AcceptChanges(); //ลบแถวที่ว่าง
                UpdateViwe();
                ds.Tables.Remove("SR");
                MessageBox.Show("ลบข้อมูลเสร็จสิ้น", "ยืนยันการลบ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LBID.Text = "";
            TBU.Text = "";
            TBP.Text = "";
        }

        private void DTGCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            eindex = e.RowIndex;
        }

        private void DTGCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ds.Tables.Remove("Admin");
            String sql = "SELECT* FROM TBLAdmin ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Admin");
            int eedit = e.RowIndex;
            button3.Enabled = true;
            if (ds.Tables.Contains("SR"))
            {
                DataRow dr = ds.Tables["SR"].Rows[eedit];
                indexE = dr["idkey"].ToString();
            }
            else
            {
                DataRow dr = ds.Tables["Admin"].Rows[eindex];
                indexE = dr["idkey"].ToString();
            }
            DataRow[] dr2 = ds.Tables["Admin"].Select("idkey='" + indexE + "'");
            LBID.Text = dr2[0]["idkey"].ToString();
            TBU.Text = dr2[0]["Username"].ToString();
            TBP.Text = dr2[0]["Password"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการแก้ไขข้อมูล Yes / No ", "ยืนยันการแก้ไขข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                DataRow[] dr2 = ds.Tables["Admin"].Select("idkey='" + indexE + "'");
                dr2[0]["idkey"] = LBID.Text;
                dr2[0]["Username"] = TBU.Text;
                dr2[0]["Password"] = TBP.Text;
                UpdateData();
                UpdateViwe();
                MessageBox.Show("แก้ไขข้อมูลเสร็จสิ้น", "ยืนยันการแก้ไข");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("SR"))
            {
                ds.Tables.Remove("SR");
            }
            string sqlsr = "SELECT * FROM ViewAdmin WHERE idkey LIKE '%" + TBSR.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlsr, FormMain.DTb);
            da.Fill(ds, "SR");
            DTGCT.DataSource = ds.Tables["SR"];
        }

        private void DTGCT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
