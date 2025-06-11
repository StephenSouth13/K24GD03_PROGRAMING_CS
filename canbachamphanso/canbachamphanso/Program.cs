using canbachamphanso;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace canbachamphanso
{

    // Ngoại lệ tùy chỉnh: biểu thức dưới căn < 0
    public class NotNegativeException : Exception
    {
        public NotNegativeException(string message) : base(message) { }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Xin chào đến với chương mục code cùng Thành Long");
            try
            {
                Console.WriteLine("Nhập số nguyên x: ");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Nhập số nguyên y: ");
                int y = int.Parse(Console.ReadLine());

                double H = TinhBieuThuc(x, y);
                Console.WriteLine($"Giá trị H = {H:F4}");

                //Ghi vào file input.txt
                File.WriteAllText("input.txt", $"x ={x}, y ={y}, H = {H:F4}");
                Console.WriteLine("Đã ghi kết quả file input.txt");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Vui lòng nhập lại số nguyên !!!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Lỗi không chia được cho O");
            }
            catch (NotNegativeException ex)
            {
                Console.WriteLine("Lỗi biểu thức trong căn bắt buộc phải lớn hơn 0" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Kết thúc chương trình");
            }
        }
        static double TinhBieuThuc(int x, int y)
        {
            double tu = 3 * x + 2 * y;
            double mau = 2 * x - y;

            if (mau == 0)
            {
                throw new DivideByZeroException("Mẫu số bằng 0!");
            }
            double bieuthuc = tu / mau;

            if (bieuthuc < 0)
            {
                throw new NotNegativeException("Biểu thức dưới căn < 0");
         
            }
            return Math.Sqrt(bieuthuc);
        }
    }
}

