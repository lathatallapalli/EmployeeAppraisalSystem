using System.ComponentModel.DataAnnotations;

namespace EmployeeAppraisalSystem.Models
{
    public class Login
    {
        [Required]
        public string username { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;
    }
}
