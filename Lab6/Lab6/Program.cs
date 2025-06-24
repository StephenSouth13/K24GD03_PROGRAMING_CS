using Firebase.Database;                       // Thư viện Firebase Realtime Database
using Firebase.Database.Query;                 // Dùng để thực hiện các truy vấn như Child(), PutAsync()...
using FirebaseAdmin;                           // Dùng để khởi tạo Firebase App
using Google.Apis.Auth.OAuth2;                 // Dùng để xác thực với Firebase bằng file JSON
using System;                                  // Thư viện chuẩn
using System.Linq;                             // Dùng để xử lý LINQ như OrderBy, Select, Take...
using System.Text;                             // Dùng để xử lý mã hóa UTF8 cho Console
using System.Threading.Tasks;                  // Dùng để sử dụng async/await bất đồng bộ
using System.IO;                               // Dùng để ghi file CSV

namespace Lab6
{
    // Khai báo lớp Student để lưu thông tin sinh viên
    public class Student
    {
        public string HoTen { get; set; }        // Họ tên sinh viên
        public string Lop { get; set; }          // Lớp
        public int MSSV { get; set; }            // Mã số sinh viên (MSSV)
        public int Diem { get; set; }            // Điểm số
        public string CreatedAt { get; set; }    // Ngày tạo bản ghi
    }

    internal class Program
    {
        private static string firebaseDB_URL = "https://demotest123-598af-default-rtdb.asia-southeast1.firebasedatabase.app/"; // URL đến Firebase Realtime DB
        private static FirebaseClient firebase; // Khởi tạo client để thao tác Firebase

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Cho phép hiển thị tiếng Việt trong Console

