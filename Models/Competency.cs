using System.ComponentModel.DataAnnotations;

namespace EmployeeAppraisalSystem.Models
{
    public class Competency
    {
        public int CompetencyId { get; set; }

        [Required]
        public string CompetencyName { get; set; } = string.Empty;

        [Required]
        public CompetencyType competencyType { get; set; }
    }

    public enum CompetencyType
    {
        Technical,
        Behavioral
    }
}
