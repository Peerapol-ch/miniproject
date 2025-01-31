using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex13
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        public static string DTb = "Data Source=.\\SQLEXPRESS;Initial Catalog=Peerapol01;Integrated Security=True;";

        private void datebaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            CloseForm(f);
        }

        private void foodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFoods f = new FormFoods();
            CloseForm(f);
        }

        private void drinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDrink f = new FormDrink();
            CloseForm(f);
        }

        private void ของหวานToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDessert f = new FormDessert();
            CloseForm(f);
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMember f = new FormMember();
            CloseForm(f);
        }
        private void CloseForm(Form FC)
        {
            foreach (Form F in this.MdiChildren) // ตรวจสอบหน้าลูกที่เปิดอยู่ทั้งหมด
            {
                if (F.Name != FC.Name) // ตรวจสอบชื่อ form ที่จะเปิด ว่าตรวกัย form ที่เปิดไว้แล้วหรือเปล่า
                {
                    F.Close(); // ปิดform ที่เปิดไว้แล้วไม่ตรงกับformที่จะเปิด
                }
                else
                {
                    return;//เรียก formที่เคยเปิดไว้ขึ้นมา
                }
            }
            FC.MdiParent = this;
            FC.WindowState = FormWindowState.Maximized;
            FC.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        public static string loginstat = "";
        private void FormMain_Load(object sender, EventArgs e)
        {
            FormLogin Flog = new FormLogin();
            Flog.ShowDialog();
            // code ดักควาย
            if (loginstat == "")
            {
                Application.Exit();
            }
        }
    }
}