            // Khởi tạo Firebase App với thông tin xác thực từ file JSON
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("serviceAccountKey.json")
            });

            // Khởi tạo FirebaseClient với URL đã cấu hình
            firebase = new FirebaseClient(firebaseDB_URL);

            // Hiển thị menu cho người dùng
            while (true)
            {
                Console.WriteLine("===== CHƯƠNG TRÌNH QUẢN LÝ SINH VIÊN =====");
                Console.WriteLine("1. Tạo dữ liệu sinh viên ngẫu nhiên");
                Console.WriteLine("2. Hiển thị toàn bộ danh sách sinh viên");
                Console.WriteLine("3. Cập nhật tên sinh viên theo MSSV");
                Console.WriteLine("4. Cập nhật điểm sinh viên theo MSSV");
                Console.WriteLine("5. Xoá sinh viên theo MSSV");
                Console.WriteLine("6. Xem Top 5 sinh viên có điểm cao nhất");
                Console.WriteLine("7. Nhập sinh viên thủ công");
                Console.WriteLine("8. Xuất danh sách ra file CSV");
                Console.WriteLine("9. Hiển thị danh sách theo điểm giảm dần");
                Console.WriteLine("0. Thoát chương trình");

                var choice = Console.ReadLine(); // Nhận lựa chọn từ người dùng

                switch (choice)
                {
                    case "1": await CreateData(5); break;             // Tạo 5 sinh viên ngẫu nhiên
                    case "2": await ReadData(); break;               // Hiển thị danh sách
                    case "3": await UpdateName(); break;             // Cập nhật tên
                    case "4": await UpdateScore(); break;            // Cập nhật điểm
                    case "5": await DeleteDataByMSSV(); break;       // Xoá sinh viên
                    case "6": await ShowTop5Student(); break;        // Hiển thị Top 5
                    case "7": await AddStudentManually(); break;     // Nhập sinh viên thủ công
                    case "8": await ExportToCSV(); break;            // Xuất file CSV
                    case "9": await SortByScoreElement();break;      //Sắp xếp điểm từ cao xuống thấp
                    case "0": return;                                 // Thoát chương trình
                    default: Console.WriteLine("Lựa chọn không hợp lệ"); break;
                }
            }
        }

        // Hàm tạo dữ liệu sinh viên ngẫu nhiên
        public static async Task CreateData(int count)
        {
            var random = new Random(); // Tạo random để sinh MSSV, lớp, điểm

            for (int i = 0; i < count; i++)
            {
                var sv = new Student
                {
                    HoTen = $"SinhVien_{i}",
                    Lop = $"Lop_{random.Next(1, 7)}",
                    MSSV = random.Next(1000, 9999),
                    Diem = random.Next(1, 11),
                    CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await firebase.Child("Student").Child(sv.MSSV.ToString()).PutAsync(sv); // Ghi vào Firebase

                Console.WriteLine($"Đã thêm: {sv.HoTen} | MSSV: {sv.MSSV} | Lớp: {sv.Lop} | Điểm: {sv.Diem}");
            }
        }

        // Hiển thị toàn bộ danh sách sinh viên
        public static async Task ReadData()
        {
            var students = await firebase.Child("Student").OnceAsync<Student>();
            Console.WriteLine("\nDanh sách sinh viên:");
            foreach (var s in students)
            {
                var sv = s.Object;
                Console.WriteLine($"MSSV: {sv.MSSV} - Tên: {sv.HoTen} - Lớp: {sv.Lop} - Điểm: {sv.Diem}");
            }
        }

        // Cập nhật điểm theo MSSV
        public static async Task UpdateScore()
        {
            Console.Write("Nhập MSSV: ");
            var mssv = Console.ReadLine();
            var student = await firebase.Child("Student").Child(mssv).OnceSingleAsync<Student>();

            if (student == null) { Console.WriteLine("Không tìm thấy MSSV"); return; }

            Console.Write("Nhập điểm mới: ");
            if (int.TryParse(Console.ReadLine(), out int newScore))
            {
                student.Diem = newScore;
                await firebase.Child("Student").Child(mssv).PutAsync(student);
                Console.WriteLine("Cập nhật thành công.");
            }
            else Console.WriteLine("Điểm không hợp lệ");
        }

        // Cập nhật tên theo MSSV
        public static async Task UpdateName()
        {
            Console.Write("Nhập MSSV: ");
            var mssv = Console.ReadLine();
            var student = await firebase.Child("Student").Child(mssv).OnceSingleAsync<Student>();

            if (student == null) { Console.WriteLine("Không tìm thấy MSSV"); return; }

            Console.Write("Nhập tên mới: ");
            var newName = Console.ReadLine();
            student.HoTen = newName;
            await firebase.Child("Student").Child(mssv).PutAsync(student);
            Console.WriteLine("Cập nhật thành công.");
        }

        // Xoá sinh viên theo MSSV
        public static async Task DeleteDataByMSSV()
        {
            Console.Write("Nhập MSSV: ");
            var mssv = Console.ReadLine();
            await firebase.Child("Student").Child(mssv).DeleteAsync();
            Console.WriteLine("Đã xoá sinh viên.");
        }

        // Hiển thị Top 5 sinh viên có điểm cao nhất
        public static async Task ShowTop5Student()
        {
            var all = await firebase.Child("Student").OnceAsync<Student>();
            var top = all.Select(s => s.Object)
                         .OrderByDescending(sv => sv.Diem)
                         .Take(5)
                         .ToList();

            Console.WriteLine("\nTop 5 sinh viên điểm cao nhất:");
            foreach (var sv in top)
            {
                Console.WriteLine($"{sv.HoTen} | MSSV: {sv.MSSV} | Lớp: {sv.Lop} | Điểm: {sv.Diem}");
            }
        }

        // Thêm sinh viên thủ công
        public static async Task AddStudentManually()
        {
            Console.Write("Nhập họ tên: ");
            string hoTen = Console.ReadLine();
            Console.Write("Nhập lớp: ");
            string lop = Console.ReadLine();
            Console.Write("Nhập MSSV: ");
            if (!int.TryParse(Console.ReadLine(), out int mssv)) { Console.WriteLine("MSSV không hợp lệ"); return; }
            Console.Write("Nhập điểm: ");
            if (!int.TryParse(Console.ReadLine(), out int diem)) { Console.WriteLine("Điểm không hợp lệ"); return; }

            var student = new Student
            {
                HoTen = hoTen,
                Lop = lop,
                MSSV = mssv,
                Diem = diem,
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await firebase.Child("Student").Child(mssv.ToString()).PutAsync(student);
            Console.WriteLine("Đã thêm sinh viên.");
        }

        // Xuất danh sách sinh viên ra file CSV
        public static async Task ExportToCSV()
        {
            var students = await firebase.Child("Student").OnceAsync<Student>();
            var list = students.Select(s => s.Object).ToList();
            string fileName = $"DanhSachSinhVien_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine("MSSV,HoTen,Lop,Diem,CreatedAt"); // Dòng tiêu đề CSV
                foreach (var sv in list)
                {
                    writer.WriteLine($"{sv.MSSV},{sv.HoTen},{sv.Lop},{sv.Diem},{sv.CreatedAt}"); // Dòng dữ liệu
                }
            }

            Console.WriteLine($"Đã xuất CSV ra file: {fileName}");
        }
        public static async Task SortByScoreElement()
        {
            var students = await firebase.Child("Student").OnceAsync<Student>();
            var sortedList = students
                .Select(s => s.Object)
                .OrderByDescending(sv => sv.Diem)
                .ToList();

            Console.WriteLine("\nDanh sách sinh viên sắp xếp theo điểm giảm dần:");
            foreach (var sv in sortedList) //Liệt kê từng dòng
            {
                Console.WriteLine($"MSSV: {sv.MSSV} | Tên: {sv.HoTen} | Lớp: {sv.Lop} | Điểm: {sv.Diem}");
            }
        }
        

    }

}
