using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Reqnroll;
using Reqnroll.BoDi;

namespace PaulsProject.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext) 
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task SetupPlaywright()
        {
          var pw = await Playwright.CreateAsync();
            var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions {BypassCSP = true});
            var page = await browserContext.NewPageAsync();
            _objectContainer.RegisterInstanceAs(browser);
            _objectContainer.RegisterInstanceAs(page);
        }
    }
}