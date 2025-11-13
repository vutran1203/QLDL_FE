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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        

        // File: fLogin.cs (trong hàm btnLogin_Click)
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var loginData = new {
                MaNV = txtMaNV.Text,
                MatKhau = txtMatKhau.Text
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            try
            {
                // 1. GỌI API LOGIN
                var response = await ApiClient.Instance.PostAsync("/api/auth/login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Dùng try...catch cho việc giải mã JSON và Token
                    try
                    {
                        var tokenResult = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                        string token = tokenResult.token; // <<< Lỗi có thể ở đây

                        if (string.IsNullOrEmpty(token))
                        {
                            MessageBox.Show("API trả về token rỗng!", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        ApiClient.SetBearerToken(token);

                        // 1. Vẫn tạo fMain như cũ
                        fMain frmMain = new fMain(token);

                        // 2. THÊM DÒNG NÀY:
                        //    "Đặt bẫy": Khi frmMain đóng, "this" (frmLogin) sẽ Show()
                        frmMain.FormClosed += (s, args) => this.Show();

                        // 3. Vẫn Show fMain và Hide fLogin như cũ
                        frmMain.Show();
                        this.Hide();
                    }
                    catch (Exception ex_parse) // Bắt lỗi giải mã (Parsing)
                    {
                        MessageBox.Show($"Lỗi giải mã Token: {ex_parse.Message}\r\n\r\nPhản hồi từ API:\r\n{jsonResponse}", "Lỗi Phân tích", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Đăng nhập thất bại: {response.StatusCode} - {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex_connect) // Bắt lỗi kết nối (Timeout, SSL...)
            {
                MessageBox.Show($"Lỗi kết nối API: {ex_connect.Message}", "Lỗi Mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
