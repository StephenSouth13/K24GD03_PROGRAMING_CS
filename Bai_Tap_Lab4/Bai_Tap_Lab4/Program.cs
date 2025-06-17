using System;
using System.Collections;
using System.Text;

namespace Bai_Tap_Lab4
{

    struct MatHang
    {
        public int MaMH;
        public string TenMH;
        public int SoLuong;
        public float DonGia;

        public MatHang(int MaMH, string TenMH, int SoLuong, float DonGia)
        {
            this.MaMH = MaMH;
            this.TenMH = TenMH;
            this.SoLuong = SoLuong;
            this.DonGia = DonGia;
        }

        public float ThanhTien()
        {
            return SoLuong * DonGia;
        }

        public override string ToString()
        {
            return $"Mã: {MaMH}, Tên: {TenMH}, SL: {SoLuong}, Đơn giá: {DonGia}, Thành tiền: {ThanhTien()}";
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Hashtable danhsach = new Hashtable();
            string TiepTuc;

            // Nhập hàng
            do
            {
                MatHang mh = NhapMatHang();
                ThemMatHang(danhsach, mh);

                Console.Write("Tiếp tục nhập mặt hàng (y/n): ");
                TiepTuc = Console.ReadLine().ToLower();

            } while (TiepTuc == "y");

            Console.WriteLine("\n=== Danh sách mặt hàng ===");
            XuatDanhSach(danhsach);

            // Tìm kiếm
            Console.Write("\nNhập mã mặt hàng cần tìm và xóa: ");
            int MaTim = int.Parse(Console.ReadLine());

            if (TimMatHang(danhsach, MaTim))
            {
                XoaMatHang(danhsach, MaTim);
                Console.WriteLine("Đã xóa mặt hàng.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy mặt hàng này.");
            }

            Console.WriteLine("\n=== Danh sách sau khi xóa ===");
            XuatDanhSach(danhsach);

            Console.ReadKey();
        }

        static MatHang NhapMatHang()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập mã mặt hàng: ");
            int ma = int.Parse(Console.ReadLine());

            Console.Write("Nhập tên mặt hàng: ");
            string ten = Console.ReadLine();

            Console.Write("Nhập số lượng: ");
            int soluong = int.Parse(Console.ReadLine());

            Console.Write("Nhập đơn giá: ");
            float dongia = float.Parse(Console.ReadLine());

            return new MatHang(ma, ten, soluong, dongia);
        }

        static void ThemMatHang(Hashtable danhsach, MatHang matHang)
        {
            Console.OutputEncoding = Encoding.UTF8;
            danhsach.Add(matHang.MaMH, matHang);
        }

        static bool TimMatHang(Hashtable danhsach, int maHang)
        {
            Console.OutputEncoding = Encoding.UTF8;
            return danhsach.ContainsKey(maHang);
        }

        static void XoaMatHang(Hashtable danhsach, int MaMH)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (TimMatHang(danhsach, MaMH))
                danhsach.Remove(MaMH);
        }

        static void XuatDanhSach(Hashtable danhsach)
        {
            Console.OutputEncoding = Encoding.UTF8;
            foreach (DictionaryEntry item in danhsach)
            {
                MatHang mh = (MatHang)item.Value;
                Console.WriteLine(mh);
            }
        }
    }
}
