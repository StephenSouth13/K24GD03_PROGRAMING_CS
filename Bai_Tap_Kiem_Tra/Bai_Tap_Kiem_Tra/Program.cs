using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bai_Tap_Kiem_Tra
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
        public int VipLevel { get; set; }
    }

    class Program
    {
        static readonly string firebaseUrl = "https://lab11-a162f-default-rtdb.asia-southeast1.firebasedatabase.app/";
        static readonly FirebaseClient firebase = new FirebaseClient(firebaseUrl);
        static readonly DateTime fixedNow = new DateTime(2025, 06, 30, 0, 0, 0, DateTimeKind.Utc);

        static async Task<List<Player>> LoadPlayersAsync()
        {
            var url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/lab12_players.json";
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Player>>(json);
            }
        }

        static async Task PushToFirebase<T>(string path, IEnumerable<T> data)
        {
            var dict = new Dictionary<string, T>();
            int i = 1;
            foreach (var item in data)
            {
                dict[i.ToString()] = item;
                i++;
            }
            await firebase.Child(path).PutAsync(dict);
        }

        static async Task Bai1_1_InactivePlayers(List<Player> players)
        {
            Console.WriteLine("\n--- 1.1. DANH SÁCH NGƯỜI CHƠI KHÔNG HOẠT ĐỘNG GẦN ĐÂY ---");
            

            var result = players.Where(p => p.IsActive == false || (fixedNow - p.LastLogin).TotalDays > 5)
                .Select(p => new
                {
                    p.Name,
                    p.IsActive,
                    LastLogin = p.LastLogin.ToString("u")
                }).ToList();

            foreach (var p in result)
            {
                Console.WriteLine(string.Format("{0,-18}| {1,-10}| {2}", p.Name, p.IsActive, p.LastLogin));
            }

            await PushToFirebase("final_exam_bai1_inactive_players", result);
        }

        static async Task Bai1_2_LowLevelPlayers(List<Player> players)
        {
            Console.WriteLine("\n--- 1.2. DANH SÁCH NGƯỜI CHƠI CẤP THẤP ---");
            

            var result = players.Where(p => p.Level < 10)
                .Select(p => new
                {
                    p.Name,
                    p.Level,
                    CurrentGold = p.Gold
                }).ToList();

            foreach (var p in result)
            {
                Console.WriteLine(string.Format("{0,-18}| {1,-8}| {2:N0}", p.Name, p.Level, p.CurrentGold));
            }

            await PushToFirebase("final_exam_bai1_low_level_players", result);
        }

        static async Task Bai2_Top3VipAward(List<Player> players)
        {
            Console.WriteLine("\n--- 2.1. TOP 3 NGƯỜI CHƠI VIP CẤP ĐỘ CAO NHẤT VÀ GOLD THƯỞNG DỰ KIẾN ---\n");
            

            var top3 = players.Where(p => p.VipLevel > 0)
                .OrderByDescending(p => p.Level)
                .Take(3)
                .Select((p, index) => new
                {
                    Rank = index + 1,
                    p.Name,
                    p.VipLevel,
                    p.Level,
                    CurrentGold = p.Gold,
                    AwardedGoldAmount = (index == 0 ? 2000 : index == 1 ? 1500 : 1000)
                }).ToList();

            foreach (var p in top3)
            {
                Console.WriteLine(string.Format("{0,4} | {1,-20} | {2,9} | {3,5} | {4,14:N0} | {5,12:N0}",
                    p.Rank, p.Name, p.VipLevel, p.Level, p.CurrentGold, p.AwardedGoldAmount));
            }

            await PushToFirebase("final_exam_bai2_top3_vip_awards", top3);
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var players = await LoadPlayersAsync();

            await Bai1_1_InactivePlayers(players);
            await Bai1_2_LowLevelPlayers(players);
            await Bai2_Top3VipAward(players);

            Console.WriteLine("\n>>> Hoàn thành tất cả bài tập. Kết quả đã được đẩy lên Firebase <<<");
        }
    }
}
