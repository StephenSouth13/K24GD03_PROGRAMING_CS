using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Mảng array gồm:");
            int[] array = { 1, 2, 3 };
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"Array[{i}] = {array[i]}");
            }

            Console.WriteLine("Nhập vào 2 số a và b:");

            try
            {
                float a = float.Parse(Console.ReadLine());
                float b = float.Parse(Console.ReadLine());
                Console.WriteLine("Thương là: " + TinhThuong(a, b));
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("DivideByZeroException: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine("ArithmeticException: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Khối finally được thực thi.");
            }

            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadLine();
        }

        static float TinhThuong(float a, float b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Không thể chia cho 0.");
            }
            return a / b;
        }

        static void CauseFormatException()
        {
            int x = int.Parse("Hello"); // Gây ra FormatException
        }   

        // Class ngoại lệ tuỳ chỉnh (custom exception)
        public class InvalidAge : Exception
        {
            public InvalidAge(string message) : base(message)
            {
            }
        }
    }
}
