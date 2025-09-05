using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class Solution
{
    // XML'den alınan Person verisini temsil eden sınıf.
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }

    // JSON formatında çıktı verisini temsil eden sınıf.
    public class FilteredPeopleData
    {
        public List<string> Names { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// XML verisini parse ederek belirli kriterlere uyan kişileri bulur ve özetini JSON formatında döndürür.
    /// </summary>
    /// <param name="xmlData">Kişi bilgilerini içeren XML veri stringi.</param>
    /// <returns>Filtrelenmiş kişi isimleri, toplam maaş, ortalama maaş, maksimum maaş ve kişi sayısını içeren JSON string.</returns>
    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            // XML verisini stringden parse et.
            XDocument doc = XDocument.Parse(xmlData);

            // LINQ to XML kullanarak Person elemanlarını sorgula.
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

            // Çıktı nesnesini varsayılan değerlerle başlat.
            var outputData = new FilteredPeopleData
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            };

            // Hiç kişi bulunup bulunmadığını kontrol et, boş koleksiyonlarda hata olmaması için.
            if (filteredPeople.Any())
            {
                // İsimleri alfabetik olarak sırala.
                outputData.Names = filteredPeople
                    .Select(p => p.Name)
                    .OrderBy(name => name)
                    .ToList();

                // Toplam, ortalama ve maksimum maaşı hesapla.
                outputData.TotalSalary = filteredPeople.Sum(p => p.Salary);
                outputData.AverageSalary = filteredPeople.Average(p => p.Salary);
                outputData.MaxSalary = filteredPeople.Max(p => p.Salary);
                outputData.Count = filteredPeople.Count;
            }

            // Çıktı nesnesini JSON string'e çevir.
            var options = new JsonSerializerOptions { WriteIndented = false };
            return JsonSerializer.Serialize(outputData, options);
        }
        catch (Exception ex)
        {
            // Olası parse hatalarını yakala.
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            // Hata durumunda boş JSON nesnesi döndür.
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MaxSalary\":0,\"Count\":0}";
        }
    }
}
