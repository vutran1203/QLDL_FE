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

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            var requestDto = new
            {
                MaKH = txtMaKH.Text,
                TenKH = txtTenKH.Text
            };

            var jsonContent = JsonConvert.SerializeObject(requestDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            this.Cursor = Cursors.WaitCursor;
            try
            {
                // Token Nhân viên đã được gắn sẵn
                var response = await ApiClient.Instance.PostAsync("/api/KhachHang/them-moi", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm khách hàng mới thành công!", "Thành công");
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
    }
}
