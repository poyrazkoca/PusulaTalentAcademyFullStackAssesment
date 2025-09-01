using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class Solution
{
    // A class to represent the Person data from the XML.
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }

    // A class to represent the output data in JSON format.
    public class FilteredPeopleData
    {
        public List<string> Names { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Parses the XML data to find individuals who meet specific criteria and returns a summary in JSON format.
    /// </summary>
    /// <param name="xmlData">The XML data string containing person information.</param>
    /// <returns>A JSON string with filtered person names, total salary, average salary, max salary, and count.</returns>
    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            // Parse the XML data from the string.
            XDocument doc = XDocument.Parse(xmlData);

            // Use LINQ to XML to query the Person elements.
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

            // Initialize the output object with default values.
            var outputData = new FilteredPeopleData
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            };

            // Check if any people were found to avoid errors on empty collections.
            if (filteredPeople.Any())
            {
                // Sort the names alphabetically.
                outputData.Names = filteredPeople
                    .Select(p => p.Name)
                    .OrderBy(name => name)
                    .ToList();

                // Calculate the total, average, and max salary.
                outputData.TotalSalary = filteredPeople.Sum(p => p.Salary);
                outputData.AverageSalary = filteredPeople.Average(p => p.Salary);
                outputData.MaxSalary = filteredPeople.Max(p => p.Salary);
                outputData.Count = filteredPeople.Count;
            }

            // Serialize the output object to a JSON string.
            var options = new JsonSerializerOptions { WriteIndented = false };
            return JsonSerializer.Serialize(outputData, options);
        }
        catch (Exception ex)
        {
            // Handle any potential parsing errors.
            Console.WriteLine($"An error occurred: {ex.Message}");
            // Return an empty JSON object in case of an error as per similar scenarios.
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MaxSalary\":0,\"Count\":0}";
        }
    }
}
