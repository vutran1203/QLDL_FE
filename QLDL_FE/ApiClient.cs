using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QLDL.WinForms
{
    public static class ApiClient
    {
        // 1. Tạo một HttpClient duy nhất
        // Lưu ý: Để xử lý SSL (localhost), ta nên khởi tạo Handler ngay tại đây nếu cần thiết.
        // Nhưng hiện tại giữ nguyên code của bạn cho đơn giản.
        public static HttpClient Instance { get; } = new HttpClient();

        // 2. Lưu trữ Token sau khi đăng nhập
        public static string JwtToken { get; set; }

        static ApiClient()
        {
            // Tạo handler bỏ qua lỗi SSL
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => true;


            // Đưa handler vào constructor của HttpClient
            Instance = new HttpClient(handler);
            Instance.BaseAddress = new Uri("https://26.172.69.215:7180/");
        }

        // 4. Hàm "Giơ thẻ ra vào" (Gắn Token vào Header)
        public static void SetBearerToken(string token)
        {
            JwtToken = token;
            if (!string.IsNullOrEmpty(token))
            {
                Instance.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Instance.DefaultRequestHeaders.Authorization = null;
            }
        }

        // --- HÀM PATCH ĐÃ ĐƯỢC SỬA CHỮA ---
        public static async Task<HttpResponseMessage> PatchAsync(string endpoint, HttpContent content)
        {
            // 1. Tạo phương thức PATCH
            var method = new HttpMethod("PATCH");

            // 2. Tạo Request
            // Vì Instance.BaseAddress đã set là "https://localhost:7180/", 
            // nên 'endpoint' chỉ cần là phần đuôi (ví dụ: "api/KhachHang/update...")
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            // 3. Gửi lệnh bằng Instance
            // Token đã nằm sẵn trong Instance.DefaultRequestHeaders do hàm SetBearerToken set rồi.
            return await Instance.SendAsync(request);
        }
    }
}