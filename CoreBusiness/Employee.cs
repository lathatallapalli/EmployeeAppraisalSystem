using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBusiness
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string? EmployeeName { get; set; }

        [Required]
        public string? EmployeeDesignation { get; set; }

        [Required]
        public string? Mobile { get; set;}

        [Required]
        public string? Email { get; set; }

      
        public int? ManagerId { get; set; }

        [Required]
        public string? Password { get; set; }

        public int? AdminPermission { get; set; }


        //Navigation Properties
        public Employee? Manager { get; set; } // Navigation property for Manager
        public ICollection<Appraisal>? Appraisals { get; set; }
        public ICollection<Appraisal>? EmployeeAppraisals { get; set; } // Navigation property for Appraisals where the Employee is evaluated
        public ICollection<Appraisal>? ManagerAppraisals { get; set; } // Navigation property for Appraisals where the Employee is the manager
        public ICollection<AppraisalDetailsCompetency>? AppraisalDetailsCompetencies { get; set; } // Navigation property for AppraisalDetailsCompetencies
        public ICollection<AppraisalDetailsObjective>? AppraisalDetailsObjectives { get; set; } // Navigation property for AppraisalDetailsObjectives
        public ICollection<Employee> ManagedEmployees { get; set; } // Navigation property for managed employees

    }
}
