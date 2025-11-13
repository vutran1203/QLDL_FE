using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QLDL.WinForms
{
    public static class ApiClient
    {
        // 1. Tạo một HttpClient duy nhất
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

            // Bỏ qua lỗi chứng chỉ SSL (vì chúng ta dùng localhost)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            // Cập nhật HttpClient để dùng handler này (nếu cần)
            // (Nếu bạn gặp lỗi SSL, chúng ta sẽ thêm dòng trên)
        }

        // 4. Hàm "Giơ thẻ ra vào" (Gắn Token vào Header)
        public static void SetBearerToken(string token)
        {
            JwtToken = token;
            Instance.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}