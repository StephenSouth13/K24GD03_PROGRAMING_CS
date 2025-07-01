using System; // Import namespace System, chứa các kiểu dữ liệu và chức năng cơ bản.
using System.Collections.Generic; // Import namespace System.Collections.Generic, chứa các kiểu dữ liệu tập hợp chung (ví dụ: List, Dictionary).
using System.Linq; // Import namespace System.Linq, cung cấp các lớp và interface hỗ trợ các truy vấn LINQ.

namespace Bai_2_Lab_10 // Định nghĩa namespace cho toàn bộ ứng dụng, giúp tổ chức code và tránh xung đột tên.
{
    public class Company // Định nghĩa lớp Company (Công ty).
    {
        public int Id { get; set; } // Thuộc tính Id: Mã định danh của công ty.
        public string Name { get; set; } // Thuộc tính Name: Tên của công ty.
    }

    public class Person // Định nghĩa lớp Person (Người).
    {
        public string FirstName { get; set; } // Thuộc tính FirstName: Tên đệm và tên của người.
        public string LastName { get; set; } // Thuộc tính LastName: Họ của người.
        public string Occupation { get; set; } // Thuộc tính Occupation: Nghề nghiệp của người.
        public int Age { get; set; } // Thuộc tính Age: Tuổi của người.
        public int CompanyId { get; set; } // Thuộc tính CompanyId: Mã định danh công ty mà người đó làm việc (khóa ngoại).
    }

    public class Program // Định nghĩa lớp Program, chứa phương thức Main là điểm bắt đầu thực thi của ứng dụng.
    {
        public static void Main() // Phương thức Main: Điểm vào của chương trình.
        {
            var companies = GenerateCompanies(); // Gọi phương thức GenerateCompanies() để tạo và lấy danh sách các công ty.
            var people = GeneratePeopleTest(); // Gọi phương thức GeneratePeopleTest() để tạo và lấy danh sách những người.

            // 1. Lọc danh sách người theo tiêu chí (tuổi, nghề nghiệp, tên)
            Console.WriteLine("Lọc danh sách người theo tiêu chí (tuổi, nghề nghiệp, tên):"); // In ra tiêu đề cho phần lọc.
            // Lọc ra những người có nghề nghiệp là "Dev" VÀ tuổi dưới 30.
            var youngDevs = FilterPeople(people, p => p.Occupation == "Dev" && p.Age < 30);
            PrintPeople(youngDevs); // In ra danh sách những người đã được lọc.

            // 2. Sắp xếp danh sách người theo tên và tuổi
            Console.WriteLine("\nSắp xếp danh sách người theo tên và tuổi:"); // In ra tiêu đề cho phần sắp xếp.
            // Sắp xếp danh sách người: ưu tiên theo FirstName, nếu FirstName giống nhau thì sắp xếp theo Age.
            var sortedPeople = SortPeople(people, (p1, p2) =>
            {
                int nameComparison = string.Compare(p1.FirstName, p2.FirstName, StringComparison.Ordinal); // So sánh FirstName.
                if (nameComparison == 0) // Nếu FirstName giống nhau.
                {
                    return p1.Age.CompareTo(p2.Age); // So sánh theo Age.
                }
                return nameComparison; // Trả về kết quả so sánh FirstName.
            });
            PrintPeople(sortedPeople); // In ra danh sách người đã được sắp xếp.

            // 3. Nhóm người theo nghề nghiệp và tính số lượng mỗi nhóm
            Console.WriteLine("\nNhóm người theo nghề nghiệp và tính số lượng mỗi nhóm:"); // In ra tiêu đề cho phần nhóm.
            var occupationGroups = GroupPeopleByOccupation(people); // Gọi phương thức GroupPeopleByOccupation để nhóm người theo nghề nghiệp.
            foreach (var group in occupationGroups) // Duyệt qua từng nhóm nghề nghiệp.
            {
                Console.WriteLine($"{group.Key}: {group.Value}"); // In ra tên nghề nghiệp và số lượng người trong nhóm đó.
            }

            // 4. Tìm người trẻ nhất/già nhất/tổng số tuổi
            Console.WriteLine("\nTìm người trẻ nhất/già nhất/tổng số tuổi:"); // In ra tiêu đề cho phần tìm kiếm và tính tổng.
            var youngestPerson = FindYoungestPerson(people); // Tìm người trẻ nhất.
            var oldestPerson = FindOldestPerson(people); // Tìm người già nhất.
            var totalAge = CalculateTotalAge(people); // Tính tổng số tuổi.
            Console.WriteLine($"Người trẻ nhất: {youngestPerson.FirstName} {youngestPerson.LastName} ({youngestPerson.Age})"); // In ra thông tin người trẻ nhất.
            Console.WriteLine($"Người già nhất: {oldestPerson.FirstName} {oldestPerson.LastName} ({oldestPerson.Age})"); // In ra thông tin người già nhất.
            Console.WriteLine($"Tổng số tuổi: {totalAge}"); // In ra tổng số tuổi.

            // 5. Lấy ra tên và tuổi của những người là "Manager"
            Console.WriteLine("\nLấy ra tên và tuổi của những người là \"Manager\":"); // In ra tiêu đề.
            var managerInfo = GetManagerInfo(people); // Lấy danh sách các Manager.
            PrintPeople(managerInfo); // In ra thông tin các Manager.

            // 6. Lọc các Manager làm tại "Microsoft"
            Console.WriteLine("\nLọc các Manager làm tại \"Microsoft\":"); // In ra tiêu đề.
            var microsoftManagers = GetMicrosoftManagers(people, companies); // Lọc các Manager làm tại Microsoft.
            PrintPeople(microsoftManagers); // In ra thông tin các Manager của Microsoft.

            // 7. Tìm công ty có nhiều Dev nhất
            Console.WriteLine("\nTìm công ty có nhiều Dev nhất:"); // In ra tiêu đề.
            var companyWithMostDevs = FindCompanyWithMostDevs(people, companies); // Tìm công ty có nhiều Dev nhất.
            Console.WriteLine($"Công ty có nhiều Dev nhất: {companyWithMostDevs.Name}"); // In ra tên công ty đó.
        }

