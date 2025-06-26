using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class TcpChatServer
{
    private const int Port = 1950;
    private static TcpListener listener;

    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        try
        {
            // Khởi tạo TcpListener để lắng nghe trên mọi địa chỉ IP cục bộ và cổng 1950
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            Console.WriteLine($"Server đang lắng nghe tại cổng {Port}...");

            while (true)
            {
                Console.WriteLine("Đang chờ kết nối client...");
                // Chờ và chấp nhận một kết nối client mới
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"Client đã kết nối từ: {client.Client.RemoteEndPoint}");

                // Xử lý client trong một tác vụ riêng để không chặn server
                _ = HandleClientAsync(client);
            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Lỗi Socket Server: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi Server: {ex.Message}");
        }
        finally
        {
            listener?.Stop();
            Console.WriteLine("Server đã dừng.");
        }
    }

    private static async Task HandleClientAsync(TcpClient client)
    {
        // Lấy NetworkStream để giao tiếp với client
        using (NetworkStream stream = client.GetStream())
        {
            // Sử dụng StreamReader và StreamWriter để đọc/ghi văn bản
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                try
                {
                    writer.AutoFlush = true; // Tự động đẩy dữ liệu đi sau mỗi lần ghi

                    await writer.WriteLineAsync("Chào mừng bạn đến với Chat Server!");

                    string clientMessage;
                    while ((clientMessage = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine($"Nhận từ Client ({client.Client.RemoteEndPoint}): {clientMessage}");
                        if (clientMessage.ToLower() == "exit")
                        {
                            await writer.WriteLineAsync("Tạm biệt!");
                            break;
                        }
                        await writer.WriteLineAsync($"Server đã nhận: {clientMessage}");
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Lỗi I/O với client {client.Client.RemoteEndPoint}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi xử lý client {client.Client.RemoteEndPoint}: {ex.Message}");
                }
                finally
                {
                    client.Close(); // Đảm bảo đóng kết nối client
                    Console.WriteLine($"Client {client.Client.RemoteEndPoint} đã ngắt kết nối.");
                }
            }
        }
    }
}