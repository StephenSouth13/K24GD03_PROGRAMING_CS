using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase.Database;
using Firebase.Database.Query;


namespace Thuc_Hanh_Lab_11
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Coins { get; set; }
        public bool IsActive { get; set; }
        public int VipLevel { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public DateTime LastLogin { get; set; }

    }
    internal class Program
    {
        static async Task<List<Player>> GetPlayersAsync()
        {
            var url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json";
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Player>>(json);
            }
        }

        static async Task PushToFirebase<T>(string path, Dictionary<string, T> data)
        {
            var firebase = new FirebaseClient("https://lab11-a162f-default-rtdb.asia-southeast1.firebasedatabase.app/");
            await firebase.Child(path).PutAsync(data);
            Console.WriteLine($"Đã đẩy dữ liệu lên Firebase tại: {path}");
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var players = await GetPlayersAsync();

            Console.WriteLine("\nCâu 1: Phân tích Tài chính Người chơi");
            var bai1_query =
                from p in players
                where p.Gold > 1000 && p.Coins > 100
                orderby p.Gold descending
                select new { p.Name, p.Gold, p.Coins };

            foreach (var p in bai1_query)
                Console.WriteLine($"{p.Name} | Gold : {p.Gold} | Coins: {p.Coins}");

            // Chuyển sang dictionary để Firebase không đánh chỉ mục từ 0
            var bai1_method = players
                .Where(p => p.Gold > 1000 && p.Coins > 100)
                .OrderByDescending(p => p.Gold)
                .Select((p, index) => new { Index = index + 1, p.Name, p.Gold, p.Coins })
                .ToDictionary(x => x.Index.ToString(), x => new { x.Name, x.Gold, x.Coins });

            await PushToFirebase("quiz_bai1_richPlayers", bai1_method);

            Console.WriteLine("\nCâu 2.1: Tổng số lượng người chơi VIP");
            var totalVip = players.Count(p => p.VipLevel > 0);
            Console.WriteLine("Tổng số người chơi VIP: " + totalVip);

            Console.WriteLine("\nCâu 2.2: Tổng số người chơi VIP theo khu vực");
            var bai2_group =
                from p in players
                where p.VipLevel > 0
                group p by p.Region into g
                select new { Region = g.Key, Count = g.Count() };

            foreach (var g in bai2_group)
            {
                Console.WriteLine($"{g.Region}: {g.Count} người");
            }

            Console.WriteLine("\nCâu 2.3: Người chơi VIP mới đăng nhập");
            var now = new DateTime(2025, 06, 30);
            var bai2_recent = players
                .Where(p => p.VipLevel > 0 && (now - p.LastLogin).TotalDays <= 2)
                .Select((p, index) => new { Index = index + 1, p.Name, p.VipLevel, p.LastLogin })
                .ToDictionary(x => x.Index.ToString(), x => new { x.Name, x.VipLevel, x.LastLogin });

            foreach (var p in bai2_recent.Values)
            {
                Console.WriteLine($"{p.Name} | VIP: {p.VipLevel} | Last Login: {p.LastLogin}");
            }

            await PushToFirebase("quiz_bai2_recentVipPlayers", bai2_recent);
            Console.WriteLine("Hoàn tất chương trình thực hành 11 thầy Hiếu môn Game Program Language");
        }
    }
}
