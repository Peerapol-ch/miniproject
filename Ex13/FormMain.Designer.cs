namespace Ex13
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.datebaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ของหวานToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datebaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1211, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // datebaseToolStripMenuItem
            // 
            this.datebaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.foodsToolStripMenuItem,
            this.drinkToolStripMenuItem,
            this.ของหวานToolStripMenuItem,
            this.memberToolStripMenuItem});
            this.datebaseToolStripMenuItem.Name = "datebaseToolStripMenuItem";
            this.datebaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.datebaseToolStripMenuItem.Text = "Datebase";
            this.datebaseToolStripMenuItem.Click += new System.EventHandler(this.datebaseToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.adminToolStripMenuItem.Text = "Admin";
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // foodsToolStripMenuItem
            // 
            this.foodsToolStripMenuItem.Name = "foodsToolStripMenuItem";
            this.foodsToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.foodsToolStripMenuItem.Text = "Foods";
            this.foodsToolStripMenuItem.Click += new System.EventHandler(this.foodsToolStripMenuItem_Click);
            // 
            // drinkToolStripMenuItem
            // 
            this.drinkToolStripMenuItem.Name = "drinkToolStripMenuItem";
            this.drinkToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.drinkToolStripMenuItem.Text = "Drink";
            this.drinkToolStripMenuItem.Click += new System.EventHandler(this.drinkToolStripMenuItem_Click);
            // 
            // ของหวานToolStripMenuItem
            // 
            this.ของหวานToolStripMenuItem.Name = "ของหวานToolStripMenuItem";
            this.ของหวานToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.ของหวานToolStripMenuItem.Text = "ของหวาน";
            this.ของหวานToolStripMenuItem.Click += new System.EventHandler(this.ของหวานToolStripMenuItem_Click);
            // 
            // memberToolStripMenuItem
            // 
            this.memberToolStripMenuItem.Name = "memberToolStripMenuItem";
            this.memberToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.memberToolStripMenuItem.Text = "Member";
            this.memberToolStripMenuItem.Click += new System.EventHandler(this.memberToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 656);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "โปรแกรมจัดการร้านอาหาร";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem datebaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ของหวานToolStripMenuItem;
    }
}

