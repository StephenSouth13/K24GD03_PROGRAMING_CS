using System;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Database;
namespace FIREBASE_LESSON1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Khởi tạo Firebase...");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("D:\\VTC Academy\\Game Programing Language\\K24GD03_PROGRAMING_CS\\FIREBASE_LESSON1\\FIREBASE_LESSON1\\bin\\Debug\\private_key.json")
            });

            Console.WriteLine("Firebase Admin SDK đã được khởi tạo thành công !");
        }
    }
}
