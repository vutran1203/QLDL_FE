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
    public partial class fThemKhachHang : Form
    {
        public fThemKhachHang()
        {
            InitializeComponent();
        }

        // File: fThemKhachHang.cs

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Tạo đối tượng DTO từ Form (ĐÃ CẬP NHẬT)
            var requestDto = new
            {
                MaKH = txtMaKH.Text,
                TenKH = txtTenKH.Text,

                // ▼▼▼ PHẦN MỚI ▼▼▼
                DiaChi = txtDiaChi.Text,
                SoDT = txtSoDT.Text,
                Email = txtEmail.Text
                // ▲▲▲ HẾT PHẦN MỚI ▲▲▲
            };

            // 2. Đóng gói JSON
            var jsonContent = JsonConvert.SerializeObject(requestDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 3. GỌI API (API Controller mới sẽ đọc 3 trường mới)
                var response = await ApiClient.Instance.PostAsync("/api/KhachHang/them-moi", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm khách hàng mới (đầy đủ) thành công!", "Thành công");
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {error}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối API: {ex.Message}\r\n(Máy con Site đang tắt?)", "Lỗi Mạng");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu
            string maKH = maKHs.Text.Trim(); // Mã khách cần sửa (Bắt buộc)

            // Các trường này nếu để trống ("") thì Backend sẽ giữ nguyên dữ liệu cũ
            var updateData = new
            {
                TenKH = tenKHs.Text.Trim(),
                DiaChi = diaChis.Text.Trim(),
                SoDT = soDTs.Text.Trim(),
                Email = emails.Text.Trim()
            };

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã Khách Hàng cần sửa.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 2. Gọi API PATCH
                string url = $"/api/KhachHang/update-patch/{maKH}";

                // Convert object sang JSON Body
                var jsonBody = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Sử dụng ApiClient để gửi PATCH (Nếu ApiClient chưa có PatchAsync thì dùng PostAsync cũng được, nhưng đúng chuẩn là Patch)
                // Ở đây mình giả dụ dùng HttpClient cơ bản hoặc bạn thêm hàm PatchAsync vào ApiClient
                var response = await ApiClient.PatchAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công! (Chỉ các trường bạn nhập mới được thay đổi)", "Thông báo");
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi cập nhật: {error}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
