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
    public partial class FormDessert : Form
    {
        public FormDessert()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();

        int eindex;

        string indexE;
        private void UpdateData()
        {
            String sql = "SELECT* FROM TBLDessert ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "Dessert");
            LBID.Text = "";
            TBname.Text = "";
            cbType.SelectedIndex = 0;
            TBpr.Text = "";
        }

        private void UpdateViwe()
        {
            ds.Tables.Remove("ViewDessert");
            string sql = "SELECT* FROM ViewDessert ";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, FormMain.DTb);
            da2.Fill(ds, "ViewDessert");
            DTGCT.DataSource = ds.Tables["ViewDessert"];
        }
        private void FormDessert_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string sql = "SELECT* FROM ViewDessert ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "ViewDessert");
            DTGCT.DataSource = ds.Tables["ViewDessert"];

            string sqlType = "SELECT* FROM TypeDessert ";
            da = new SqlDataAdapter(sqlType, FormMain.DTb);
            da.Fill(ds, "TypeDessert");
            cbType.DisplayMember = "NameTypeDessert";
            cbType.ValueMember = "TypeDessert";
            cbType.DataSource = ds.Tables["TypeDessert"];

            string sqlFood = "SELECT* FROM TBLDessert ";
            da = new SqlDataAdapter(sqlFood, FormMain.DTb);
            da.Fill(ds, "Dessert");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "SELECT* FROM TBLDessert ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Dessert");
            //เช็คว่าข้อมูล id ไม่ซ้ำ
            DataRow[] drs = ds.Tables["Dessert"].Select("IDDessert='" + LBID.Text + "'");
            if (drs.Length == 0)
            {
                DataRow dr = ds.Tables["Dessert"].NewRow();
                dr["IDDessert"] = LBID.Text;
                dr["DessertName"] = TBname.Text;
                dr["TypeDessert"] = cbType.SelectedValue;
                dr["price"] = TBpr.Text;
                ds.Tables["Dessert"].Rows.Add(dr);
                UpdateData();
            }
            else
            {
                MessageBox.Show("เลขID ซ้ำ", "ERORR");
            }
            UpdateViwe();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM TBLDessert ORDER BY IDDessert DESC ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(dt);
            int Topid = Convert.ToInt32(dt.Rows[0]["IDDessert"]) + 1;
            LBID.Text = Topid.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการลบข้อมูล Yes / No ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                if (ds.Tables.Contains("SR"))
                {
                    DataRow dr = ds.Tables["SR"].Rows[eindex];
                    DataRow[] dr2 = ds.Tables["Dessert"].Select("IDDessert ='" + dr["IDDessert"] + "'");
                    dr2[0].Delete();
                }
                else
                {
                    DataRow dr = ds.Tables["Dessert"].Rows[eindex];
                    dr.Delete();
                }
                UpdateData();
                ds.Tables["Dessert"].AcceptChanges(); //ลบแถวที่ว่าง
                UpdateViwe();
                ds.Tables.Remove("SR");
                MessageBox.Show("ลบข้อมูลเสร็จสิ้น", "ยืนยันการลบ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            DialogResult dl = MessageBox.Show("คุณต้องการแก้ไขข้อมูล Yes / No ", "ยืนยันการแก้ไขข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                DataRow[] dr2 = ds.Tables["Dessert"].Select("IDDessert='" + indexE + "'");
                dr2[0]["IDDessert"] = LBID.Text;
                dr2[0]["DessertName"] = TBname.Text;
                dr2[0]["TypeDessert"] = cbType.SelectedValue;
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
            TBpr.Text = "";
        }

        private void DTGCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            eindex = e.RowIndex;
        }

        private void DTGCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ds.Tables.Remove("Dessert");
            String sql = "SELECT* FROM TBLDessert ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Dessert");
            int eedit = e.RowIndex;
            button3.Enabled = true;
            if (ds.Tables.Contains("SR"))
            {
                DataRow dr = ds.Tables["SR"].Rows[eedit];
                indexE = dr["IDDessert"].ToString();
            }
            else
            {
                DataRow dr = ds.Tables["Dessert"].Rows[eindex];
                indexE = dr["IDDessert"].ToString();
            }

            DataRow[] dr2 = ds.Tables["Dessert"].Select("IDDessert='" + indexE + "'");
            LBID.Text = dr2[0]["IDDessert"].ToString();
            TBname.Text = dr2[0]["DessertName"].ToString();
            cbType.SelectedValue = dr2[0]["TypeDessert"].ToString();
            TBpr.Text = dr2[0]["price"].ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Contains("SR"))
            {
                ds.Tables.Remove("SR");
            }
            string sqlsr = "SELECT * FROM ViewDessert WHERE IDDessert LIKE '%" + TBSR.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlsr, FormMain.DTb);
            da.Fill(ds, "SR");
            DTGCT.DataSource = ds.Tables["SR"];
        }

        private void DTGCT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        }
    }

