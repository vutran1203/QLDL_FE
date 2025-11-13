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
            var requestDto = new
            {
                SoHD = txtSoHD.Text,
                MaKH = txtMaKH.Text,
                KwDinhMuc = Convert.ToInt32(txtKwDinhMuc.Text), // Cần try-catch ở đây
                DonGiaKW = Convert.ToDecimal(txtDonGiaKW.Text) // Cần try-catch ở đây
            };

            // 2. Đóng gói JSON
            var jsonContent = JsonConvert.SerializeObject(requestDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 3. GỌI API (Token Nhân viên đã được gắn sẵn)
                // API sẽ tự biết kết nối Site 1 hay Site 2 dựa trên Token
                var response = await ApiClient.Instance.PostAsync("/api/HopDong/lap-moi", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Lập hợp đồng mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form
                }
                else
                {
                    // 4. Báo lỗi (SP báo lỗi, vd: "Mã khách hàng không tồn tại")
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi API: {response.StatusCode}\r\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 5. Báo lỗi Mạng (Máy Con bị tắt)
                MessageBox.Show($"Lỗi kết nối API: {ex.Message}\r\n\r\n(Hãy đảm bảo Máy Con (Site) đang được bật!)", "Lỗi Mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