        public static List<Company> GenerateCompanies() // Phương thức tạo và trả về danh sách các công ty.
        {
            return new List<Company> // Trả về một List mới chứa các đối tượng Company.
            {
                new Company { Id = 1, Name = "Microsoft" }, // Khởi tạo đối tượng Company với Id và Name.
                new Company { Id = 2, Name = "Google" }, // Khởi tạo đối tượng Company với Id và Name.
                new Company { Id = 3, Name = "Apple" } // Khởi tạo đối tượng Company với Id và Name.
            };
        }

        public static List<Person> GeneratePeopleTest() // Phương thức tạo và trả về danh sách những người.
        {
            return new List<Person> // Trả về một List mới chứa các đối tượng Person.
            {
                new Person { FirstName = "Sophia", LastName = "Dao", Occupation = "Manager", Age = 42, CompanyId = 2 },
                new Person { FirstName = "Albert", LastName = "Vo", Occupation = "Dev", Age = 25, CompanyId = 3 },
                new Person { FirstName = "Mia", LastName = "Pham", Occupation = "Dev", Age = 23, CompanyId = 2 },
                new Person { FirstName = "Nathan", LastName = "Do", Occupation = "Support", Age = 34, CompanyId = 1 },
                new Person { FirstName = "Karen", LastName = "Tran", Occupation = "QA", Age = 28, CompanyId = 3 },
                new Person { FirstName = "Henry", LastName = "Bui", Occupation = "DevOps", Age = 31, CompanyId = 2 },
                new Person { FirstName = "Lily", LastName = "Mai", Occupation = "Manager", Age = 43, CompanyId = 1 },
                new Person { FirstName = "Dylan", LastName = "Nguyen", Occupation = "Dev", Age = 30, CompanyId = 3 },
                new Person { FirstName = "Isabella", LastName = "Pham", Occupation = "Dev", Age = 26, CompanyId = 2 },
                new Person { FirstName = "Jason", LastName = "Le", Occupation = "Support", Age = 29, CompanyId = 1 },
                new Person { FirstName = "Cindy", LastName = "Vo", Occupation = "QA", Age = 27, CompanyId = 3 },
                new Person { FirstName = "Kevin", LastName = "Tran", Occupation = "Dev", Age = 28, CompanyId = 2 },
                new Person { FirstName = "Amy", LastName = "Lam", Occupation = "Manager", Age = 37, CompanyId = 1 },
                new Person { FirstName = "Sean", LastName = "Hoang", Occupation = "DevOps", Age = 32, CompanyId = 3 },
                new Person { FirstName = "Nina", LastName = "Huynh", Occupation = "HR", Age = 33, CompanyId = 2 },
                new Person { FirstName = "Tony", LastName = "Ngoc", Occupation = "Dev", Age = 25, CompanyId = 1 },
                new Person { FirstName = "Grace", LastName = "Nguyen", Occupation = "Dev", Age = 26, CompanyId = 3 },
                new Person { FirstName = "David", LastName = "Ly", Occupation = "Support", Age = 30, CompanyId = 2 },
                new Person { FirstName = "Olivia", LastName = "Pham", Occupation = "Dev", Age = 24, CompanyId = 1 },
                new Person { FirstName = "Ethan", LastName = "Le", Occupation = "QA", Age = 28, CompanyId = 3 },
                new Person { FirstName = "Ashley", LastName = "Truong", Occupation = "Dev", Age = 22, CompanyId = 2 },
                new Person { FirstName = "Tyler", LastName = "Kim", Occupation = "Manager", Age = 39, CompanyId = 1 },
                new Person { FirstName = "Luna", LastName = "Vo", Occupation = "DevOps", Age = 34, CompanyId = 3 },
                new Person { FirstName = "Zack", LastName = "Nguyen", Occupation = "Dev", Age = 27, CompanyId = 2 },
                new Person { FirstName = "Bella", LastName = "Pham", Occupation = "Support", Age = 31, CompanyId = 1 }
            };
        }

