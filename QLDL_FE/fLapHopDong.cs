using Newtonsoft.Json;
using QLDL.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDL_FE
{
    public partial class fLapHopDong : Form
    {
        public fLapHopDong()
        {
            InitializeComponent();
        }

        private async void btnLuuHopDong_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện
            string soHD = txtSoHD.Text;
            string maKH = txtMaKH.Text;
            string soDienKe = txtSoDienKe.Text; // <--- LẤY TỪ TEXTBOX MỚI THÊM
            string kwDinhMucStr = txtKwDinhMuc.Text;
            string donGiaStr = txtDonGiaKW.Text;

            // 2. Validate (Kiểm tra rỗng)
            if (string.IsNullOrWhiteSpace(soHD) || string.IsNullOrWhiteSpace(maKH) ||
                string.IsNullOrWhiteSpace(soDienKe)) // <--- KIỂM TRA LUÔN CÁI NÀY
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin (bao gồm Số Điện Kế).");
                return;
            }

            // Parse số
            int kwDinhMuc = int.Parse(kwDinhMucStr);
            decimal donGia = decimal.Parse(donGiaStr);

            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 3. Tạo cục dữ liệu (Payload) để gửi đi
                // Tên thuộc tính bên trái phải KHỚP với HopDongRequestDto bên API
                var payload = new
                {
                    SoHD = soHD,
                    MaKH = maKH,
                    SoDienKe = soDienKe, // <--- BỔ SUNG DÒNG QUAN TRỌNG NÀY
                    KwDinhMuc = kwDinhMuc,
                    DongiaKW = donGia,
                    NgayKy = DateTime.Now // Lấy ngày hiện tại
                };

                // 4. Gửi API
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await ApiClient.Instance.PostAsync("/api/HopDong/lap-hop-dong", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Lập hợp đồng thành công!");
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Lỗi: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtKwDinhMuc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
