using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text; // Cần cho Encoding.UTF8
using System.Windows.Forms;
using QLDL.WinForms;



namespace QLDL_FE
{
    public partial class fAdminUpdateCN : Form
    {
        public fAdminUpdateCN()
        {
            InitializeComponent();
        }

        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maCN = txtMaCN.Text;
            string tenMoi = txtTenMoi.Text;

            if (string.IsNullOrWhiteSpace(maCN) || string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã CN và Tên Mới.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Xây dựng URL với Query Parameters
                // (Vì API của bạn dùng [FromQuery] cho POST)
                string url = $"/api/Admin/update-branch-name?maCN={Uri.EscapeDataString(maCN)}&tenMoi={Uri.EscapeDataString(tenMoi)}";

                // 2. Tạo một nội dung (body) rỗng, vì POST cần có body
                var httpContent = new StringContent("", Encoding.UTF8, "application/json");

                // 3. GỌI API GIAO DỊCH PHÂN TÁN
                // Token Admin đã được gắn sẵn trong ApiClient
                var response = await ApiClient.Instance.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật tên chi nhánh thành công trên TOÀN BỘ hệ thống (2PC Verified)!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // 4. Báo lỗi (403, 500...)
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\r\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 5. BÁO LỖI NGUY HIỂM (Lỗi 2PC / MSDTC / Linked Server)
                MessageBox.Show($"Lỗi nghiêm trọng: {ex.Message}\r\n\r\n(Hãy đảm bảo MSDTC đã được cấu hình trên cả 2 máy chủ và Linked Server đang hoạt động!)", "Lỗi Giao dịch Phân tán", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
