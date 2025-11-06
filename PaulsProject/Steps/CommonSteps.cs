using CsvHelper.Configuration;
using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PaulsProject.Steps
{
    [Binding]
    public class CommonSteps
    {
        public readonly IPage _page;

        private readonly ScenarioContext _scenarioContext;

        public CommonSteps(IPage page, ScenarioContext scenarioContext)
        {
            _page = page;
            _scenarioContext = scenarioContext;
        }

        [Given("The user creates a new employer")]
        public async Task GivenTheUserCreatesANewEmployer()
        {
            var employerName = Helpers.EmployerHelpers.GenerateEmployerName(_scenarioContext.ScenarioInfo.Title);
            await _page.GetByRole(AriaRole.Link, new() { Name = "Employer Logo 2023 P45" }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Add Employer" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Employer Name" }).FillAsync(employerName);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Create Employer" }).ClickAsync();
        }

        [Given("The user creates a new pay schedule")]
        public async Task GivenTheUserCreatesANewPaySchedule()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Payroll" }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Set Schedule" }).ClickAsync();
            await _page.Locator("#PaySchedule_FirstPaymentDate").FillAsync("30/04/2025");
            await _page.GetByRole(AriaRole.Button, new() { Name = "Same date" }).First.ClickAsync();
            await _page.GetByRole(AriaRole.Option, new() { Name = "Last dayLast day of the month" }).ClickAsync();
            await _page.Locator("#PaySchedule_FirstPeriodEndDate").FillAsync("30/04/2025");
            await _page.GetByRole(AriaRole.Button, new() { Name = "Same date" }).ClickAsync();
            await _page.GetByRole(AriaRole.Option, new() { Name = "Last dayLast day of the month" }).ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
        }

        [Given("The user adds new employees as follows:")]
        public async Task GivenTheUserAddsANewEmployee(Table table)
        {
            int employeeCount = 0;
            foreach (var row in table.Rows)
            {
                await _page.Locator("//a[normalize-space()='Employees']").ClickAsync();
                // Add new employee button in different location for first employee
                if (employeeCount == 0)
                {
                    await _page.GetByRole(AriaRole.Link, new() { Name = "Add an Employee" }).ClickAsync();
                }
                else
                {
                    await _page.GetByText("Add New").ClickAsync();
                }
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Title" }).FillAsync(row["Title"]);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "First" }).FillAsync(row["First Name"]);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Last" }).FillAsync(row["Last Name"]);
                var DOB = _page.GetByRole(AriaRole.Textbox, new() { Name = "Date of Birth" });
                await DOB.IsVisibleAsync();
                await DOB.FillAsync(row["DOB"]);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Start Date" }).FillAsync(row["Start Date"]);
                await _page.GetByRole(AriaRole.Button, new() { Name = "Create Employee" }).ClickAsync();
                await _page.GetByRole(AriaRole.Button, new() { Name = "-" }).ClickAsync();
                await _page.GetByRole(AriaRole.Option, new() { Name = "AThis is my first job since" }).ClickAsync();
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "NI Number" }).FillAsync(row["NI Number"]);
                await _page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
                await _page.GetByRole(AriaRole.Link, new() { Name = "Pay Options" }).ClickAsync();
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Monthly Amount" }).FillAsync(row["Salary"]);
                // Need to look into why 2 clicks are needed here
                await _page.Locator("span:has-text('Update Employee')").ClickAsync();
                await _page.Locator("span:has-text('Update Employee')").ClickAsync();
                employeeCount++;
            }
        }

        [Given("The user starts the next pay run")]
        public async Task GivenTheUserStartsTheNextPayRun()
        {
            await _page.Locator("a").Filter(new() { HasText = "Start Monthly Pay Run for 1st" }).ClickAsync();
        }

        [Then("The payrun should contain the following:")]
        public async Task ThenThePayrunShouldContainTheFollowing(DataTable table)
        {
            var columnHeadings = table.Header.ToList();
            columnHeadings.Remove("Employee");
            int headerIndex = 0;

            var payTable = _page.Locator("#table-list-paylinex");

            foreach (var row in table.Rows)
            {
                await _page.GetByText(row["Employee"]).ClickAsync();

                foreach (var heading in columnHeadings)
                {
                    var payslipRowDescription = payTable.Locator("tr").Filter(new()
                    {
                        HasText = heading
                    });
                    var payslipPayValue = await payslipRowDescription.Locator("td").Nth(2).InnerTextAsync();

                    payslipPayValue.Should().Be(row[heading], $"Expected {heading} to be {row[heading]}, found {payslipPayValue}.");
                    headerIndex++;
                }

                await _page.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();
            }
        }

        [Given("The user imports the {string} file")]
        public async Task GivenTheUserImportsTheFile(string filename)
        {
            var csvHelper = new Helpers.CSVHelper();
            var employeeData = csvHelper.ReadEmployeesFromCSV(filename);
            int employeeCount = 0;
            foreach (var row in employeeData)
            {
                await _page.Locator("//a[normalize-space()='Employees']").ClickAsync();
                // Add new employee button in different location for first employee
                if (employeeCount == 0)
                {
                    await _page.GetByRole(AriaRole.Link, new() { Name = "Add an Employee" }).ClickAsync();
                }
                else
                {
                    await _page.GetByText("Add New").ClickAsync();
                }
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Title" }).FillAsync(row.Title);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "First" }).FillAsync(row.FirstName);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Last" }).FillAsync(row.LastName);
                var DOB = _page.GetByRole(AriaRole.Textbox, new() { Name = "Date of Birth" });
                await DOB.IsVisibleAsync();
                await DOB.FillAsync(row.DOB);
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Start Date" }).FillAsync(row.StartDate);
                await _page.GetByRole(AriaRole.Button, new() { Name = "Create Employee" }).ClickAsync();
                await _page.GetByRole(AriaRole.Button, new() { Name = "-" }).ClickAsync();
                await _page.GetByRole(AriaRole.Option, new() { Name = "AThis is my first job since" }).ClickAsync();
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "NI Number" }).FillAsync(row.NINumber);
                await _page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
                await _page.GetByRole(AriaRole.Link, new() { Name = "Pay Options" }).ClickAsync();
                await _page.GetByRole(AriaRole.Textbox, new() { Name = "Monthly Amount" }).FillAsync(row.Salary);
                // Need to look into why 2 clicks are needed here
                await _page.Locator("span:has-text('Update Employee')").ClickAsync();
                await _page.Locator("span:has-text('Update Employee')").ClickAsync();
                employeeCount++;
            }
        }

        [Then("the payrun data should match the {string} file")]
        public async Task ThenThePayrunDataShouldMatchTheFile(string filename)
        {
            var csvHelper = new Helpers.CSVHelper();
            var payRunData = csvHelper.ReadPayRunDataFromCSV(filename);
            
            var columnHeadings = new List<string>();            
            var firstRow = payRunData.First();
            columnHeadings = firstRow.GetType().GetProperties()
            .Select(f => f.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? f.Name).ToList();            
            columnHeadings.Remove("Employee");
           
            var payTable = _page.Locator("#table-list-paylinex");

            foreach (var row in payRunData)
            {
                await _page.GetByText(row.Employee).ClickAsync();

                int headerIndex = 0;
                foreach (var heading in columnHeadings)
                {                    
                    var payslipRowDescription = payTable.Locator("tr").Filter(new()
                    {
                        HasText = heading
                    });
                    var payslipPayValue = await payslipRowDescription.Locator("td").Nth(2).InnerTextAsync();

                    var property = row.GetType().GetProperties()
                    .FirstOrDefault(p => (p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? p.Name) == heading);

                    var expectedValue = property?.GetValue(row)?.ToString() ?? "";

                    payslipPayValue.Should().Be(expectedValue, $"Expected {heading} to be {expectedValue}, found {payslipPayValue}.");
                }

                await _page.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();
            }
        }
    }
}

                
    