        public static List<Person> FilterPeople(List<Person> people, Func<Person, bool> filter) // Phương thức lọc danh sách người.
        {
            return people.Where(filter).ToList(); // Sử dụng LINQ's Where để lọc các phần tử thỏa mãn điều kiện 'filter', sau đó chuyển kết quả thành List.
        }

        public static List<Person> SortPeople(List<Person> people, Func<Person, Person, int> comparer) // Phương thức sắp xếp danh sách người.
        {
            // Sử dụng LINQ's OrderBy để sắp xếp theo FirstName, sau đó ThenBy để sắp xếp thêm theo Age nếu FirstName giống nhau.
            return people.OrderBy(p => p.FirstName).ThenBy(p => p.Age).ToList();
        }

        public static Dictionary<string, int> GroupPeopleByOccupation(List<Person> people) // Phương thức nhóm người theo nghề nghiệp và đếm số lượng.
        {
            // Sử dụng LINQ's GroupBy để nhóm các Person theo thuộc tính Occupation,
            // sau đó ToDictionary để chuyển kết quả nhóm thành Dictionary, với Key là nghề nghiệp và Value là số lượng người trong nhóm.
            return people.GroupBy(p => p.Occupation)
                         .ToDictionary(g => g.Key, g => g.Count());
        }

        public static Person FindYoungestPerson(List<Person> people) // Phương thức tìm người trẻ nhất.
        {
            // Sắp xếp danh sách người theo tuổi tăng dần (OrderBy) và lấy phần tử đầu tiên (FirstOrDefault) là người trẻ nhất.
            return people.OrderBy(p => p.Age).FirstOrDefault();
        }

        public static Person FindOldestPerson(List<Person> people) // Phương thức tìm người già nhất.
        {
            // Sắp xếp danh sách người theo tuổi giảm dần (OrderByDescending) và lấy phần tử đầu tiên (FirstOrDefault) là người già nhất.
            return people.OrderByDescending(p => p.Age).FirstOrDefault();
        }

        public static int CalculateTotalAge(List<Person> people) // Phương thức tính tổng số tuổi.
        {
            return people.Sum(p => p.Age); // Sử dụng LINQ's Sum để tính tổng thuộc tính Age của tất cả các Person.
        }

        public static List<Person> GetManagerInfo(List<Person> people) // Phương thức lấy thông tin Manager.
        {
            // Lọc ra những người có nghề nghiệp là "Manager", sau đó tạo một đối tượng Person mới chỉ với FirstName, LastName, Age, Occupation và CompanyId.
            return people.Where(p => p.Occupation == "Manager")
                         .Select(p => new Person { FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, Occupation = p.Occupation, CompanyId = p.CompanyId })
                         .ToList();
        }

        public static List<Person> GetMicrosoftManagers(List<Person> people, List<Company> companies) // Phương thức lọc Manager của Microsoft.
        {
            // Tìm đối tượng Company có tên là "Microsoft" trong danh sách companies.
            var microsoftCompany = companies.FirstOrDefault(c => c.Name == "Microsoft");
            if (microsoftCompany == null) // Kiểm tra nếu không tìm thấy công ty Microsoft.
            {
                return new List<Person>(); // Trả về danh sách rỗng để tránh lỗi NullReferenceException.
            }
            // Lọc ra những người có nghề nghiệp là "Manager" VÀ làm việc tại công ty Microsoft (dựa vào CompanyId).
            return people.Where(p => p.Occupation == "Manager" && p.CompanyId == microsoftCompany.Id)
                         .ToList();
        }

        public static Company FindCompanyWithMostDevs(List<Person> people, List<Company> companies) // Phương thức tìm công ty có nhiều Dev nhất.
        {
            // Tạo một Dictionary, với Key là đối tượng Company và Value là số lượng Dev trong công ty đó.
            var devCounts = companies.ToDictionary(c => c, c => people.Count(p => p.Occupation == "Dev" && p.CompanyId == c.Id));

            // Kiểm tra nếu không có Dev nào trong bất kỳ công ty nào.
            if (!devCounts.Any())
            {
                return null; // Trả về null hoặc có thể throw một ngoại lệ tùy thuộc vào logic mong muốn.
            }
            // Sắp xếp Dictionary theo số lượng Dev giảm dần (OrderByDescending) và lấy Key (đối tượng Company) của phần tử đầu tiên.
            return devCounts.OrderByDescending(kvp => kvp.Value).First().Key;
        }

        private static void PrintPeople(IEnumerable<Person> people) // Phương thức trợ giúp để in danh sách người ra console.
        {
            foreach (var person in people) // Duyệt qua từng người trong danh sách.
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} ({person.Age})"); // In ra tên, họ và tuổi của người đó.
            }
        }
    }
}