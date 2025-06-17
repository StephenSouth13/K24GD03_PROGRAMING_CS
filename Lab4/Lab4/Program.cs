using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            SortedListExample();

            StudentListExample();

            ListExample();

            Dictionay();

            QueueExample();

            StackExample();
        }
        static void SortedListExample()
        {
            SortedList mySL = new SortedList();
            mySL.Add("Third", "!");
            mySL.Add("Second", "World");
            mySL.Add("First", "Hello");

            Console.WriteLine("mySL");
            Console.WriteLine("   Count:    {0}", mySL.Count);
            //Console.WriteLine("   Capacity: {0}", mySL.Capacity); // Capacity được thay bằng Count, SortedList không có thuộc tính Capacity

            Console.WriteLine("  Keys and Values:");
            Console.WriteLine("\t-KEY-\t-VALUE-");

            for (int i = 0; i < mySL.Count; i++)
            {
                Console.WriteLine("\t{0}\t{1}", mySL.GetKey(i), mySL.GetByIndex(i));
            }

        }

        static void StudentListExample()
        {
            List<Student> studentList = new List<Student>();

            // Thêm sinh viên
            studentList.Add(new Student(1, "Alice"));
            studentList.Add(new Student(2, "Bob"));
            studentList.Add(new Student(4, "David"));

            // Chèn sinh viên vào vị trí số 1
            studentList.Insert(1, new Student(5, "Eva"));

            // Xóa sinh viên vị trí thứ 3 (index = 3)
            if (studentList.Count > 3)
                studentList.RemoveAt(3);

            // In danh sách sinh viên
            Console.WriteLine("Danh sách sinh viên:");
            foreach (var student in studentList)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
            }

            Console.WriteLine();


        }
        static void ListExample()
        {

            List<string> fruits = new List<string>();
            fruits.Add("Hello");
            fruits.Add("Haloo");
            fruits.Insert(1, "Ni Hảo");
            Console.WriteLine("Contains Halo ?" + fruits.Contains("Hallo"));
            fruits[0] = "A nhan xê ô";
            fruits.Remove("Haloo");
            fruits.RemoveAt(0);

            foreach (var fruit in fruits)
                Console.WriteLine(fruit);
        }
        static void Dictionay()
        {
            Dictionary<string, int> age = new Dictionary<string, int>();
            age.Add("Jackie Chan", 59);
            age.Add("Yasou", 21);

            if (age.ContainsKey("Jackie Chan"))
                Console.WriteLine("Jackie Chan is " + age["Jackie Chan"] + "years old.");

            age["Jackie Chan"] = 28;
            age.Remove("Yasou");

            foreach (var item in age)
                Console.WriteLine($"{item.Key}: {item.Key}");
        }
        static void QueueExample()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Download File");
            queue.Enqueue("Scan file");
            Console.WriteLine("Next task: "+queue.Peek());
            Console.WriteLine("Processing" +queue.Dequeue());

            foreach (var item in queue) Console.WriteLine(item);

        }
        static void StackExample()
        {
            Stack<string> history = new Stack<string>();

            // Thêm các trang vào lịch sử (giống như người dùng đang truy cập các trang)
            history.Push("Page 1");
            history.Push("Page 2");
            history.Push("Page 3");

            // Hiển thị trang hiện tại (trang ở đỉnh stack)
            Console.WriteLine("Current page: " + history.Peek());

            // Quay lại trang trước (Back)
            history.Pop();
            Console.WriteLine("After going back, current page: " + history.Peek());

            // Thêm một trang mới sau khi quay lại (giống như đi đến trang mới)
            history.Push("Page 4");
            Console.WriteLine("Visited new page: " + history.Peek());

            // Hiển thị toàn bộ lịch sử (từ mới nhất đến cũ nhất)
            Console.WriteLine("\nBrowsing history:");
            foreach (var page in history)
            {
                Console.WriteLine(page);
            }

            Console.WriteLine(); // Dòng trống phân cách
        }

    }
}
