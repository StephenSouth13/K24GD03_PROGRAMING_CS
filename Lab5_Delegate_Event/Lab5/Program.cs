using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DelegateDemo.DelegateRunExample();
            Delegate_KhaiBao.KhaiBaoRunDemo();
            Anonymos.An_Danh();
            LogSystem.LoggerRun();
            Events.Run();
            Swap.ShowTwo();
            Console.ReadKey();
        }
    }

    class DelegateDemo
    {
        public delegate double Temperature(double temp);

        public static double FahrenheitToCelsius(double temp)
        {
            return ((temp - 32) / 9) * 5;
        }

        public static void DelegateRunExample()
        {
            Temperature tempConversion = new Temperature(FahrenheitToCelsius);
            double tempF = 96;
            double tempC = tempConversion(tempF);

            Console.WriteLine("Temperature in F = {0:F}", tempF);
            Console.WriteLine("Temperature in C = {0:F}", tempC);
        }
    }

    class Delegate_KhaiBao
    {
        // Khai báo delegate
        delegate void MyDelegateVD00();
        delegate void MyDelegateVD01(string s);
        delegate float TinhToanDelegate(float a, float b);
        delegate float TinhToanDeNhan(float c, float d);

        // Các hàm tương ứng với delegate
        static void ShowTextDemo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Đây là ví dụ MyDelegateVD00");
        }

        static void ShowTextDemoWithString(string s)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Xin chào: " + s);
        }

        static float TinhCong(float a, float b)
        {
            return a + b;
        }
        static float TinhNhan(float c, float d)
        {
            return c * d;
        }

        public static void KhaiBaoRunDemo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== DEMO DELEGATE CƠ BẢN ===");

            // VD 00: Delegate không tham số
            MyDelegateVD00 myV00 = new MyDelegateVD00(ShowTextDemo);
            myV00();

            // VD 01: Delegate có tham số string
            MyDelegateVD01 myVD01 = new MyDelegateVD01(ShowTextDemoWithString);
            myVD01?.Invoke("Tom");

            // VD 02: Delegate với tính toán 2 tham số
            TinhToanDelegate tinhtoan = TinhCong;
            Console.WriteLine("Đây là kết quả của phép tính tổng");
            float tong = tinhtoan?.Invoke(10, 5.3f) ?? 0;
            Console.WriteLine("Tổng = " + tong);

            // VD 03: Delegate với tính toán nhân 2 tham số 
            TinhToanDeNhan tinhnhan = TinhNhan;
            Console.WriteLine(" Đây là kết quả của phép tính nhân");
            float nhan = tinhnhan?.Invoke(10, 10) ?? 0;
            Console.WriteLine("Nhân = " + nhan);

            
        }

        // === VD04: CallBack ===
        delegate void MyCallback(string name);

        static void VD04_ShowTenCallBack(MyCallback callback)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập tên của bạn: ");
            string name = Console.ReadLine();
            callback(name);
        }

        static void ShowName(string name)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Xin chào, " + name);
        }

        // Hàm gọi VD04
        public static void VD04()
        {
            VD04_ShowTenCallBack(ShowName);

        }
    }
    class Anonymos
    {
        //Khai báo hàm
        delegate void Xinchao(string chao);


        public static void An_Danh()
        {
            Xinchao btn01 = delegate (string chao)
            {
                Console.Write("BTN01: Xin chào \n " + chao);
            };

            //Gọi Delegate
            btn01("Long");
            Xinchao btn02 = delegate (string chao)
            {
                Console.Write("BTN02: Halo \n " + chao);
            };

            //Gọi Delegate
            btn01("Bình");
            Xinchao btn03 = delegate (string chao)
            {
                Console.Write(" BTN03: Halo \n " + chao);
            };

            //Gọi Delegate
            btn01("Hưng");

            //Gọi hàm string name
            string name = "Yasou";
            //Hàm Lamda
            Xinchao btn04 = (string s) => Console.WriteLine("Lamda: \n " + s);
            {
                btn01?.Invoke("A");
                btn01?.Invoke("B");
                btn01?.Invoke("C");
                btn01?.Invoke(name);
            }
            ;

            

        }
    }

    //Lớp Logger
    public delegate void Logger();
    class LogSystem
    {
        public static void LogToFile() => Console.WriteLine("\n Ghi vào file");
        public static void LogToConsole() => Console.WriteLine("Hiển thị lên Console");
    
    public static void LoggerRun()
        {
            Logger logger = LogSystem.LogToFile;
            logger += LogSystem.LogToConsole;
            logger();//Gọi cả hai hàm
        }
    }
    // Hàm Event 
    public delegate void Display();

    class Events
    {
        public event Display Print; //Event phải public nếu gọi từ ngoài

        public void Show()
        {
            Console.Write("This is an event");
            Print?.Invoke();
        }
        public static void Run()
        {
            Events objEvents = new Events();

            //Đăng ký sự kiện bằng phương thức ẩn danh
            objEvents.Print += () => Console.WriteLine("Sự kiện đã được xử lý !");
            //Gọi hàm trigger sự kiện
            objEvents.Show();
        }
    }
    class Swap
    {
        delegate float HaiSo(float m, float n);


        public static void ShowTwo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Chạy 2 biến m và n");
            float m = 5;
            float n = 10;
            float tam = m;
            m = n;
            n = tam;
            Console.WriteLine("Kết quả của biến là m = {0},n = {1} ", m,n);
        }
    }

}
