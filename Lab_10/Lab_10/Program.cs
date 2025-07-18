﻿using System;
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
            QueryOlderDevelopers(); // Truy vấn các developer lớn tuổi
            FilterYoungestAndSmartestDev(); // Lọc ra developer trẻ nhất và học giỏi nhất
            CountAlternatingDevs(); // Đếm xen kẽ 1 developer già và 1 developer trẻ
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

            var query = from c in list
                        where c.City == "HN"
                        select new { c.City, c.ContactName };

            if (!query.Any())
            {
                Console.WriteLine("No customers found in HN.");
            }
            else
            {
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

            var peopleWithCompanies = people.Join(companies,
                person => person.CompanyId,
                company => company.Id,
                (person, company) => new { person.FirstName, CompanyName = company.Name });

            Console.WriteLine("People with Companies (Method Syntax):");
            foreach (var item in peopleWithCompanies)
            {
                Console.WriteLine($"{item.FirstName} works at {item.CompanyName}");
            }

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
            public double Score { get; set; } // Điểm số
        }

        // Phương thức tạo danh sách người
        public static List<Person> GenerateListOfPeople()
        {
            var people = new List<Person>
            {
                new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24, CompanyId = 1, Score = 85 },
                new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40, CompanyId = 1, Score = 90 },
                new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30, CompanyId = 2, Score = 78 },
                new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35, CompanyId = 1, Score = 95 },
                new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24, CompanyId = 2, Score = 88 }
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

        // Kiểm tra dữ liệu người
        public static void CheckPeopleData()
        {
            var people = GenerateListOfPeople();

            bool thereArePeople = people.Any();
            Console.WriteLine($"Are there any people? {thereArePeople}");

            bool anyDevOver30 = people.Any(x => x.Occupation == "Dev" && x.Age > 30);
            Console.WriteLine($"Is there any developer over 30? {anyDevOver30}");
        }

        // Tìm developer trẻ nhất và lớn tuổi nhất
        public static void FindYoungestAndOldestDevs()
        {
            var people = GenerateListOfPeople();

            var developers = people.Where(p => p.Occupation == "Dev").ToList();

            if (!developers.Any())
            {
                Console.WriteLine("No developers found.");
                return;
            }

            var youngestDev = developers.OrderBy(p => p.Age).FirstOrDefault();
            Console.WriteLine($"Youngest Developer: {youngestDev.FirstName} {youngestDev.LastName}, Age: {youngestDev.Age}");

            var oldestDev = developers.OrderByDescending(p => p.Age).FirstOrDefault();
            Console.WriteLine($"Oldest Developer: {oldestDev.FirstName} {oldestDev.LastName}, Age: {oldestDev.Age}");

            var threeYoungestDevs = developers.OrderBy(p => p.Age).Take(3);
            Console.WriteLine("Three Youngest Developers:");
            foreach (var dev in threeYoungestDevs)
            {
                Console.WriteLine($"{dev.FirstName} {dev.LastName}, Age: {dev.Age}");
            }

            var twoOldestDevs = developers.OrderByDescending(p => p.Age).Take(2);
            Console.WriteLine("Two Oldest Developers:");
            foreach (var dev in twoOldestDevs)
            {
                Console.WriteLine($"{dev.FirstName} {dev.LastName}, Age: {dev.Age}");
            }
        }

        // Lọc ra developer trẻ nhất và học giỏi nhất
        public static void FilterYoungestAndSmartestDev()
        {
            var people = GenerateListOfPeople();

            var smartestDev = people.Where(p => p.Occupation == "Dev")
                                     .OrderByDescending(p => p.Score)
                                     .FirstOrDefault();

            Console.WriteLine($"Smartest Developer: {smartestDev.FirstName} {smartestDev.LastName}, Score: {smartestDev.Score}");

            var youngestDev = people.Where(p => p.Occupation == "Dev")
                                     .OrderBy(p => p.Age)
                                     .FirstOrDefault();

            Console.WriteLine($"Youngest Developer: {youngestDev.FirstName} {youngestDev.LastName}, Age: {youngestDev.Age}");
        }

        // Đếm xen kẽ 1 developer già và 1 developer trẻ
        public static void CountAlternatingDevs()
        {
            var people = GenerateListOfPeople();
            var developers = people.Where(p => p.Occupation == "Dev").ToList();

            var sortedDevelopers = developers.OrderBy(p => p.Age).ToList();
            var oldestDevelopers = developers.OrderByDescending(p => p.Age).ToList();

            Console.WriteLine("Alternating Developers (Old and Young):");
            int maxCount = Math.Min(sortedDevelopers.Count, oldestDevelopers.Count);
            for (int i = 0; i < maxCount; i++)
            {
                Console.WriteLine($"Old Developer: {oldestDevelopers[i].FirstName} {oldestDevelopers[i].LastName}, Age: {oldestDevelopers[i].Age}");
                Console.WriteLine($"Young Developer: {sortedDevelopers[i].FirstName} {sortedDevelopers[i].LastName}, Age: {sortedDevelopers[i].Age}");
            }
        }

        // Phương thức tìm các developer lớn tuổi
        public static void QueryOlderDevelopers()
        {
            var people = GenerateListOfPeople();

            var olderDevelopers = from p in people
                                  where p.Occupation == "Dev" && p.Age > 30
                                  select p;

            Console.WriteLine("Older Developers (Age > 30):");
            foreach (var dev in olderDevelopers)
            {
                Console.WriteLine($"{dev.FirstName} {dev.LastName}, Age: {dev.Age}");
            }
        }
    }
}
