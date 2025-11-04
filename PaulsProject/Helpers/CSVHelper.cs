using CsvHelper.Configuration;
using PaulsProject.Models;
using CsvHelper;
using System.Globalization;

namespace PaulsProject.Helpers
{
    public class CSVHelper
    {
        public List <Employee> ReadEmployeesFromCSV(string filename)
        {            
            string filePath = @"C:\Pauls PW Project\Pauls-Test-Project\PaulsProject\Data\";
            string file = filePath + filename;            
            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var csvData = csv.GetRecords<Employee>();            
            return csvData.ToList();
        }

        public List<PayRunData> ReadPayRunDataFromCSV(string filename)
        {               
            string filePath = @"C:\Pauls PW Project\Pauls-Test-Project\PaulsProject\Data\";
            string file = filePath + filename;
            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var csvData = csv.GetRecords<PayRunData>();
            return csvData.ToList();
        }
    }
}




