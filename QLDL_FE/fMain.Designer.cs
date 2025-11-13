namespace QLDL_FE
{
    partial class fMain
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
            this.btnGhiHoaDon = new System.Windows.Forms.Button();
            this.btnBaoCaoToanCuc = new System.Windows.Forms.Button();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnAdmin2PC = new System.Windows.Forms.Button();
            this.btnLapHopDong = new System.Windows.Forms.Button();
            this.btnThemKhachHang = new System.Windows.Forms.Button();
            this.lTimKH = new System.Windows.Forms.Label();
            this.txtTimMaKH = new System.Windows.Forms.TextBox();
            this.btnTimKhachHang = new System.Windows.Forms.Button();
            this.gbAdmin = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaNVThu = new System.Windows.Forms.TextBox();
            this.txtMaKHChi = new System.Windows.Forms.TextBox();
            this.btnTinhTongTien = new System.Windows.Forms.Button();
            this.gbNhanVien = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbAdmin.SuspendLayout();
            this.gbNhanVien.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGhiHoaDon
            // 
            this.btnGhiHoaDon.Location = new System.Drawing.Point(15, 25);
            this.btnGhiHoaDon.Name = "btnGhiHoaDon";
            this.btnGhiHoaDon.Size = new System.Drawing.Size(163, 24);
            this.btnGhiHoaDon.TabIndex = 0;
            this.btnGhiHoaDon.Text = "Ghi Hóa Đơn";
            this.btnGhiHoaDon.UseVisualStyleBackColor = true;
            this.btnGhiHoaDon.Click += new System.EventHandler(this.btnGhiHoaDon_Click);
            // 
            // btnBaoCaoToanCuc
            // 
            this.btnBaoCaoToanCuc.Location = new System.Drawing.Point(21, 21);
            this.btnBaoCaoToanCuc.Name = "btnBaoCaoToanCuc";
            this.btnBaoCaoToanCuc.Size = new System.Drawing.Size(150, 26);
            this.btnBaoCaoToanCuc.TabIndex = 1;
            this.btnBaoCaoToanCuc.Text = "Báo cáo Toàn cục";
            this.btnBaoCaoToanCuc.UseVisualStyleBackColor = true;
            this.btnBaoCaoToanCuc.Click += new System.EventHandler(this.btnBaoCaoToanCuc_Click);
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new System.Drawing.Point(22, 272);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(39, 16);
            this.lblUserInfo.TabIndex = 2;
            this.lblUserInfo.Text = "User:";
            this.lblUserInfo.Click += new System.EventHandler(this.lblUserInfo_Click);
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(25, 300);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(391, 138);
            this.dgvData.TabIndex = 3;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(422, 408);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(101, 30);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAdmin2PC
            // 
            this.btnAdmin2PC.Location = new System.Drawing.Point(21, 64);
            this.btnAdmin2PC.Name = "btnAdmin2PC";
            this.btnAdmin2PC.Size = new System.Drawing.Size(150, 25);
            this.btnAdmin2PC.TabIndex = 5;
            this.btnAdmin2PC.Text = "Cập nhật Tên CN";
            this.btnAdmin2PC.UseVisualStyleBackColor = true;
            this.btnAdmin2PC.Click += new System.EventHandler(this.btnAdmin2PC_Click);
            // 
            // btnLapHopDong
            // 
            this.btnLapHopDong.Location = new System.Drawing.Point(15, 55);
            this.btnLapHopDong.Name = "btnLapHopDong";
            this.btnLapHopDong.Size = new System.Drawing.Size(163, 28);
            this.btnLapHopDong.TabIndex = 6;
            this.btnLapHopDong.Text = "Lập hợp đồng";
            this.btnLapHopDong.UseVisualStyleBackColor = true;
            this.btnLapHopDong.Click += new System.EventHandler(this.btnLapHopDong_Click);
            // 
            // btnThemKhachHang
            // 
            this.btnThemKhachHang.Location = new System.Drawing.Point(15, 89);
            this.btnThemKhachHang.Name = "btnThemKhachHang";
            this.btnThemKhachHang.Size = new System.Drawing.Size(163, 23);
            this.btnThemKhachHang.TabIndex = 7;
            this.btnThemKhachHang.Text = "Thêm KH";
            this.btnThemKhachHang.UseVisualStyleBackColor = true;
            this.btnThemKhachHang.Click += new System.EventHandler(this.btnThemKhachHang_Click);
            // 
            // lTimKH
            // 
            this.lTimKH.AutoSize = true;
            this.lTimKH.Location = new System.Drawing.Point(43, 126);
            this.lTimKH.Name = "lTimKH";
            this.lTimKH.Size = new System.Drawing.Size(113, 16);
            this.lTimKH.TabIndex = 8;
            this.lTimKH.Text = "Tìm KH (theo Mã):";
            // 
            // txtTimMaKH
            // 
            this.txtTimMaKH.Location = new System.Drawing.Point(46, 154);
            this.txtTimMaKH.Name = "txtTimMaKH";
            this.txtTimMaKH.Size = new System.Drawing.Size(110, 22);
            this.txtTimMaKH.TabIndex = 9;
            // 
            // btnTimKhachHang
            // 
            this.btnTimKhachHang.Location = new System.Drawing.Point(100, 191);
            this.btnTimKhachHang.Name = "btnTimKhachHang";
            this.btnTimKhachHang.Size = new System.Drawing.Size(56, 23);
            this.btnTimKhachHang.TabIndex = 10;
            this.btnTimKhachHang.Text = "Tìm";
            this.btnTimKhachHang.UseVisualStyleBackColor = true;
            this.btnTimKhachHang.Click += new System.EventHandler(this.btnTimKhachHang_Click);
            // 
            // gbAdmin
            // 
            this.gbAdmin.Controls.Add(this.btnTinhTongTien);
            this.gbAdmin.Controls.Add(this.txtMaKHChi);
            this.gbAdmin.Controls.Add(this.txtMaNVThu);
            this.gbAdmin.Controls.Add(this.label3);
            this.gbAdmin.Controls.Add(this.label2);
            this.gbAdmin.Controls.Add(this.label1);
            this.gbAdmin.Controls.Add(this.btnBaoCaoToanCuc);
            this.gbAdmin.Controls.Add(this.btnAdmin2PC);
            this.gbAdmin.Location = new System.Drawing.Point(25, 21);
            this.gbAdmin.Name = "gbAdmin";
            this.gbAdmin.Size = new System.Drawing.Size(244, 237);
            this.gbAdmin.TabIndex = 11;
            this.gbAdmin.TabStop = false;
            this.gbAdmin.Text = "Chức năng Admin (Toàn cục)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tính tổng tiền";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mã NV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mã KH";
            // 
            // txtMaNVThu
            // 
            this.txtMaNVThu.Location = new System.Drawing.Point(84, 130);
            this.txtMaNVThu.Name = "txtMaNVThu";
            this.txtMaNVThu.Size = new System.Drawing.Size(100, 22);
            this.txtMaNVThu.TabIndex = 9;
            // 
            // txtMaKHChi
            // 
            this.txtMaKHChi.Location = new System.Drawing.Point(84, 160);
            this.txtMaKHChi.Name = "txtMaKHChi";
            this.txtMaKHChi.Size = new System.Drawing.Size(100, 22);
            this.txtMaKHChi.TabIndex = 10;
            // 
            // btnTinhTongTien
            // 
            this.btnTinhTongTien.Location = new System.Drawing.Point(103, 188);
            this.btnTinhTongTien.Name = "btnTinhTongTien";
            this.btnTinhTongTien.Size = new System.Drawing.Size(81, 33);
            this.btnTinhTongTien.TabIndex = 11;
            this.btnTinhTongTien.Text = "Tính Tổng";
            this.btnTinhTongTien.UseVisualStyleBackColor = true;
            this.btnTinhTongTien.Click += new System.EventHandler(this.btnTinhTongTien_Click);
            // 
            // gbNhanVien
            // 
            this.gbNhanVien.Controls.Add(this.btnGhiHoaDon);
            this.gbNhanVien.Controls.Add(this.btnLapHopDong);
            this.gbNhanVien.Controls.Add(this.btnTimKhachHang);
            this.gbNhanVien.Controls.Add(this.btnThemKhachHang);
            this.gbNhanVien.Controls.Add(this.txtTimMaKH);
            this.gbNhanVien.Controls.Add(this.lTimKH);
            this.gbNhanVien.Location = new System.Drawing.Point(299, 21);
            this.gbNhanVien.Name = "gbNhanVien";
            this.gbNhanVien.Size = new System.Drawing.Size(200, 237);
            this.gbNhanVien.TabIndex = 12;
            this.gbNhanVien.TabStop = false;
            this.gbNhanVien.Text = "Chức năng nhân viên";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 450);
            this.Controls.Add(this.gbNhanVien);
            this.Controls.Add(this.gbAdmin);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.lblUserInfo);
            this.Name = "fMain";
            this.Text = "Trang chủ";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbAdmin.ResumeLayout(false);
            this.gbAdmin.PerformLayout();
            this.gbNhanVien.ResumeLayout(false);
            this.gbNhanVien.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGhiHoaDon;
        private System.Windows.Forms.Button btnBaoCaoToanCuc;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAdmin2PC;
        private System.Windows.Forms.Button btnLapHopDong;
        private System.Windows.Forms.Button btnThemKhachHang;
        private System.Windows.Forms.Label lTimKH;
        private System.Windows.Forms.TextBox txtTimMaKH;
        private System.Windows.Forms.Button btnTimKhachHang;
        private System.Windows.Forms.GroupBox gbAdmin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTinhTongTien;
        private System.Windows.Forms.TextBox txtMaKHChi;
        private System.Windows.Forms.TextBox txtMaNVThu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbNhanVien;
    }
}