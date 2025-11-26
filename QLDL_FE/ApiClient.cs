using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QLDL.WinForms
{
    public static class ApiClient
    {
        // 1. Khai báo biến (Chưa khởi tạo ngay, để dành cho Constructor lo)
        public static HttpClient Instance { get; private set; }

        // 2. Lưu trữ Token
        public static string JwtToken { get; set; }

        // 3. Constructor tĩnh: Chạy 1 lần duy nhất khi ứng dụng vừa mở
        static ApiClient()
        {
            // --- CẤU HÌNH BỎ QUA LỖI SSL (QUAN TRỌNG) ---
            var handler = new HttpClientHandler();

            // Bỏ qua check chứng chỉ (cho cả .NET Core và .NET Framework cũ)
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => true;

            // --- KHỞI TẠO HTTP CLIENT VỚI HANDLER ĐÓ ---
            Instance = new HttpClient(handler);

            // Cấu hình địa chỉ Base (Dùng IP Radmin của bạn)
            // Lưu ý: Đảm bảo IP này là IP của máy chạy API
            Instance.BaseAddress = new Uri("https://26.172.69.215:7180/");

            // Cấu hình Header JSON
            Instance.DefaultRequestHeaders.Accept.Clear();
            Instance.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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

        // 5. Hàm PATCH (Vì HttpClient cũ không có sẵn)
        public static async Task<HttpResponseMessage> PatchAsync(string endpoint, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            // endpoint chỉ cần là phần đuôi, ví dụ: "api/KhachHang/..."
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            return await Instance.SendAsync(request);
        }
    }
}