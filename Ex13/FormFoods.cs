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
    public partial class FormFoods : Form
    {
        public FormFoods()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();

        int eindex;

        string indexE;

        private void UpdateData()
        {
            String sql = "SELECT* FROM TBLFood ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "Food");
            LBID.Text = "";
            TBname.Text = "";
            cbType.SelectedIndex = 0;
            TBpr.Text = "";
        }

        private void UpdateViwe() 
        {
            ds.Tables.Remove("ViewFood");
            string sql = "SELECT* FROM ViewFood ";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, FormMain.DTb);
            da2.Fill(ds, "ViewFood");
            DTGCT.DataSource = ds.Tables["ViewFood"];
        }

        private void FormFoods_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string sql = "SELECT* FROM ViewFood ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "ViewFood");
            DTGCT.DataSource = ds.Tables["ViewFood"];

            string sqlType = "SELECT* FROM TBLType ";
            da = new SqlDataAdapter(sqlType, FormMain.DTb);
            da.Fill(ds, "TBLType");
            cbType.DisplayMember = "NameTypeFood";
            cbType.ValueMember = "TBLType";
            cbType.DataSource = ds.Tables["TBLType"];

            string sqlFood = "SELECT* FROM TBLFood ";
            da = new SqlDataAdapter(sqlFood, FormMain.DTb);
            da.Fill(ds,"Food");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM TBLFood ORDER BY ID DESC ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(dt);
            int Topid = Convert.ToInt32(dt.Rows[0]["ID"]) + 1;
            LBID.Text = Topid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "SELECT* FROM TBLFood ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Food");
            //เช็คว่าข้อมูล id ไม่ซ้ำ
            DataRow[] drs = ds.Tables["Food"].Select("ID='" + LBID.Text + "'");
            if (drs.Length == 0)
            {
                DataRow dr = ds.Tables["Food"].NewRow();
                dr["ID"] = LBID.Text;
                dr["NameFood"] = TBname.Text;
                dr["TypeFood"] = cbType.SelectedValue;
                dr["price"] = TBpr.Text;
                ds.Tables["Food"].Rows.Add(dr);
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
                    DataRow[] dr2 = ds.Tables["Food"].Select("ID ='" + dr["ID"] + "'");
                    dr2[0].Delete();
                }
                else
                {
                    DataRow dr = ds.Tables["Food"].Rows[eindex];
                    dr.Delete();
                }
                UpdateData();
                ds.Tables["Food"].AcceptChanges(); //ลบแถวที่ว่าง
                UpdateViwe();
                ds.Tables.Remove("SR");
                MessageBox.Show("ลบข้อมูลเสร็จสิ้น", "ยืนยันการลบ");
            }
        }

        private void DTGCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            eindex = e.RowIndex;
        }

        private void DTGCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ds.Tables.Remove("Food");
            String sql = "SELECT* FROM TBLFood ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Food");
            int eedit = e.RowIndex;
            button3.Enabled = true;
            if (ds.Tables.Contains("SR"))
            {
                DataRow dr = ds.Tables["SR"].Rows[eedit];
                indexE = dr["ID"].ToString();
            }
            else
            {
                DataRow dr = ds.Tables["Food"].Rows[eindex];
                indexE = dr["ID"].ToString();
            }

            DataRow[] dr2 = ds.Tables["Food"].Select("ID='" + indexE + "'");
            LBID.Text = dr2[0]["ID"].ToString();
            TBname.Text = dr2[0]["NameFood"].ToString();
            cbType.SelectedValue = dr2[0]["TypeFood"].ToString();
            TBpr.Text = dr2[0]["price"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการแก้ไขข้อมูล Yes / No ", "ยืนยันการแก้ไขข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                DataRow[] dr2 = ds.Tables["Food"].Select("ID='" + indexE + "'");
                dr2[0]["ID"] = LBID.Text;
                dr2[0]["NameFood"] = TBname.Text;
                dr2[0]["TypeFood"] = cbType.SelectedValue;
                dr2[0]["price"] = TBpr.Text;
                UpdateData();
                UpdateViwe();
                MessageBox.Show("แก้ไขข้อมูลเสร็จสิ้น", "ยืนยันการแก้ไข");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LBID.Text = "";
            TBname.Text = "";
            cbType.SelectedValue = 0;
            TBpr.Text= "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("SR"))
            {
                ds.Tables.Remove("SR");
            }
            string sqlsr = "SELECT * FROM ViewFood WHERE ID LIKE '%" + TBSR.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlsr, FormMain.DTb);
            da.Fill(ds,"SR");
            DTGCT.DataSource = ds.Tables["SR"];
        }
    }
}
