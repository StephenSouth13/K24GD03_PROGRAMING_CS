using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_TAP_LAB10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo danh sách các số
            List<int> numbers = new List<int>
            
            {
                12, -5, 0, 7, -14, 25, -1, 3, 8, -9,
                17, -22, 4, -3, 10, -6, 31, -17, 2, -8,
                19, -12, 0, 23, -30, 11, -7, 5, -15, 9,
                26, -18, 6, -4, 13, -2, 16, -20, 1, -10,
                28, -11, 14, -13, 15, -19, 18, -16, 21, -21
            };

            // 1. Tìm các số dương từ 1 đến 12
            Console.OutputEncoding = Encoding.UTF8;
            var positiveNumbers = numbers.Where(n => n > 0 && n <= 12).ToList();
            Console.WriteLine("Viết chương trình tạo số từ 1 đến 12");
            foreach (var num in positiveNumbers)
            {
                Console.WriteLine(num);
            }

            // 2. Tính bình phương các số lớn hơn 10
            Console.OutputEncoding = Encoding.UTF8;
            var squaresOfNumbersGreaterThanTen = numbers.Where(n => n > 10)
                                                        .Select(n => n * n)
                                                        .ToList();
            Console.WriteLine("\nBình phương những số lớn hơn 10:");
            foreach (var square in squaresOfNumbersGreaterThanTen)
            {
                Console.WriteLine(square);
            }
        }
    }
}
