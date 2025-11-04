using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace PaulsProject.Models
{
    public class PayRunData
    {
        [DisplayName("Employee")]
        [Name("Employee")]
        public string Employee{ get; set; }
        [DisplayName("Monthly Pay")]
        [Name("Monthly Pay")]
        public string MonthlyPay { get; set; }
        [DisplayName("PAYE Tax")]
        [Name("PAYE Tax")]
        public string PAYETax { get; set; }
        [DisplayName("National Insurance Contribution")]
        [Name("National Insurance Contribution")]
        public string NationalInsuranceContribution { get; set; }
        [DisplayName("Take Home Pay")]
        [Name("Take Home Pay")]
        public string TakeHomePay { get; set; }
    }
}
