using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class TcpChatClient
{
    private const string ServerIp = "127.0.0.1"; // Địa chỉ IP của Server (localhost)
    private const int Port = 1950;

    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        using (TcpClient client = new TcpClient())
        {
            try
            {
                Console.WriteLine($"Đang kết nối đến server {ServerIp}:{Port}...");
                await client.ConnectAsync(ServerIp, Port);
                Console.WriteLine("Đã kết nối thành công!");

                using (NetworkStream stream = client.GetStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        writer.AutoFlush = true; // Tự động đẩy dữ liệu đi

                        // Đọc tin nhắn chào mừng từ server
                        string welcomeMessage = await reader.ReadLineAsync();
                        Console.WriteLine($"Server: {welcomeMessage}");

                        // Bắt đầu một tác vụ riêng để đọc tin nhắn từ server
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                string serverMessage;
                                while ((serverMessage = await reader.ReadLineAsync()) != null)
                                {
                                    Console.WriteLine($"Server: {serverMessage}");
                                    if (serverMessage == "Tạm biệt!")
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine($"Lỗi I/O khi đọc từ server: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Lỗi khi đọc từ server: {ex.Message}");
                            }
                        });

                        // Gửi tin nhắn từ client đến server
                        Console.WriteLine("Gõ 'exit' để thoát.");
                        string clientMessage;
                        while (true)
                        {
                            Console.Write("Bạn: ");
                            clientMessage = Console.ReadLine();

                            if (string.IsNullOrEmpty(clientMessage)) continue;

                            await writer.WriteLineAsync(clientMessage);

                            if (clientMessage.ToLower() == "exit")
                            {
                                break;
                            }
                            // Client sẽ đọc phản hồi của server trong tác vụ riêng
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Lỗi Socket Client: {ex.Message}");
                Console.WriteLine("Đảm bảo server đang chạy và địa chỉ IP/cổng đúng.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Client: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Đã ngắt kết nối với server.");
            }
        }
    }
}