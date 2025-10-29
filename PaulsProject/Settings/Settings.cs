using Microsoft.Extensions.Configuration;

namespace PaulsProject.Settings
{
    public class Settings
    {        
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
            .AddJsonFile("C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\Settings\\appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration appSettingsConfig = appSettings.Build();
            AppSettings settings = new AppSettings();
            settings.environment = appSettingsConfig.GetValue<string>("environment");
            settings.user = appSettingsConfig.GetValue<string>("user");
            return settings;
        }

        public string SelectEnvironment()
        {            
            string envName = GetAppSettings().environment;
            var builder2 = new ConfigurationBuilder()
           .AddJsonFile("C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\Settings\\envs.json", optional: true, reloadOnChange: true);
            IConfiguration config2 = builder2.Build();
            string env = config2.GetValue<string>(envName);
            return env;
        }

        public UserCredentials GetUser()
        {                
            string userName = GetAppSettings().user;
            var builder2 = new ConfigurationBuilder()
            .AddJsonFile($"C:\\Pauls PW Project\\Pauls-Test-Project\\PaulsProject\\Users\\{userName}.json", optional: true, reloadOnChange: true);
            IConfiguration config2 = builder2.Build();
            UserCredentials user = new UserCredentials();
            user.Email = config2.GetValue<string>("email");
            user.Password = config2.GetValue<string>("password");
            return user;
        }
    }
}
