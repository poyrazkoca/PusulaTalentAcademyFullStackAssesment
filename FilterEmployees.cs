using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class FilterEmployees
{
    /// <summary>
    /// Filters a collection of employee tuples based on specific criteria,
    /// performs salary calculations, and returns the result as a JSON string.
    /// </summary>
    /// <param name="employees">A collection of employee tuples containing Name, Age, Department, Salary, and HireDate.</param>
    /// <returns>A JSON string with the filtered employee names, and the total, average, lowest, and highest salaries, and the employee count.</returns>
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        // 1. Filter the employees based on the specified criteria.
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList(); // Convert to a list to avoid multiple enumerations.

        // 2. Define a data structure to hold the result.
        var result = new
        {
            Names = new List<string>(),
            TotalSalary = 0m,
            AverageSalary = 0m,
            MinSalary = 0m,
            MaxSalary = 0m,
            Count = 0
        };

        // 3. Perform calculations and sorting only if there are filtered employees.
        if (filteredEmployees.Count > 0)
        {
            // 3a. Sort the names first by length (descending) and then alphabetically (ascending).
            var sortedNames = filteredEmployees
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            // 3b. Perform salary calculations.
            var totalSalary = filteredEmployees.Sum(e => e.Salary);
            var averageSalary = filteredEmployees.Average(e => e.Salary);
            var minSalary = filteredEmployees.Min(e => e.Salary);
            var maxSalary = filteredEmployees.Max(e => e.Salary);
            var count = filteredEmployees.Count;

            // 3c. Update the result object with the calculated values.
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

        // 4. Serialize the result object to a JSON string.
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        return JsonSerializer.Serialize(result, jsonOptions);
    }
}
