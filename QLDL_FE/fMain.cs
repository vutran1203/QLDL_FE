using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLDL.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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

        public class GlobalCustomerDTO
        {
            public string MaKH { get; set; }
            public string TenKH { get; set; }
            public string DiaChi { get; set; }
            public string SoDT { get; set; }
            public string Email { get; set; }
            public string TenChiNhanh { get; set; } // Cột quan trọng
        }

        public class KhachHangDTO
        {
            public string MaKH { get; set; }
            public string TenKH { get; set; }
            public string DiaChi { get; set; }
            public string SoDT { get; set; }
            public string Email { get; set; }
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
            string tenKH = txtTimTenKH.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenKH))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Thông báo");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            // Xóa dữ liệu cũ trên lưới (nếu có)
            // dgvData.DataSource = null; 

            try
            {
                // 1. Xây dựng URL (EscapeDataString để xử lý tiếng Việt/ký tự đặc biệt)
                string url = $"/api/KhachHang/search-local?ten={Uri.EscapeDataString(tenKH)}";

                // 2. Gọi API (Token đã có sẵn trong ApiClient)
                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<KhachHangDTO>>(json); // Dùng lại class DTO cũ

                    // 3. Hiển thị
                    dgvData.DataSource = data;

                    if (data.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng nào tại chi nhánh này.", "Kết quả");
                    }
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {err}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi");
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
            string maNV = txtMaNVThu.Text.Trim(); // Tên textbox của bạn
            string maKH = txtMaKHChi.Text.Trim(); // Tên textbox của bạn

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập Mã NV và Mã KH");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 1. Gọi API
                string url = $"/api/Reporting/tong-tien?maNV_thu={maNV}&maKH_chi={maKH}";
                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // 2. Đọc JSON
                    var jsonString = await response.Content.ReadAsStringAsync();

                    // 3. Parse JSON (Dùng dynamic cho nhanh)
                    dynamic data = JsonConvert.DeserializeObject(jsonString);

                    // 4. Lấy số tiền ra và Format
                    decimal tien = data.tongSoTienDaThu;

                    MessageBox.Show($"Tổng số tiền NV {maNV} thu từ KH {maKH} là:\n\n{tien.ToString("N0")} VNĐ", "Kết quả");
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {err}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTuKhoa.Text.Trim();

            // 1. Validate đầu vào
            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa (Tên khách hàng hoặc SĐT).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 2. Xây dựng URL
                // Lưu ý: Tìm kiếm thường dùng GET
                string url = $"/api/Admin/search-customer-global?keyword={Uri.EscapeDataString(keyword)}";

                // 3. GỌI API (GET)
                // Token Admin đã được gắn sẵn trong ApiClient
                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // 4. Đọc dữ liệu JSON trả về
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // 5. Deserialize JSON thành List Object
                    var data = JsonConvert.DeserializeObject<List<GlobalCustomerDTO>>(jsonResult);

                    // 6. Hiển thị lên DataGridView
                    dgvData.DataSource = data;

                    // Tinh chỉnh giao diện lưới một chút cho đẹp
                    if (dgvData.Columns["TenChiNhanh"] != null)
                    {
                        dgvData.Columns["TenChiNhanh"].HeaderText = "Chi Nhánh (Nguồn)";
                        dgvData.Columns["TenChiNhanh"].Width = 200;
                    }

                    if (data == null || data.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng nào khớp với từ khóa trên toàn hệ thống.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Báo lỗi từ Server trả về
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\r\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Báo lỗi hệ thống/mạng
                MessageBox.Show($"Lỗi kết nối: {ex.Message}\r\n\r\n(Hãy kiểm tra xem API và SQL Server Trụ Sở có đang chạy không)", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Class này dùng để hứng dữ liệu JSON từ API trả về
        public class KhachHangGlobalDTO
        {
            public string MaKH { get; set; }
            public string TenKH { get; set; }
            public string DiaChi { get; set; }
            public string SoDT { get; set; }
            public string Email { get; set; }

            // Cột quan trọng nhất để biết khách hàng thuộc Site nào
            public string TenChiNhanh { get; set; }
        }

        private async void btnTimKiemGlobal_Click(object sender, EventArgs e)
        {
            string keyword = txtTuKhoa.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng cần tìm.", "Thông báo");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            // Xóa dữ liệu cũ trên lưới để tránh nhầm lẫn
            dgvData.DataSource = null;

            try
            {
                // 2. Gọi API Tìm kiếm toàn cục
                // URL này trỏ đến ReportingController mà chúng ta đã làm
                string url = $"/api/Reporting/search?keyword={Uri.EscapeDataString(keyword)}";

                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // 3. Đọc chuỗi JSON
                    var json = await response.Content.ReadAsStringAsync();

                    // 4. Convert JSON sang List DTO (Dùng class vừa tạo ở Bước 1)
                    var data = JsonConvert.DeserializeObject<List<KhachHangGlobalDTO>>(json);

                    // 5. Hiển thị lên DataGridView
                    dgvData.DataSource = data;

                    // 6. Tinh chỉnh giao diện cho đẹp (Tùy chọn)
                    if (data.Count > 0)
                    {
                        // Đổi tên cột cho thân thiện
                        if (dgvData.Columns["MaKH"] != null) dgvData.Columns["MaKH"].HeaderText = "Mã KH";
                        if (dgvData.Columns["TenKH"] != null) dgvData.Columns["TenKH"].HeaderText = "Họ Tên";
                        if (dgvData.Columns["DiaChi"] != null) dgvData.Columns["DiaChi"].HeaderText = "Địa Chỉ";

                        // Cột quan trọng: Nguồn Dữ Liệu
                        if (dgvData.Columns["TenChiNhanh"] != null)
                        {
                            dgvData.Columns["TenChiNhanh"].HeaderText = "🔍 Nguồn Dữ Liệu";
                            dgvData.Columns["TenChiNhanh"].Width = 150;

                            // Tô màu xanh để Admin dễ nhận biết
                            dgvData.Columns["TenChiNhanh"].DefaultCellStyle.ForeColor = Color.Blue;
                            dgvData.Columns["TenChiNhanh"].DefaultCellStyle.Font = new Font(dgvData.Font, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng nào khớp với từ khóa trên toàn hệ thống.", "Kết quả");
                    }
                }
                else
                {
                    // Báo lỗi nếu API trả về lỗi (ví dụ 500, 404)
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\nNội dung: {err}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                // Báo lỗi nếu mất mạng hoặc API chưa bật
                MessageBox.Show($"Lỗi kết nối: {ex.Message}\n(Kiểm tra xem API và SQL Server có đang chạy không)", "Lỗi Hệ Thống");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnXemTatCa_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 1. Gọi API Get All
                // Token đã có sẵn trong ApiClient.Instance
                var response = await ApiClient.Instance.GetAsync("/api/KhachHang/get-all");

                if (response.IsSuccessStatusCode)
                {
                    // 2. Đọc JSON
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // 3. Convert sang List
                    var data = JsonConvert.DeserializeObject<List<KhachHangDTO>>(jsonResult);

                    // 4. Đổ vào Grid
                    dgvData.DataSource = data;

                    // Tinh chỉnh giao diện (Optional)
                    if (data.Count > 0)
                    {
                        // Ví dụ: Đổi tên cột cho đẹp
                        dgvData.Columns["MaKH"].HeaderText = "Mã KH";
                        dgvData.Columns["TenKH"].HeaderText = "Họ Tên";
                        dgvData.Columns["TenKH"].Width = 200;
                    }
                }
                else
                {
                    MessageBox.Show("Không thể tải dữ liệu. Lỗi API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public class KetQuaKiemTraDTO
        {
            public string TenSite { get; set; }
            public string TrangThai { get; set; }
            public string GhiChu { get; set; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // Xóa dữ liệu cũ
            dgvData.DataSource = null;

            try
            {
                // 1. Gọi API (Sẽ mất khoảng 5-40 giây nếu có site chết, nên cần kiên nhẫn)
                var response = await ApiClient.Instance.GetAsync("/api/Reporting/check-connection");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<KetQuaKiemTraDTO>>(json);

                    // 2. Hiển thị lên lưới
                    dgvData.DataSource = data;

                    // 3. Tinh chỉnh giao diện
                    if (dgvData.Columns["TenSite"] != null)
                    {
                        dgvData.Columns["TenSite"].HeaderText = "Tên Chi Nhánh";
                        dgvData.Columns["TenSite"].Width = 150;
                    }
                    if (dgvData.Columns["TrangThai"] != null)
                    {
                        dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
                        dgvData.Columns["TrangThai"].Width = 100;
                    }
                    if (dgvData.Columns["GhiChu"] != null)
                    {
                        dgvData.Columns["GhiChu"].HeaderText = "Ghi Chú Hệ Thống";
                        dgvData.Columns["GhiChu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    MessageBox.Show("Kiểm tra hoàn tất!", "Thông báo");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {error}", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                // Lỗi này thường do API bị tắt hoặc Timeout client quá ngắn
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi Hệ Thống");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public class HoaDonDTO
        {
            public string SoHDN { get; set; }      // Số Hóa Đơn
            public int Thang { get; set; }
            public int Nam { get; set; }
            public string SoHD { get; set; }       // Số Hợp Đồng
            public string MaNV { get; set; }       // Nhân viên lập
            public double SoKwTieuThu { get; set; }
            public decimal SoTien { get; set; }    // Thành tiền
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // Xóa dữ liệu cũ để tránh nhầm lẫn
            dgvData.DataSource = null;

            try
            {
                // 1. Gọi API (Token đã tự động có trong ApiClient)
                // URL này phải khớp với Controller bạn vừa viết
                string url = "/api/HoaDon/get-all-local";

                var response = await ApiClient.Instance.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // 2. Đọc JSON trả về
                    var json = await response.Content.ReadAsStringAsync();

                    // 3. Chuyển JSON thành List danh sách
                    var data = JsonConvert.DeserializeObject<List<HoaDonDTO>>(json);

                    // 4. Đổ dữ liệu vào dgvData
                    dgvData.DataSource = data;

                    // --- Tinh chỉnh giao diện (Optional) ---
                    if (data.Count > 0)
                    {
                        // Đổi tên cột tiếng Việt cho đẹp
                        if (dgvData.Columns["SoHDN"] != null) dgvData.Columns["SoHDN"].HeaderText = "Số Hóa Đơn";
                        if (dgvData.Columns["SoHD"] != null) dgvData.Columns["SoHD"].HeaderText = "Hợp Đồng";
                        if (dgvData.Columns["MaNV"] != null) dgvData.Columns["MaNV"].HeaderText = "NV Lập";

                        // Format tiền: 300000 -> 300,000
                        if (dgvData.Columns["SoTien"] != null)
                        {
                            dgvData.Columns["SoTien"].HeaderText = "Thành Tiền (VNĐ)";
                            dgvData.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                            dgvData.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa có hóa đơn nào tại chi nhánh này.", "Thông báo");
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi tải dữ liệu: {response.ReasonPhrase}\n{error}", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}\n(Kiểm tra lại API hoặc Database)", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public class HopDongDTO
        {
            public string SoHD { get; set; }
            public DateTime NgayKy { get; set; }
            public string MaKH { get; set; }
            public string SoDienKe { get; set; }
            public int KwDinhMuc { get; set; }
            public decimal DongiaKW { get; set; }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            dgvData.DataSource = null;

            try
            {
                // 1. Gọi API
                var response = await ApiClient.Instance.GetAsync("/api/HopDong/get-all-local");

                if (response.IsSuccessStatusCode)
                {
                    // 2. Đọc và Convert dữ liệu
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<HopDongDTO>>(json);

                    // 3. Hiển thị
                    dgvData.DataSource = data;

                    // 4. Tinh chỉnh giao diện (Optional)
                    if (data.Count > 0)
                    {
                        if (dgvData.Columns["SoHD"] != null) dgvData.Columns["SoHD"].HeaderText = "Số Hợp Đồng";

                        if (dgvData.Columns["DongiaKW"] != null)
                        {
                            dgvData.Columns["DongiaKW"].HeaderText = "Đơn Giá";
                            dgvData.Columns["DongiaKW"].DefaultCellStyle.Format = "N0"; // Dạng 2,000
                        }

                        if (dgvData.Columns["NgayKy"] != null)
                        {
                            dgvData.Columns["NgayKy"].HeaderText = "Ngày Ký";
                            dgvData.Columns["NgayKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
