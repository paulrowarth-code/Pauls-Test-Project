using CsvHelper.Configuration.Attributes;

namespace PaulsProject.Models
{
    public class Employee
    {
        [Name("Title")]
        public string Title { get; set; }
        [Name("First Name")]
        public string FirstName { get; set; }
        [Name("Last Name")]
        public string LastName { get; set; }
        [Name("DOB")]
        public string DOB { get; set; }
        [Name("Start Date")]
        public string StartDate { get; set; }
        [Name("NI Number")]
        public string NINumber { get; set; }
        [Name("Salary")]
        public string Salary { get; set; }
    }
}
