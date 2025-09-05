using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class FilterEmployees
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
    // personelleri belirtilen kriterlere göre filtreledim.
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList(); 

    // sonucu tutacak veri yapısını tanımladım.
        var result = new
        {
            Names = new List<string>(),
            TotalSalary = 0m,
            AverageSalary = 0m,
            MinSalary = 0m,
            MaxSalary = 0m,
            Count = 0
        };

    // sadece filtrelenmiş personel varsa hesaplama ve sıralama yaptırdım.
        if (filteredEmployees.Count > 0)
        {
            // isimleri önce uzunluğa göre azalan, sonra alfabetik olarak artan şekilde sıraladım.
            var sortedNames = filteredEmployees
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            // maaş hesaplamalarını yaptım.
            var totalSalary = filteredEmployees.Sum(e => e.Salary);
            var averageSalary = filteredEmployees.Average(e => e.Salary);
            var minSalary = filteredEmployees.Min(e => e.Salary);
            var maxSalary = filteredEmployees.Max(e => e.Salary);
            var count = filteredEmployees.Count;

            // hesaplanan değerlerle sonuç nesnesini güncelledim.
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

    // sonuç nesnesini JSON string'e çevirdim.
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        return JsonSerializer.Serialize(result, jsonOptions);
    }
}
