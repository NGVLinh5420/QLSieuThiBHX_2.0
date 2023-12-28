namespace QLSieuThiBHX
{
    partial class FormHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.msQLNS = new System.Windows.Forms.ToolStripMenuItem();
            this.msiNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.msiKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.msQLHD = new System.Windows.Forms.ToolStripMenuItem();
            this.msiHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.msiChiTietHD = new System.Windows.Forms.ToolStripMenuItem();
            this.msSPKho = new System.Windows.Forms.ToolStripMenuItem();
            this.msTimKiem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiTheoNgay = new System.Windows.Forms.ToolStripMenuItem();
            this.msInCTHD = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msQLNS,
            this.msQLHD,
            this.msSPKho,
            this.msTimKiem,
            this.msInCTHD});
            this.menuStrip.Name = "menuStrip";
            // 
            // msQLNS
            // 
            this.msQLNS.BackColor = System.Drawing.SystemColors.MenuBar;
            this.msQLNS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiNhanVien,
            this.msiKhachHang});
            resources.ApplyResources(this.msQLNS, "msQLNS");
            this.msQLNS.Name = "msQLNS";
            // 
            // msiNhanVien
            // 
            this.msiNhanVien.Name = "msiNhanVien";
            resources.ApplyResources(this.msiNhanVien, "msiNhanVien");
            this.msiNhanVien.Click += new System.EventHandler(this.msiNhanVien_Click);
            // 
            // msiKhachHang
            // 
            this.msiKhachHang.Name = "msiKhachHang";
            resources.ApplyResources(this.msiKhachHang, "msiKhachHang");
            this.msiKhachHang.Click += new System.EventHandler(this.msiKhachHang_Click);
            // 
            // msQLHD
            // 
            this.msQLHD.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiHoaDon,
            this.msiChiTietHD});
            resources.ApplyResources(this.msQLHD, "msQLHD");
            this.msQLHD.Name = "msQLHD";
            // 
            // msiHoaDon
            // 
            this.msiHoaDon.Name = "msiHoaDon";
            resources.ApplyResources(this.msiHoaDon, "msiHoaDon");
            this.msiHoaDon.Click += new System.EventHandler(this.msiHoaDon_Click);
            // 
            // msiChiTietHD
            // 
            this.msiChiTietHD.Name = "msiChiTietHD";
            resources.ApplyResources(this.msiChiTietHD, "msiChiTietHD");
            this.msiChiTietHD.Click += new System.EventHandler(this.msiChiTietHD_Click);
            // 
            // msSPKho
            // 
            resources.ApplyResources(this.msSPKho, "msSPKho");
            this.msSPKho.Name = "msSPKho";
            this.msSPKho.Click += new System.EventHandler(this.msSPKho_Click);
            // 
            // msTimKiem
            // 
            this.msTimKiem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiTheoNgay});
            resources.ApplyResources(this.msTimKiem, "msTimKiem");
            this.msTimKiem.Name = "msTimKiem";
            this.msTimKiem.Click += new System.EventHandler(this.msTimKiem_Click);
            // 
            // msiTheoNgay
            // 
            this.msiTheoNgay.Name = "msiTheoNgay";
            resources.ApplyResources(this.msiTheoNgay, "msiTheoNgay");
            this.msiTheoNgay.Click += new System.EventHandler(this.msiTheoNgay_Click);
            // 
            // msInCTHD
            // 
            resources.ApplyResources(this.msInCTHD, "msInCTHD");
            this.msInCTHD.Name = "msInCTHD";
            this.msInCTHD.Click += new System.EventHandler(this.msINChiTietHD_Click);
            // 
            // FormHome
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormHome";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHome_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem msQLNS;
        private System.Windows.Forms.ToolStripMenuItem msiNhanVien;
        private System.Windows.Forms.ToolStripMenuItem msiKhachHang;
        private System.Windows.Forms.ToolStripMenuItem msQLHD;
        private System.Windows.Forms.ToolStripMenuItem msInCTHD;
        private System.Windows.Forms.ToolStripMenuItem msiChiTietHD;
        private System.Windows.Forms.ToolStripMenuItem msiHoaDon;
        private System.Windows.Forms.ToolStripMenuItem msSPKho;
        private System.Windows.Forms.ToolStripMenuItem msTimKiem;
        private System.Windows.Forms.ToolStripMenuItem msiTheoNgay;
    }
}

