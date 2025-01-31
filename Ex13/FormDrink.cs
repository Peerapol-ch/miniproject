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
    public partial class FormDrink : Form
    {
        public FormDrink()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        int eindex;
        string indexE;
        private void UpdateData()
        {
            String sql = "SELECT* FROM TBLDrink ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "Drink");
            LBID.Text = "";
            TBname.Text = "";
            cbType.SelectedIndex = 0;
            TBpr.Text = "";
        }

        private void UpdateViwe()
        {
            ds.Tables.Remove("ViewDrink");
            string sql = "SELECT* FROM ViewDrink ";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, FormMain.DTb);
            da2.Fill(ds, "ViewDrink");
            DTGCT.DataSource = ds.Tables["ViewDrink"];
        }

        private void FormDrink_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string sql = "SELECT* FROM ViewDrink ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "ViewDrink");
            DTGCT.DataSource = ds.Tables["ViewDrink"];

            string sqlType = "SELECT* FROM TypeDrink ";
            da = new SqlDataAdapter(sqlType, FormMain.DTb);
            da.Fill(ds, "TypeDrink");
            cbType.DisplayMember = "NameTypeDrink";
            cbType.ValueMember = "TypeDrink";
            cbType.DataSource = ds.Tables["TypeDrink"];

            string sqlFood = "SELECT* FROM TBLDrink ";
            da = new SqlDataAdapter(sqlFood, FormMain.DTb);
            da.Fill(ds, "Drink");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM TBLDrink ORDER BY DrinkID DESC ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(dt);
            int Topid = Convert.ToInt32(dt.Rows[0]["DrinkID"]) + 1;
            LBID.Text = Topid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "SELECT* FROM TBLDrink ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Drink");
            //เช็คว่าข้อมูล id ไม่ซ้ำ
            DataRow[] drs = ds.Tables["Drink"].Select("DrinkID ='" + LBID.Text + "'");
            if (drs.Length == 0)
            {
                DataRow dr = ds.Tables["Drink"].NewRow();
                dr["DrinkID"] = LBID.Text;
                dr["DrinkName"] = TBname.Text;
                dr["TypeDrink"] = cbType.SelectedValue;
                dr["price"] = TBpr.Text;
                ds.Tables["Drink"].Rows.Add(dr);
                UpdateData();
            }
            else
            {
                MessageBox.Show("เลขID ซ้ำ", "ERORR");
            }
            UpdateViwe();
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
            ds.Tables.Remove("Drink");
            String sql = "SELECT* FROM TBLDrink ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "Drink");
            int eedit = e.RowIndex;
            button3.Enabled = true;
            if (ds.Tables.Contains("SR"))
            {
                DataRow dr = ds.Tables["SR"].Rows[eedit];
                indexE = dr["DrinkID"].ToString();
            }
            else
            {
                DataRow dr = ds.Tables["Drink"].Rows[eindex];
                indexE = dr["DrinkID"].ToString();
            }

            DataRow[] dr2 = ds.Tables["Drink"].Select("DrinkID='" + indexE + "'");
            LBID.Text = dr2[0]["DrinkID"].ToString();
            TBname.Text = dr2[0]["DrinkName"].ToString();
            cbType.SelectedValue = dr2[0]["TypeDrink"].ToString();
            TBpr.Text = dr2[0]["price"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการแก้ไขข้อมูล Yes / No ", "ยืนยันการแก้ไขข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                DataRow[] dr2 = ds.Tables["Drink"].Select("DrinkID='" + indexE + "'");
                dr2[0]["DrinkID"] = LBID.Text;
                dr2[0]["DrinkName"] = TBname.Text;
                dr2[0]["TypeDrink"] = cbType.SelectedValue;
                dr2[0]["price"] = TBpr.Text;
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
            string sqlsr = "SELECT * FROM ViewDrink WHERE DrinkID LIKE '%" + TBSR.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlsr, FormMain.DTb);
            da.Fill(ds, "SR");
            DTGCT.DataSource = ds.Tables["SR"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("คุณต้องการลบข้อมูล Yes / No ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                if (ds.Tables.Contains("SR"))
                {
                    DataRow dr = ds.Tables["SR"].Rows[eindex];
                    DataRow[] dr2 = ds.Tables["Drink"].Select("DrinkID ='" + dr["DrinkID"] + "'");
                    dr2[0].Delete();
                }
                else
                {
                    DataRow dr = ds.Tables["Drink"].Rows[eindex];
                    dr.Delete();
                }
                UpdateData();
                ds.Tables["Drink"].AcceptChanges(); //ลบแถวที่ว่าง
                UpdateViwe();
                ds.Tables.Remove("SR");
                MessageBox.Show("ลบข้อมูลเสร็จสิ้น", "ยืนยันการลบ");
            }
        }
    }
}
