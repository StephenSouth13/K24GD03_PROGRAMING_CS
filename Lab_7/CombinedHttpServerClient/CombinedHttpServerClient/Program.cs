using System.Net;
using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;
namespace CombinedHttpServerClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Bắt đầu tạo một tác vụ riêng biệt
            Task severTask = StartSever();
            //Đợi để sever khởi động
            await Task.Delay(500);

            //Gọi Client để gửi tới yêu cầu tới sever
            await StartClient();

            //Chờ seve kết thúc(nếu có)
            await severTask;
        }
        static async Task StartSever()
        {
            Console.OutputEncoding = Encoding.UTF8;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");

            listener.Start();
            Console.WriteLine("Đang kết nối với sever");

            while (true)
            {
                var context = await listener.GetContextAsync();
                var reponse = context.Response;
                string jsonResponse = "{\"message\" : \"Xin chào,chào mừng bạn trở lại hệ thống !\"}";
                byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
                reponse.ContentLength64 = buffer.Length;
                reponse.ContentType = "application/json";
                await reponse.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                reponse.OutputStream.Close();
            }
        }
        static async Task StartClient()
        {
            using HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStreamAsync("http://localhost:8080/");
                Console.WriteLine("Response from sever: " + response);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
