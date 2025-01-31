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
    public partial class FormMember : Form
    {
        public FormMember()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();

        string indexE;

        int eindex;

        private void UpdateData()
        {
            String sql = "SELECT* FROM TBLCT ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "CT");
            LBID.Text = "";
            TBname.Text = "";
            TBsname.Text = "";
            TBTAL.Text = "";
            textBox1.Text = "";
            cbsex.SelectedIndex = 0;
            cbmember.SelectedIndex = 0;
        }
        private void UpdateViwe()//อัพเดพที่show
        {
            ds.Tables.Remove("ViewCT");
            string sql = "SELECT* FROM ViewCT ";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, FormMain.DTb);
            da2.Fill(ds, "ViewCT");
            DTGCT.DataSource = ds.Tables["ViewCT"];

        }

        private void FormMember_Load(object sender, EventArgs e)
        {
            //ใส่dataลงตาราง
            string sql = "SELECT* FROM ViewCT ";
            SqlDataAdapter da = new SqlDataAdapter(sql,FormMain.DTb);
            da.Fill(ds, "ViewCT");
            DTGCT.DataSource = ds.Tables["ViewCT"];
            //ใส่ตัวเลือกเพศจากdata ลง combobox
            string sqlsex = "SELECT* FROM TBLSEX ";
            da = new SqlDataAdapter(sqlsex, FormMain.DTb);
            da.Fill(ds, "TBLSEX");
            cbsex.DisplayMember = "Namesex";
            cbsex.ValueMember = "IDSEX";
            cbsex.DataSource = ds.Tables["TBLSEX"];
            //ใส่ตัวเลือกสมาชิกจากdata ลง combobox
            string sqlmamber = "SELECT* FROM TBLMAMBER ";
            da = new SqlDataAdapter(sqlmamber, FormMain.DTb);
            da.Fill(ds, "TBLMAMBER");
            cbmember.DisplayMember = "Mamber";
            cbmember.ValueMember = "TypeMamber";
            cbmember.DataSource = ds.Tables["TBLMAMBER"];
            button3.Enabled = false;
            string sqlCT = "SELECT* FROM TBLCT ";
            da = new SqlDataAdapter(sqlCT, FormMain.DTb);
            da.Fill(ds, "CT");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT TOP 1 * FROM TBLCT ORDER BY IDCT DESC ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(dt);
            int Topid = Convert.ToInt32(dt.Rows[0]["IDCT"]) + 1 ;
            LBID.Text = Topid.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LBID.Text = "";
            TBname.Text = "";
            TBsname.Text = "";
            TBTAL.Text = "";
            textBox1.Text = "";
            cbsex.SelectedIndex = 0;
            cbmember.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "SELECT* FROM TBLCT ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "CT");
            //เช็คว่าข้อมูล id ไม่ซ้ำ
            DataRow[] drs = ds.Tables["CT"].Select("IDCT='"+ LBID.Text+"'");
            if (drs.Length == 0)
            {
                DataRow dr = ds.Tables["CT"].NewRow();//เปิดแถวใหม่
                dr["IDCT"] = LBID.Text;
                dr["NameCT"] = TBname.Text;
                dr["Sname"] = TBsname.Text;
                dr["AddressCT"] = textBox1.Text;
                dr["TalCT"] = TBTAL.Text;
                dr["IDSEX"] = cbsex.SelectedValue;
                dr["TypeMamber"] = cbmember.SelectedValue;
                ds.Tables["CT"].Rows.Add(dr);//ใส่ข้อมูลในตาราง
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
            DialogResult dl = MessageBox.Show("คุณต้องการลบข้อมูล Yes / No ","ยืนยันการลบข้อมูล",MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                if (ds.Tables.Contains("SR"))
                {
                    DataRow dr = ds.Tables["SR"].Rows[eindex];
                    DataRow[] dr2 = ds.Tables["CT"].Select("IDCT ='" + dr["IDCT"] + "'");
                    dr2[0].Delete();
                }
                else
                {
                    DataRow dr = ds.Tables["CT"].Rows[eindex];
                    dr.Delete();
                }
                UpdateData();
                ds.Tables["CT"].AcceptChanges(); //ลบแถวที่ว่าง
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
            ds.Tables.Remove("CT");
            String sql = "SELECT* FROM TBLCT ";
            SqlDataAdapter da = new SqlDataAdapter(sql, FormMain.DTb);
            da.Fill(ds, "CT");
            int eedit = e.RowIndex;
            button3.Enabled = true;

            if (ds.Tables.Contains("SR"))
            {
                DataRow dr = ds.Tables["SR"].Rows[eedit];
                indexE = dr["IDCT"].ToString();
            }
            else
            {
                DataRow dr = ds.Tables["CT"].Rows[eindex];
                indexE = dr["IDCT"].ToString();
            }
            DataRow[] dr2 = ds.Tables["CT"].Select("IDCT='" + indexE + "'");
            LBID.Text = dr2[0]["IDCT"].ToString();
            TBname.Text = dr2[0]["NameCT"].ToString();
            TBsname.Text = dr2[0]["Sname"].ToString();
            textBox1.Text = dr2[0]["AddressCT"].ToString();
            TBTAL.Text = dr2[0]["TalCT"].ToString();
            cbsex.SelectedValue = dr2[0]["IDSEX"].ToString();
            cbmember.SelectedValue = dr2[0]["TypeMamber"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             DialogResult dl = MessageBox.Show("คุณต้องการแก้ไขข้อมูล Yes / No ","ยืนยันการแก้ไขข้อมูล",MessageBoxButtons.YesNo);
             if (dl == DialogResult.Yes)
             {
                 DataRow[] dr2 = ds.Tables["CT"].Select("IDCT='" + indexE + "'");
                 dr2[0]["IDCT"] = LBID.Text;
                 dr2[0]["NameCT"] = TBname.Text;
                 dr2[0]["Sname"] = TBsname.Text;
                 dr2[0]["AddressCT"] = textBox1.Text;
                 dr2[0]["TalCT"] = TBTAL.Text;
                 dr2[0]["IDSEX"] = cbsex.SelectedValue;
                 dr2[0]["TypeMamber"] = cbmember.SelectedValue;
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
            string sqlsr = "SELECT * FROM ViewCT WHERE IDCT LIKE '%" + TBSR.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sqlsr, FormMain.DTb);
            da.Fill(ds,"SR");
            DTGCT.DataSource = ds.Tables["SR"];
        }

    }
}
