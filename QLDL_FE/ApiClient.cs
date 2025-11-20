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
            // 3. Cấu hình địa chỉ API
            Instance.BaseAddress = new Uri("https://localhost:7180/"); // <<< ĐỊA CHỈ API CỦA BẠN
            Instance.DefaultRequestHeaders.Accept.Clear();
            Instance.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Lưu ý: Phần cấu hình SSL Handler bạn viết trong constructor static 
            // sẽ KHÔNG có tác dụng vì 'Instance' đã được new() ở dòng số 11 rồi.
            // Tuy nhiên, nếu localhost chạy ổn thì không cần sửa.
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