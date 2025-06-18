using System;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIREBASE_LESSON1
{
    internal class Program
    {
        private static string firebaseDB_URL = "https://wolfcard5may-default-rtdb.asia-southeast1.firebasedatabase.app/";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Khởi tạo Firebase...");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("D:\\VTC Academy\\Game Programing Language\\K24GD03_PROGRAMING_CS\\FIREBASE_LESSON1\\FIREBASE_LESSON1\\bin\\Debug\\private_key.json")
            });

            Console.WriteLine("Firebase Admin SDK đã được khởi tạo thành công!");

            await AddTestData();
            await ReadTestData();
            await DeteleTestData();
            await UpdateTestData();
        }

        public static async Task AddTestData()
        {
            var firebase = new FirebaseClient(firebaseDB_URL);

            var testData = new
            {
                Message = "Hello Firebase!",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await firebase.Child("test").PutAsync(testData);
            Console.WriteLine("✅ Dữ liệu đã được thêm vào Firebase thành công!");
        }
        public static async Task ReadTestData()
        {
            var firebase = new FirebaseClient("https://wolfcard5may-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var testData = await firebase
                .Child("test")
                .OnceSingleAsync<dynamic>();
            Console.WriteLine($"Message: {testData.Message}");
            Console.WriteLine($"Timestamp: {testData.Timestamp}");
        }
       
        public static async Task UpdateTestData()
        {
            var firebase = new FirebaseClient("https://wolfcard5may-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var updatedData = new
            {
                Message = "Updated Message!",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await firebase.Child("test").PatchAsync(updatedData);
            Console.WriteLine("Dữ liệu đã được cập nhật trong Firebase");
        }
        public static async Task DeteleTestData()
        {
            var firebase = new FirebaseClient("https://wolfcard5may-default-rtdb.asia-southeast1.firebasedatabase.app/");
            await firebase.Child("Test").DeleteAsync();
            Console.WriteLine("Dữ liệu đã được xóa khỏi Firebase");

        }
    }
}
