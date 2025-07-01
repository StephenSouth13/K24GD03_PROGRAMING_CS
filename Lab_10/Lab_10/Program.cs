using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_10
{
    // Lớp Customer đại diện cho thông tin khách hàng
    public class Customer
    {
        public string CustomerID { get; set; } // ID của khách hàng
        public string ContactName { get; set; } // Tên liên hệ của khách hàng
        public string City { get; set; } // Thành phố của khách hàng
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Gọi các phương thức để thực hiện các tác vụ khác nhau
            await StartList(); // Khởi tạo danh sách khách hàng
            await IncreaseNum(); // Sắp xếp số theo thứ tự tăng dần
            await DecreaseNum(); // Sắp xếp số theo thứ tự giảm dần
            await Reverse(); // Đảo ngược thứ tự của danh sách số
            await Group(); // Nhóm số theo chẵn và lẻ
            await DisplayCompanies(); // Hiển thị danh sách công ty
            await DisplayPeopleWithCompanies(); // Hiển thị danh sách người với công ty
            CheckPeopleData(); // Kiểm tra dữ liệu người
            FindYoungestAndOldestDevs(); // Tìm kiếm developer trẻ nhất và lớn tuổi nhất
        }

        // Phương thức khởi tạo danh sách khách hàng và lọc theo thành phố
        public static async Task StartList()
        {
            // Khởi tạo danh sách khách hàng
            List<Customer> list = new List<Customer>
            {
                new Customer { CustomerID = "LoveYourSelf", ContactName = "Hasagi", City = "HCM" },
                new Customer { CustomerID = "Love love", ContactName = "Hasagi", City = "HCM" },
                new Customer { CustomerID = "CellphoneS", ContactName = "Hasagi", City = "HCM" }
            };

            // Lọc danh sách khách hàng theo thành phố "HN"
            var query = from c in list
                        where c.City == "HN" // Chỉ lấy khách hàng ở thành phố "HN"
                        select new { c.City, c.ContactName };

            // Kiểm tra nếu không có khách hàng nào và thông báo
            if (!query.Any())
            {
                Console.WriteLine("No customers found in HN.");
            }
            else
            {
                // In ra tên liên hệ và thành phố
                foreach (var c in query)
                {
                    Console.WriteLine($"{c.ContactName} - {c.City}");
                }
            }
        }

        // Phương thức sắp xếp danh sách số theo thứ tự tăng dần
        public static async Task IncreaseNum()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var sortedNumbers = list.OrderBy(n => n); // Sắp xếp theo thứ tự tăng dần

            Console.WriteLine("Sorted Numbers (Ascending):");
            foreach (int num in sortedNumbers)
            {
                Console.WriteLine(num); // In từng số đã sắp xếp
            }
        }

        // Phương thức sắp xếp danh sách số theo thứ tự giảm dần
        public static async Task DecreaseNum()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var sortedDecreaseNumbers = list.OrderByDescending(n => n); // Sắp xếp theo thứ tự giảm dần

            Console.WriteLine("Sorted Numbers (Descending):");
            foreach (int num in sortedDecreaseNumbers)
            {
                Console.WriteLine(num); // In từng số đã sắp xếp
            }
        }

        // Phương thức đảo ngược thứ tự của danh sách số
        public static async Task Reverse()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var revertList = list.AsEnumerable().Reverse(); // Đảo ngược danh sách

            Console.WriteLine("Reversed Numbers:");
            foreach (int num in revertList)
            {
                Console.WriteLine(num); // In từng số đã đảo ngược
            }
        }

        // Phương thức nhóm số theo chẵn và lẻ
        public static async Task Group()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var groupedNumbers = list.GroupBy(n => n % 2 == 0 ? "Even" : "Odd"); // Nhóm theo chẵn và lẻ

            foreach (var group in groupedNumbers)
            {
                Console.WriteLine($"Group: {group.Key}"); // In ra tên nhóm
                foreach (var num in group)
                {
                    Console.WriteLine(num); // In từng số trong nhóm
                }
            }
        }

        // Phương thức hiển thị danh sách công ty
        public static async Task DisplayCompanies()
        {
            var companies = GenerateCompanies(); // Tạo danh sách công ty
            Console.WriteLine("List of Companies:");
            foreach (var company in companies)
            {
                Console.WriteLine($"ID: {company.Id}, Name: {company.Name}"); // In ra ID và tên công ty
            }
        }

        // Phương thức hiển thị người cùng với công ty mà họ làm việc
        public static async Task DisplayPeopleWithCompanies()
        {
            var people = GenerateListOfPeople(); // Tạo danh sách người
            var companies = GenerateCompanies(); // Tạo danh sách công ty

            // Phép nối (Join) giữa danh sách người và công ty (Method Syntax)
            var peopleWithCompanies = people.Join(companies,
                person => person.CompanyId, // Khóa nối từ danh sách người
                company => company.Id, // Khóa nối từ danh sách công ty
                (person, company) => new { person.FirstName, CompanyName = company.Name }); // Kết quả nối

            Console.WriteLine("People with Companies (Method Syntax):");
            foreach (var item in peopleWithCompanies)
            {
                Console.WriteLine($"{item.FirstName} works at {item.CompanyName}"); // In ra tên người và công ty
            }

            // Phép nối (Join) giữa danh sách người và công ty (Query Syntax)
            var peopleWithCompaniesQuery = from p in people
                                           join c in companies on p.CompanyId equals c.Id
                                           select new { p.FirstName, CompanyName = c.Name };

            Console.WriteLine("People with Companies (Query Syntax):");
            foreach (var item in peopleWithCompaniesQuery)
            {
                Console.WriteLine($"{item.FirstName} works at {item.CompanyName}"); // In ra tên người và công ty
            }
        }

        // Lớp Person đại diện cho thông tin người
        public class Person
        {
            public string FirstName { get; set; } // Tên
            public string LastName { get; set; } // Họ
            public string Occupation { get; set; } // Nghề nghiệp
            public int Age { get; set; } // Tuổi
            public int CompanyId { get; set; } // ID công ty
        }

        // Phương thức tạo danh sách người
        public static List<Person> GenerateListOfPeople()
        {
            var people = new List<Person>
            {
                new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24, CompanyId = 1 },
                new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40, CompanyId = 1 },
                new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30, CompanyId = 2 },
                new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35, CompanyId = 1 },
                new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24, CompanyId = 2 }
            };
            return people; // Trả về danh sách người
        }

        // Lớp Company đại diện cho thông tin công ty
        public class Company
        {
            public int Id { get; set; } // ID công ty
            public string Name { get; set; } // Tên công ty
        }

        // Phương thức tạo danh sách công ty
        public static List<Company> GenerateCompanies()
        {
            return new List<Company>
            {
                new Company { Id = 1, Name = "Microsoft" },
                new Company { Id = 2, Name = "Google" },
                new Company { Id = 3, Name = "Apple" }
            }; // Trả về danh sách công ty
        }

        // Ví dụ sử dụng LINQ để kiểm tra dữ liệu trong danh sách người
        public static void CheckPeopleData()
        {
            var people = GenerateListOfPeople(); // Tạo danh sách người

            // Kiểm tra xem có người nào không
            bool thereArePeople = people.Any();
            Console.WriteLine($"Are there any people? {thereArePeople}");

            // Kiểm tra xem có developer nào trên 30 tuổi không
            bool anyDevOver30 = people.Any(x => x.Occupation == "Dev" && x.Age > 30);
            Console.WriteLine($"Is there any developer over 30? {anyDevOver30}");
        }

        // Phương thức tìm developer trẻ nhất và lớn tuổi nhất
        public static void FindYoungestAndOldestDevs()
        {
            var people = GenerateListOfPeople(); // Tạo danh sách người

            // Lọc danh sách developer
            var developers = people.Where(p => p.Occupation == "Dev").ToList();

            // Kiểm tra nếu không có developer nào
            if (!developers.Any())
            {
                Console.WriteLine("No developers found.");
                return;
            }

            // Tìm developer trẻ nhất
            var youngestDev = developers.OrderBy(p => p.Age).FirstOrDefault();
            Console.WriteLine($"Youngest Developer: {youngestDev.FirstName} {youngestDev.LastName}, Age: {youngestDev.Age}");

            // Tìm developer lớn tuổi nhất
            var oldestDev = developers.OrderByDescending(p => p.Age).FirstOrDefault();
            Console.WriteLine($"Oldest Developer: {oldestDev.FirstName} {oldestDev.LastName}, Age: {oldestDev.Age}");

            // Tìm 3 developer trẻ nhất
            var threeYoungestDevs = developers.OrderBy(p => p.Age).Take(3);
            Console.WriteLine("Three Youngest Developers:");
            foreach (var dev in threeYoungestDevs)
            {
                Console.WriteLine($"{dev.FirstName} {dev.LastName}, Age: {dev.Age}");
            }

            // Tìm 5 developer lớn tuổi nhất
            var fiveOldestDevs = developers.OrderByDescending(p => p.Age).Take(5);
            Console.WriteLine("Five Oldest Developers:");
            foreach (var dev in fiveOldestDevs)
            {
                Console.WriteLine($"{dev.FirstName} {dev.LastName}, Age: {dev.Age}");
            }
        }
    }
}