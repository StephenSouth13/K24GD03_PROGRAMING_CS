using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BÀI_TẬP_BUỔI3
{
    internal class Program
    {   
     
        //Lưu trữ mảng số vừa xuất ra
        List<int> list = new List<int>();
        //Sử dụng lớp Random và lớp Next(maxValue)
        public static void RandomNum(int num, List<int> list)
        {
            Console.WriteLine("Value: " + num);
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("Count Random Numbers");
                int value = random.Next(100);
                Console.WriteLine(value + "");
                list.Add(value);

            }
        }
         //Dùng list để lưu trữ mảng ố nguyên phát sinh
        static void Main(string[] args)
        {
        // Sắp xếp số
            List<int> list = new List<int>();
            RandomNum(100, list);
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Count" + list[i]);
            }
            Console.ReadLine();

        }
        



        
         
    }
}
