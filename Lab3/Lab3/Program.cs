using System;                        // Thư viện cơ bản cho các hàm nhập/xuất (Console, v.v.)
using System.Collections.Generic;    // Dành cho các collection generic như List<T> (chưa dùng ở đây)
using System.Linq;                   // Cho phép dùng LINQ (không dùng, có thể bỏ)
using System.Text;                   // Dùng để xử lý chuỗi (nếu cần)
using System.Collections;           // Bắt buộc để dùng ArrayList và Hashtable
using System.Threading.Tasks;       // Xử lý bất đồng bộ (không dùng ở đây)

namespace Lab3                        // Tên không gian của chương trình
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ------------------- ARRAYLIST -------------------
            ArrayList list01 = new ArrayList();  // Tạo một danh sách ArrayList

            // Thêm các phần tử số nguyên vào list01
            list01.Add(1);
            list01.Add(2);
            list01.Add(3);
            list01.Add(4);
            list01.Add(5);

            // In ra danh sách ban đầu
            for (int i = 0; i < list01.Count; i++)
            {
                Console.WriteLine($"Item {i}: {list01[i]}");
            }

            // Xoá phần tử tại vị trí chỉ số 3 (là số 4)
            list01.RemoveAt(3);

            // Chèn giá trị 30 vào vị trí chỉ số 2
            list01.Insert(2, 30);

            // Chèn giá trị 10 vào vị trí chỉ số 4
            list01.Insert(4, 10);

            // In số lượng phần tử hiện tại
            Console.WriteLine($"list01 Count: {list01.Count}");

            // Tạo list02 và thêm các chuỗi
            ArrayList list02 = new ArrayList();
            list02.Add("C1");
            list02.Add("C2");
            list02.Add("C3");

            // Sửa giá trị tại chỉ số 2
            list02[2] = "C2";  // Thay "C3" thành "C2"

            // Không thể gán giá trị tại index 3 vì hiện list02 chỉ có 3 phần tử (0,1,2)
            // Nếu muốn thêm phần tử mới, dùng Add
            list02.Add(100);

            // Xoá phần tử "C1" khỏi list02
            list02.Remove("C1");

            // Xoá 2 phần tử từ vị trí thứ 3 trong list01
            list01.RemoveRange(3, 2);

            // Xoá toàn bộ phần tử trong list02
            list02.Clear();

            // Chèn list02 (rỗng) vào vị trí 5 của list01
            list01.InsertRange(5, list02); // Không ảnh hưởng vì list02 đang rỗng

            Console.WriteLine($"list01 Count: {list01.Count} ");

            // ------------------- HASHTABLE -------------------
            Hashtable ht01 = new Hashtable(); // Tạo một Hashtable mới

            // Thêm các cặp key-value
            ht01.Add("C1", 1);
            ht01.Add("C2", 2);
            ht01.Add("C3", 3);
            ht01.Add("C4", 4);

            // Cố gắng xoá key 1 (sẽ không thành công vì key là chuỗi, không phải số)
            ht01.Remove(1); // Không có tác dụng

            // Cố gắng xoá key "c" (không tồn tại)
            ht01.Remove("c");

            // Kiểm tra xem key "c" có tồn tại không
            bool hasKey = ht01.ContainsKey("c");

            // Nếu có key "f" thì xoá (trong trường hợp này không có nên không xoá gì)
            if (ht01.ContainsKey("f"))
            {
                ht01.Remove("f");
            }

            // Kiểm tra xem có value 3 hay không
            bool hasValue = ht01.ContainsValue(3); // true
            hasValue = ht01.ContainsValue(6);      // false

            // Duyệt qua Hashtable và in ra các cặp key-value
            foreach (DictionaryEntry item in ht01)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
            //Duyệt qua key
            foreach(var key in ht01.Keys)
            {
                Console.WriteLine(key);
            };
            //Viết màn hình giá trị h01
            Console.WriteLine("========================================");
            foreach(var value in ht01.Values)
            {
                Console.WriteLine(value);
            };
            Hashtable ht2 = (Hashtable)ht01.Clone();
             foreach(var value in ht2)
             {
                Console.WriteLine(value);
            };
            
            //Hàm stack
            Stack myStack = new Stack();
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            var a = myStack.Pop();
            var b = myStack.Peek();
            myStack.Clear();
            myStack.Contains(2);

            //Hàm Queue
            Queue myQueue = new Queue();
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);
            myQueue.Enqueue(3);
            myQueue.Enqueue(4);
            myQueue.Enqueue("I love you 3000");
            var item01 = myQueue.Dequeue();
            Console.WriteLine("Size of Queue"+ myQueue.Count);

            // Chờ người dùng nhấn Enter
            Console.ReadLine();


        }
    }
}
