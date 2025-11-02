using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaulsProject.Helpers
{
    public class EmployerHelpers
    {
        public static string GenerateEmployerName(string scenarioName)
        {
            return scenarioName + " " + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
