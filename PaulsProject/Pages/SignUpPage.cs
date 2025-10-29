using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace PaulsProject.Pages
{
    public class SignUpPage
    {
        public readonly IPage _page;

        public SignUpPage(IPage page)
        {
            _page = page;
        }
        
        public class UserCredentials
        {             
            public string Email;
            public string Password;
        }

        public class AppSettings
        {
            public string environment;
            public string user;
        }

        public AppSettings GetAppSettings()
        {
            var appSettings = new ConfigurationBuilder()
            .AddJsonFile("C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration appSettingsConfig = appSettings.Build();
            AppSettings settings = new AppSettings();
            settings.environment = (appSettingsConfig.GetValue<string>("environment"));
            settings.user = (appSettingsConfig.GetValue<string>("user"));
            return settings;
        }

        public string SelectEnvironment()
        {            
            string envName = GetAppSettings().environment;
            var builder2 = new ConfigurationBuilder()
           .AddJsonFile("C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\envs.json", optional: true, reloadOnChange: true);
            IConfiguration config2 = builder2.Build();
            string env = (config2.GetValue<string>(envName));
            return env;
        }

        public UserCredentials GetUser()
        {                
            string userName = GetAppSettings().user;
            var builder2 = new ConfigurationBuilder()
            .AddJsonFile($"C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\Users\\{userName}.json", optional: true, reloadOnChange: true);
            IConfiguration config2 = builder2.Build();
            UserCredentials user = new UserCredentials();
            user.Email = (config2.GetValue<string>("email"));
            user.Password = (config2.GetValue<string>("password"));
            return user;
        }
    }
}
