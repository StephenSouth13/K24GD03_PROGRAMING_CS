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
            await StartList();
            await IncreaseNum();
            await DecreaseNum();
            await Reverse();
            await Group();
            await DisplayCompanies(); // Hiển thị danh sách công ty
            await DisplayPeopleWithCompanies(); // Hiển thị danh sách người với công ty
        }

        // Phương thức khởi tạo danh sách khách hàng và lọc theo thành phố
        public static async Task StartList()
        {
            List<Customer> list = new List<Customer>
            {
                new Customer { CustomerID = "LoveYourSelf", ContactName = "Hasagi", City = "HCM" },
                new Customer { CustomerID = "Love love", ContactName = "Hasagi", City = "HCM" },
                new Customer { CustomerID = "CellphoneS", ContactName = "Hasagi", City = "HCM" }
            };

            // Lọc danh sách khách hàng theo thành phố "HN"
            var query = from c in list
                        where c.City == "HN"
                        select new { c.City, c.ContactName };

            // In ra tên liên hệ và thành phố
            foreach (var c in query)
                Console.WriteLine($"{c.ContactName} - {c.City}");
        }

        // Phương thức sắp xếp danh sách số theo thứ tự tăng dần
        public static async Task IncreaseNum()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var sortedNumbers = list.OrderBy(n => n);

            Console.WriteLine("Sorted Numbers (Ascending):");
            foreach (int num in sortedNumbers)
            {
                Console.WriteLine(num);
            }
        }

        // Phương thức sắp xếp danh sách số theo thứ tự giảm dần
        public static async Task DecreaseNum()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var sortedDecreaseNumbers = list.OrderByDescending(n => n);

            Console.WriteLine("Sorted Numbers (Descending):");
            foreach (int num in sortedDecreaseNumbers)
            {
                Console.WriteLine(num);
            }
        }

        // Phương thức đảo ngược thứ tự của danh sách số
        public static async Task Reverse()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var revertList = list.AsEnumerable().Reverse();

            Console.WriteLine("Reversed Numbers:");
            foreach (int num in revertList)
            {
                Console.WriteLine(num);
            }
        }

        // Phương thức nhóm số theo chẵn và lẻ
        public static async Task Group()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var groupedNumbers = list.GroupBy(n => n % 2 == 0 ? "Even" : "Odd");

            foreach (var group in groupedNumbers)
            {
                Console.WriteLine($"Group: {group.Key}");
                foreach (var num in group)
                {
                    Console.WriteLine(num);
                }
            }
        }

        // Phương thức hiển thị danh sách công ty
        public static async Task DisplayCompanies()
        {
            var companies = GenerateCompanies();
            Console.WriteLine("List of Companies:");
            foreach (var company in companies)
            {
                Console.WriteLine($"ID: {company.Id}, Name: {company.Name}");
            }
        }

        // Phương thức hiển thị người cùng với công ty mà họ làm việc
        public static async Task DisplayPeopleWithCompanies()
        {
            var people = GenerateListOfPeople();
            var companies = GenerateCompanies();

            // Phép nối (Join) giữa danh sách người và công ty (Method Syntax)
            var peopleWithCompanies = people.Join(companies,
                person => person.CompanyId, // Khóa nối từ danh sách người
                company => company.Id, // Khóa nối từ danh sách công ty
                (person, company) => new { person.FirstName, CompanyName = company.Name }); // Kết quả nối

            Console.WriteLine("People with Companies (Method Syntax):");
            foreach (var item in peopleWithCompanies)
            {
                Console.WriteLine($"{item.FirstName} works at {item.CompanyName}");
            }

            // Phép nối (Join) giữa danh sách người và công ty (Query Syntax)
            var peopleWithCompaniesQuery = from p in people
                                           join c in companies on p.CompanyId equals c.Id
                                           select new { p.FirstName, CompanyName = c.Name };

            Console.WriteLine("People with Companies (Query Syntax):");
            foreach (var item in peopleWithCompaniesQuery)
            {
                Console.WriteLine($"{item.FirstName} works at {item.CompanyName}");
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
            return people;
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
            };
        }
    }
}
