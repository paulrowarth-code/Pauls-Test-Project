using Microsoft.Playwright;
using Reqnroll;


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

        [Given("the user adds new employees as follows:")]
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
                    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Date of Birth" }).FillAsync(row["DOB"]);            
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
    }
}
