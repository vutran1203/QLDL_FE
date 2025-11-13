using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLDL.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IdentityModel.Tokens.Jwt;

namespace QLDL_FE
{
    public partial class fMain : Form
    {

        private readonly string _jwtToken;
        private string _maNV;
        private string _maCN;
        public fMain(string token)
        {
            InitializeComponent();
            _jwtToken = token; // Cất token vào "túi"
            ParseToken(_jwtToken);
        }

        private void ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Lấy "ADN" từ Token
            _maNV = jsonToken.Claims.First(claim => claim.Type == "maNV").Value;
            _maCN = jsonToken.Claims.First(claim => claim.Type == "maCN").Value;
        }

        private void btnGhiHoaDon_Click(object sender, EventArgs e)
        {
            // Khi nhấn nút, mở form Ghi Hóa đơn
            fGhiHoaDon f = new fGhiHoaDon();
            f.ShowDialog(); // ShowDialog để nó "nổi" lên trên và khóa fMain
        }

        private async void btnBaoCaoToanCuc_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            dgvData.DataSource = null; // Xóa dữ liệu cũ

            try
            {
                // 1. GỌI API BÁO CÁO
                // Token đã được gắn sẵn trong ApiClient khi đăng nhập
                var response = await ApiClient.Instance.GetAsync("/api/reporting/vuot-dinh-muc");

                if (response.IsSuccessStatusCode)
                {
                    // 2. Đọc kết quả JSON (danh sách hóa đơn)
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // 3. Dùng Newtonsoft.Json để "giải nén" JSON thành một danh sách
                    var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    // 4. Đổ dữ liệu vào DataGridView
                    dgvData.DataSource = data;
                    MessageBox.Show("Tải báo cáo toàn cục thành công!");
                }
                else
                {
                    // 5. Báo lỗi (nếu API trả về 400, 500...)
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\r\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 6. Báo lỗi Mạng (nếu 1 trong 2 Site Con bị tắt)
                // Đây là lúc bạn sẽ thấy lỗi "Linked server is not available..."
                MessageBox.Show($"Lỗi kết nối hoặc Linked Server: {ex.Message}\r\n\r\n(Hãy đảm bảo cả 3 máy chủ đều đang bật!)", "Lỗi Mạng/CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Trả con trỏ chuột về bình thường
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"User: {_maNV} (Chi nhánh: {_maCN})";

            bool isNhanVien = (_maCN != "TS01");

            gbNhanVien.Visible = isNhanVien; 
            gbAdmin.Visible = !isNhanVien;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. "Vứt" Token toàn cục
            ApiClient.SetBearerToken(null);

            // 2. Thông báo (Tùy chọn, nhưng nên có)
            MessageBox.Show("Bạn đã đăng xuất.");

            // 3. Đóng Form Chính (fMain)
            this.Close();
        }

        private void btnAdmin2PC_Click(object sender, EventArgs e)
        {
            fAdminUpdateCN frm = new fAdminUpdateCN();
            frm.ShowDialog();
        }

        private void btnLapHopDong_Click(object sender, EventArgs e)
        {
            fLapHopDong frm = new fLapHopDong();
            frm.ShowDialog();
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            fThemKhachHang frm = new fThemKhachHang();
            frm.ShowDialog();
        }

        private async void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            string maKH = txtTimMaKH.Text;
            if (string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã Khách hàng cần tìm.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            dgvData.DataSource = null; // Xóa lưới

            try
            {
                // 1. GỌI API GET (Token NV đã được gắn sẵn)
                // API sẽ tự biết kết nối Site 1 hay Site 2
                var response = await ApiClient.Instance.GetAsync($"/api/KhachHang/{maKH}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Vì chỉ trả về 1 khách hàng, chúng ta cho nó vào 1 danh sách tạm
                    var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    dgvData.DataSource = new List<dynamic> { data };
                    // (Nếu không thấy, có thể KH đó ở Site khác)
                }
                else
                {
                    // Lỗi (ví dụ 404 Not Found nếu KH không có ở Site này)
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\r\n{error}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi Mạng");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblUserInfo_Click(object sender, EventArgs e)
        {

        }

        private async void btnTinhTongTien_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNVThu.Text;
            string maKH = txtMaKHChi.Text;

            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã NV và Mã KH.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Xây dựng URL với Query Parameters
                string url = $"/api/Reporting/tong-tien?maNV_thu={Uri.EscapeDataString(maNV)}&maKH_chi={Uri.EscapeDataString(maKH)}";

                // 2. GỌI API (Token Admin đã được gắn sẵn)
                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                { 
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                // 4. Hiển thị kết quả
                MessageBox.Show($"Tổng số tiền NV {data.MaNhanVien} đã thu từ KH {data.MaKhachHang} là: {data.TongSoTienDaThu}", "Kết quả Báo cáo (Câu 1)");
                }
                else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi API: {error}", "Lỗi");
            }
        }
    catch (Exception ex)
    {
        // 5. Báo lỗi Mạng (Nếu 1 trong 2 Site Con bị tắt khi SP đang chạy)
        MessageBox.Show($"Lỗi kết nối hoặc Linked Server: {ex.Message}\r\n(Hãy đảm bảo cả 2 Máy Con đều đang bật!)", "Lỗi Mạng/CSDL");
    }
    finally
    {
        this.Cursor = Cursors.Default;
    }
}
    }
}
