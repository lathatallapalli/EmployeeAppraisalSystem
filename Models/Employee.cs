using System.ComponentModel.DataAnnotations;

namespace EmployeeAppraisalSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string? EmployeeName { get; set; }

        [Required]
        public string? EmployeeDesignation { get; set; }

        [Required]
        public string? Mobile { get; set;}

        [Required]
        public string? Email { get; set; }

        [Required]
        public int? ManagerId { get; set; }

    }
}
