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
    public partial class fGhiHoaDon : Form
    {
        public fGhiHoaDon()
        {
            InitializeComponent();
            numThang.Value = DateTime.Now.Month;
            numNam.Value = DateTime.Now.Year;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu nhập (đơn giản)
            if (string.IsNullOrWhiteSpace(txtSoHDN.Text) ||
                string.IsNullOrWhiteSpace(txtSoHD.Text) ||
                string.IsNullOrWhiteSpace(txtSoKwTieuThu.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            // 2. Tạo đối tượng DTO để gửi đi
            var requestDto = new
            {
                SoHDN = txtSoHDN.Text,
                Thang = (int)numThang.Value,
                Nam = (int)numNam.Value,
                SoHD = txtSoHD.Text,
                SoKwTieuThu = Convert.ToDecimal(txtSoKwTieuThu.Text)
            };

            // 3. Đóng gói JSON
            var jsonContent = JsonConvert.SerializeObject(requestDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                // 4. GỌI API (Token đã được gắn sẵn trong ApiClient)
                // Đây là lúc Máy Con PHẢI ĐANG BẬT
                var response = await ApiClient.Instance.PostAsync("/api/HoaDon/ghi-nhan", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ghi nhận hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi thành công
                }
                else
                {
                    // 5. Báo lỗi (403, 400, 500...)
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi: {error}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 6. Báo lỗi Mạng (Máy Con bị tắt)
                MessageBox.Show($"Lỗi kết nối API: {ex.Message}\r\n\r\n(Hãy đảm bảo Máy Con (Site) đang được bật và API đang chạy!)", "Lỗi Mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numNam_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
