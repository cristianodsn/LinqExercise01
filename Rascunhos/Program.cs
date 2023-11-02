using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Rascunhos.Entities;
using System.IO;

namespace Rascunhos
{

    class Program
    {
        static void Main(string[] args)
        {
            //C:\Users\crist\OneDrive\Área de Trabalho\Employee.csv

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            List<Employee> employees = new List<Employee>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] data = sr.ReadLine().Split(',');
                    string nameData = data[0];
                    string emailData = data[1];
                    double salaryData = double.Parse(data[2], CultureInfo.InvariantCulture);
                    Employee e = new Employee(nameData, emailData, salaryData);
                    employees.Add(e);
                }
            }
            Console.WriteLine();

            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Email for people whose salary is more than " + salary.ToString("F2", CultureInfo.InvariantCulture));

            //var greaterThanParameter = employees.Where(e => e.Salary > salary).OrderBy(e => e.Email).Select(e => e.Email);
            var greaterThanParameter =
                from e in employees
                where e.Salary > salary
                orderby e.Email
                select e.Email;


            foreach (string s in greaterThanParameter)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

            //var sumSalaryParameter = employees.Where(e => e.Name[0] == 'M').Select(e => e.Salary).DefaultIfEmpty(0.0).Sum();
            var sumSalaryParameter =
                (from e in employees
                 where e.Name[0] == 'M'
                 select e.Salary).DefaultIfEmpty(0.0).Sum();

            Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sumSalaryParameter.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
