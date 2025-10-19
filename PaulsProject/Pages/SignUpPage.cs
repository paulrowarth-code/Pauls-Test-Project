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

        public string SelectEnvironment(string env)
        {
            switch (env)
            {
                case "Test":
                    return "https://test-app.staffology.co.uk/";

                case "Prod":
                    return "https://app.staffology.co.uk/";
            }
            return "Prod";
        }

        public UserCredentials GetUser(string user, string env)
        {
            var filePath = Path.Combine("Users", user + "." + env + ".json");
            var fileName = File.ReadAllText(filePath);
            UserCredentials userCredentials = JsonConvert.DeserializeObject<UserCredentials>(fileName);
            return userCredentials;
        }
    }
}
