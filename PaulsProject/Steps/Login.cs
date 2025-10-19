using Microsoft.Playwright;
using PaulsProject.Pages;
using Reqnroll;

namespace PaulsProject.Steps
{
    [Binding]
    public class Login
    {
        public readonly IPage _page;
        public readonly SignUpPage _signUpPage;        

        public Login(IPage page)
        {
            _page = page;
            _signUpPage = new SignUpPage(_page);
        }

        [Given("The user logs into the {string} environment as user {string}")]
        public async Task GivenTheUserLogsIntoTheEnvironmentAsUser(string env, string user)                   
        {         
            string selectedEnv = _signUpPage.SelectEnvironment(env);
            await _page.GotoAsync(selectedEnv);
            var userDetails = _signUpPage.GetUser(user, env);            
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync(userDetails.Email);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(userDetails.Password);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Verify" }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Stay signed in", Exact = true }).ClickAsync();            
        }
    }    
}
