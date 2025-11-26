namespace QLDL_FE
{
    partial class fLapHopDong
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSoHD = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtDonGiaKW = new System.Windows.Forms.TextBox();
            this.txtKwDinhMuc = new System.Windows.Forms.TextBox();
            this.btnLuuHopDong = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoDienKe = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số Hợp đồng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã Khách hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "KW Định mức";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Đơn giá KW";
            // 
            // txtSoHD
            // 
            this.txtSoHD.Location = new System.Drawing.Point(138, 28);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Size = new System.Drawing.Size(100, 22);
            this.txtSoHD.TabIndex = 4;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(138, 69);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(100, 22);
            this.txtMaKH.TabIndex = 5;
            // 
            // txtDonGiaKW
            // 
            this.txtDonGiaKW.Location = new System.Drawing.Point(138, 160);
            this.txtDonGiaKW.Name = "txtDonGiaKW";
            this.txtDonGiaKW.Size = new System.Drawing.Size(100, 22);
            this.txtDonGiaKW.TabIndex = 6;
            // 
            // txtKwDinhMuc
            // 
            this.txtKwDinhMuc.Location = new System.Drawing.Point(138, 113);
            this.txtKwDinhMuc.Name = "txtKwDinhMuc";
            this.txtKwDinhMuc.Size = new System.Drawing.Size(100, 22);
            this.txtKwDinhMuc.TabIndex = 7;
            this.txtKwDinhMuc.TextChanged += new System.EventHandler(this.txtKwDinhMuc_TextChanged);
            // 
            // btnLuuHopDong
            // 
            this.btnLuuHopDong.Location = new System.Drawing.Point(163, 229);
            this.btnLuuHopDong.Name = "btnLuuHopDong";
            this.btnLuuHopDong.Size = new System.Drawing.Size(75, 23);
            this.btnLuuHopDong.TabIndex = 8;
            this.btnLuuHopDong.Text = "Lưu";
            this.btnLuuHopDong.UseVisualStyleBackColor = true;
            this.btnLuuHopDong.Click += new System.EventHandler(this.btnLuuHopDong_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Số điện kế";
            // 
            // txtSoDienKe
            // 
            this.txtSoDienKe.Location = new System.Drawing.Point(138, 201);
            this.txtSoDienKe.Name = "txtSoDienKe";
            this.txtSoDienKe.Size = new System.Drawing.Size(100, 22);
            this.txtSoDienKe.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLuuHopDong);
            this.groupBox1.Controls.Add(this.txtSoDienKe);
            this.groupBox1.Controls.Add(this.txtSoHD);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaKH);
            this.groupBox1.Controls.Add(this.txtDonGiaKW);
            this.groupBox1.Controls.Add(this.txtKwDinhMuc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(192, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 271);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lập hợp đồng";
            // 
            // fLapHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "fLapHopDong";
            this.Text = "fLapHopDong";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSoHD;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.TextBox txtDonGiaKW;
        private System.Windows.Forms.TextBox txtKwDinhMuc;
        private System.Windows.Forms.Button btnLuuHopDong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoDienKe;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}