using Microsoft.Playwright;
using Reqnroll;

namespace PaulsProject.Steps
{
    [Binding]
    public class Login
    {
        public readonly IPage _page;
        public readonly Settings.Settings _settings;        

        public Login(IPage page)
        {
            _page = page;  
            _settings = new Settings.Settings();
        }

        [Given("The user logs in")]
        public async Task GivenTheUserLogsIntoTheEnvironmentAsUser()                   
        {         
            string selectedEnv = _settings.SelectEnvironment();
            await _page.GotoAsync(selectedEnv);
            var userDetails = _settings.GetUser();            
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync(userDetails.Email);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(userDetails.Password);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Verify" }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Stay signed in", Exact = true }).ClickAsync();            
        }
    }    
}
