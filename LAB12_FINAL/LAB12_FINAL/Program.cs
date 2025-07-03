using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LAB12_FINAL
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

    internal class Program
    {
        static readonly string firebaseURL = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/lab12_players.json";
        static readonly FirebaseClient firebase = new FirebaseClient(firebaseURL);
        static readonly DateTime fixedNow = new DateTime(2025, 06, 30, 0, 0, 0, DateTimeKind.Utc);

        static async Task<List<Player>> LoadPlayerAsync()
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
        static async Task Bai1_1_IsActivePlayer(List<Player>players)
        {
            Console.WriteLine("\nBài 1.1.Danh sách người chơi không hoạt động gần đây.");

            var result = players.Where(p => p.IsActive == false || (fixedNow - p.LastLogin).TotalDays > 5)
                .Select(p => new
                {
                    p.Name,
                    p.IsActive,
                    p.LastLogin,
                }).ToList();
            foreach (var p in result)
            {
                Console.WriteLine(string.Format("{0,-18} | {1,-10} | {2}", p.Name, p.IsActive, p.LastLogin));

            }
            await PushToFirebase("final_exam_bai1_inactive_players", result);
        }
        static async Task Bai1_2_LowLevelPlayers(List<Player>players)
        {
            Console.WriteLine("\nBài 1.2.Danh sách những người chơi cấp thấp");
            var result = players.Where(p => p.Level < 10)
                .Select(p => new
                {
                    p.Name,
                    p.Level,
                    CurrentGold = p.Gold
                }).ToList();
            foreach(var p in result)
            {
                Console.WriteLine(string.Format("{0,-18}| {1,-8}| {2:NO}",p.Name, p.Level,p.CurrentGold));
            }
            await PushToFirebase("final_exam_bai1_low_level_players", result);  
        }
        //Bài 2
        static async Task Bai2_Top3_Vip_Players(List<Player> players)
        {
            Console.WriteLine("\nBài 2.1.Top 3 người chơi VIP có Level cao nhất và Gold được thưởng dự kiến");

            var top3 = players.Where(P => P.VipLevel > 0)
                .OrderByDescending(p => p.Level)
                .Take(3)
                .Select((p, index) => new
                {
                    Rank = index + 1,
                    p.Name,
                    p.VipLevel,
                    CurrentGold = p.Gold,
                    AwardedGoldAmount = (index == 0 ? 2000 : index == 1 ? 1500 : 1000)
                }).ToList();
            foreach (var p in top3)
            {
                Console.WriteLine(string.Format("{0,4} | {1,-20} | {2,9} | {3,5} | {4,14:NO} | {5,12:N0}",
                    p.Rank, p.Name, p.VipLevel, p.CurrentGold, p.AwardedGoldAmount));
            }
            await PushToFirebase("final_exam_bai2_top3_vip_awards", top3);
        }
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var players = await LoadPlayerAsync();

            await Bai1_1_IsActivePlayer(players);
            await Bai1_2_LowLevelPlayers(players);
            await Bai2_Top3_Vip_Players(players);

            Console.WriteLine("Kết thúc môn lập trình Game Programming Language");

        }
    }
}
