using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class FilterEmployees
{
    /// <summary>
    /// Personel tuple koleksiyonunu belirli kriterlere göre filtreler,
    /// maaş hesaplamalarını yapar ve sonucu JSON string olarak döndürür.
    /// </summary>
    /// <param name="employees">Ad, Yaş, Departman, Maaş ve İşe Giriş Tarihi içeren personel tuple koleksiyonu.</param>
    /// <returns>Filtrelenmiş personel isimleri, toplam, ortalama, en düşük ve en yüksek maaş ile çalışan sayısını içeren JSON string.</returns>
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
    // 1. Personelleri belirtilen kriterlere göre filtrele.
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList(); // Convert to a list to avoid multiple enumerations.

    // 2. Sonucu tutacak veri yapısını tanımla.
        var result = new
        {
            Names = new List<string>(),
            TotalSalary = 0m,
            AverageSalary = 0m,
            MinSalary = 0m,
            MaxSalary = 0m,
            Count = 0
        };

    // 3. Sadece filtrelenmiş personel varsa hesaplama ve sıralama yap.
        if (filteredEmployees.Count > 0)
        {
            // 3a. İsimleri önce uzunluğa göre azalan, sonra alfabetik olarak artan şekilde sırala.
            var sortedNames = filteredEmployees
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            // 3b. Maaş hesaplamalarını yap.
            var totalSalary = filteredEmployees.Sum(e => e.Salary);
            var averageSalary = filteredEmployees.Average(e => e.Salary);
            var minSalary = filteredEmployees.Min(e => e.Salary);
            var maxSalary = filteredEmployees.Max(e => e.Salary);
            var count = filteredEmployees.Count;

            // 3c. Hesaplanan değerlerle sonuç nesnesini güncelle.
            result = new
            {
                Names = sortedNames,
                TotalSalary = totalSalary,
                AverageSalary = Math.Round(averageSalary, 2),
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };
        }

    // 4. Sonuç nesnesini JSON string'e çevir.
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        return JsonSerializer.Serialize(result, jsonOptions);
    }
}
