using EmployeeAppraisalSystem.Controllers;
using Org.BouncyCastle.Ocsp;
using System.Data;

namespace EmployeeAppraisalSystem.Models
{
    public static class LoginRepository
    {
        /*private static List<Login> _users = new List<Login>()
        {
*//*            new Login { empId=1, username = "jacob", password = "jacob", role= "manager"},
            new Login { empId=10,username = "john", password = "john", role="employee"},
            new Login { empId=6,username = "lili", password = "lili", role="hr"}*//*
        };

        public static bool UserAuthentication(string username, string password)
        {
            // Search for the user in the login data
            foreach (var user in _users)
            {
                if (user.username == username.ToLower() && user.password == password.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class AuthorizationResult
    {
        public string Role { get; set; }
        public int EmpId { get; set; }
    }*/

    }

}
