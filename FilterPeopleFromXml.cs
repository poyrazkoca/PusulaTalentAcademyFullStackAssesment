using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class Solution
{
    // XML'den alınan Person verisini temsil eden sınıfı oluşturdum.
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }

    // JSON formatında çıktı verisini temsil eden sınıfı oluşturdum.
    public class FilteredPeopleData
    {
        public List<string> Names { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int Count { get; set; }
    }

    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            // XML verisini stringden parse ettim.
            XDocument doc = XDocument.Parse(xmlData);

            // LINQ to XML kullanarak Person elemanlarını sorgulattım.
            var filteredPeople = doc.Descendants("Person")
                .Select(personElement => new Person
                {
                    Name = (string)personElement.Element("Name"),
                    Age = (int)personElement.Element("Age"),
                    Department = (string)personElement.Element("Department"),
                    Salary = (decimal)personElement.Element("Salary"),
                    HireDate = (DateTime)personElement.Element("HireDate")
                })
                .Where(person =>
                    person.Age > 30 &&
                    person.Department == "IT" &&
                    person.Salary > 5000 &&
                    person.HireDate.Year < 2019)
                .ToList();

            // çıktı nesnesini varsayılan değerlerle başlattım.
            var outputData = new FilteredPeopleData
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            };

            // boş koleksiyonlarda hata olmaması için hiç kişi bulunup bulunmadığını kontrol ettim.
            if (filteredPeople.Any())
            {
                // isimleri alfabetik olarak sıraladım.
                outputData.Names = filteredPeople
                    .Select(p => p.Name)
                    .OrderBy(name => name)
                    .ToList();

                // toplam, ortalama ve maksimum maaşı hesapladım.
                outputData.TotalSalary = filteredPeople.Sum(p => p.Salary);
                outputData.AverageSalary = filteredPeople.Average(p => p.Salary);
                outputData.MaxSalary = filteredPeople.Max(p => p.Salary);
                outputData.Count = filteredPeople.Count;
            }

            // çıktı nesnesini JSON string'e çevirdim.
            var options = new JsonSerializerOptions { WriteIndented = false };
            return JsonSerializer.Serialize(outputData, options);
        }
        catch (Exception ex)
        {
            // olası parse hatalarını yakalattım.
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            // hatalı data durumunda boş JSON nesnesi döndürdüm.
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MaxSalary\":0,\"Count\":0}";
        }
    }
}
