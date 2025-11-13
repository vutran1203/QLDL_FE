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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số Hợp đồng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã Khách hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "KW Định mức";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Đơn giá KW";
            // 
            // txtSoHD
            // 
            this.txtSoHD.Location = new System.Drawing.Point(271, 80);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Size = new System.Drawing.Size(100, 22);
            this.txtSoHD.TabIndex = 4;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(271, 113);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(100, 22);
            this.txtMaKH.TabIndex = 5;
            // 
            // txtDonGiaKW
            // 
            this.txtDonGiaKW.Location = new System.Drawing.Point(271, 182);
            this.txtDonGiaKW.Name = "txtDonGiaKW";
            this.txtDonGiaKW.Size = new System.Drawing.Size(100, 22);
            this.txtDonGiaKW.TabIndex = 6;
            // 
            // txtKwDinhMuc
            // 
            this.txtKwDinhMuc.Location = new System.Drawing.Point(271, 147);
            this.txtKwDinhMuc.Name = "txtKwDinhMuc";
            this.txtKwDinhMuc.Size = new System.Drawing.Size(100, 22);
            this.txtKwDinhMuc.TabIndex = 7;
            // 
            // btnLuuHopDong
            // 
            this.btnLuuHopDong.Location = new System.Drawing.Point(296, 224);
            this.btnLuuHopDong.Name = "btnLuuHopDong";
            this.btnLuuHopDong.Size = new System.Drawing.Size(75, 23);
            this.btnLuuHopDong.TabIndex = 8;
            this.btnLuuHopDong.Text = "Lưu";
            this.btnLuuHopDong.UseVisualStyleBackColor = true;
            this.btnLuuHopDong.Click += new System.EventHandler(this.btnLuuHopDong_Click);
            // 
            // fLapHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLuuHopDong);
            this.Controls.Add(this.txtKwDinhMuc);
            this.Controls.Add(this.txtDonGiaKW);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtSoHD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "fLapHopDong";
            this.Text = "fLapHopDong";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}