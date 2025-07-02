using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
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
        public DateTime LastLogin { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Firebase installed successfully!");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\Predator\\Documents\\GitHub\\Learning\\Lab11\\fir-bcb83-firebase-adminsdk-fbsvc-a2fb1a32b0.json")
            });

            List<Player> players = await GetPlayerListFromFirebaseAsync();

            if (players == null)
            {
                Console.WriteLine("Không thể tải dữ liệu từ JSON.");
                return;
            }

            Console.WriteLine($"Tổng Vip Players: {players.Count(p => p.VipLevel > 0)}");

            ShowVipCountByRegion(players, "Asia");
            ShowVipCountByRegion(players, "America");
            ShowVipCountByRegion(players, "Europe");

            await SendTopRichPlayersToFirebaseAsync(players);
        }

        static void ShowVipCountByRegion(List<Player> players, string region)
        {
            var count = players.Count(p => p.Region == region && p.VipLevel > 0);
            Console.WriteLine($"Khu vực: {region}, Số người chơi VIP: {count}");
        }

        static async Task SendTopRichPlayersToFirebaseAsync(List<Player> players)
        {
            string databaseUrl = "https://fir-bcb83-default-rtdb.asia-southeast1.firebasedatabase.app/";
            string node = "quiz_bai1_richPlayers";

            using (var httpClient = new HttpClient())
            {
                var findPlayers = players
                    .Where(p => p.Gold > 1000 && p.Coins > 100)
                    .OrderByDescending(p => p.Gold)
                    .Select((p, index) => new
                    {
                        Rank = index + 1,
                        p.Name,
                        p.Gold,
                        p.Coins
                    });

                var dict = findPlayers.ToDictionary(p => p.Rank.ToString(), p => p);

                var json = JsonConvert.SerializeObject(dict);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{databaseUrl}/{node}.json", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine("✅ Đã gửi danh sách top người chơi giàu (key 1,2,3...) lên Firebase.");
            }
        }

        static async Task<List<Player>> GetPlayerListFromFirebaseAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string responseBody = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json");
                    return JsonConvert.DeserializeObject<List<Player>>(responseBody);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Lỗi HTTP khi tải dữ liệu: {e.Message}");
                return null;
            }
        }
    }
}